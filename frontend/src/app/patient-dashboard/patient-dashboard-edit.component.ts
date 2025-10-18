import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PatientProfileService } from '../services/patient-profile.service';
import { PatientRegistration } from '../models/patient-profile.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-patient-dashboard-edit',
  templateUrl: './patient-dashboard-edit.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class PatientDashboardEditComponent implements OnInit {
  downloadBarcode(value: string, filename: string) {
    const url = this.getBarcodeUrl(value);
    fetch(url)
      .then(res => res.text())
      .then(svg => {
        const blob = new Blob([svg], { type: 'image/svg+xml' });
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = filename;
        link.click();
        URL.revokeObjectURL(link.href);
      });
  }

  printBarcode(value: string) {
    const url = this.getBarcodeUrl(value);
    fetch(url)
      .then(res => res.text())
      .then(svg => {
        const printWindow = window.open('', '_blank');
        if (printWindow) {
          printWindow.document.write('<html><head><title>Print Barcode</title></head><body style="margin:0;display:flex;align-items:center;justify-content:center;height:100vh;">' + svg + '</body></html>');
          printWindow.document.close();
          printWindow.focus();
          setTimeout(() => printWindow.print(), 300);
        }
      });
  }
  patients: PatientRegistration[] = [];
  selectedPatient: PatientRegistration | null = null;
  loading = false;
  error: string | null = null;
  updateError: string | null = null;
  updateSuccess = false;
  searchTerm = '';

  genderStats: any = {};
  ageGroups: any = {};
  ethnicityStats: any = {};
  commonConditions: any = {};
  languageStats: any = {};

  constructor(private profileService: PatientProfileService, public auth: AuthService) {}
  // RBAC helpers
  isAdmin(): boolean {
    return this.auth.hasRole('Admin');
  }
  isDoctor(): boolean {
    return this.auth.hasRole('Doctor');
  }
  isReceptionist(): boolean {
    return this.auth.hasRole('Receptionist');
  }

  ngOnInit() {
    this.loadPatients();
    this.loadAnalytics();
  }

  getBarcodeUrl(value: string): string {
    return `/api/barcode?value=${encodeURIComponent(value)}`;
  }
  loadAnalytics() {
    this.profileService.getGenderAnalytics().subscribe(data => this.genderStats = data);
    this.profileService.getAgeGroupAnalytics().subscribe(data => this.ageGroups = data);
    this.profileService.getEthnicityAnalytics().subscribe(data => this.ethnicityStats = data);
    this.profileService.getCommonConditionsAnalytics().subscribe(data => this.commonConditions = data);
    this.profileService.getLanguageAnalytics().subscribe(data => this.languageStats = data);
  }

  loadPatients() {
    this.loading = true;
    this.profileService.getAllPatients().subscribe({
      next: (data) => {
        this.patients = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.error?.message || 'Failed to load patients.';
        this.loading = false;
      }
    });
  }

  filteredPatients(): PatientRegistration[] {
    const term = this.searchTerm.toLowerCase();
    return this.patients.filter(p =>
      p.firstName.toLowerCase().includes(term) ||
      p.lastName.toLowerCase().includes(term) ||
      p.nhiNumber.toLowerCase().includes(term)
    );
  }

  selectPatient(patient: PatientRegistration) {
    this.selectedPatient = { ...patient };
    this.updateError = null;
    this.updateSuccess = false;
  }

  clearSelection() {
    this.selectedPatient = null;
    this.updateError = null;
    this.updateSuccess = false;
  }

  onUpdate(form: any) {
    if (form.valid && this.selectedPatient) {
      this.profileService.updatePatient(this.selectedPatient.id, this.selectedPatient).subscribe({
        next: () => {
          this.updateSuccess = true;
          this.updateError = null;
          this.loadPatients();
        },
        error: (err) => {
          this.updateError = err.error?.message || 'Update failed.';
          this.updateSuccess = false;
        }
      });
    } else {
      this.updateError = 'Please fill all required fields.';
      this.updateSuccess = false;
    }
  }

  // The following methods are now replaced by backend analytics:
  // getUniqueEthnicities, getUniqueLanguages, getGenderStats, getAgeGroups, getCommonConditions

  exportPatientsCSV() {
    if (!this.patients.length) return;
  const header = Object.keys(this.patients[0]).filter(k => typeof (this.patients[0] as any)[k] !== 'object');
  const rows = this.patients.map(p => header.map(h => JSON.stringify((p as any)[h] ?? '')));
    const csv = [header.join(','), ...rows.map(r => r.join(','))].join('\r\n');
    this.downloadCSV(csv, 'patients.csv');
  }

  exportAnalyticsCSV() {
    const analytics = [
      ['Gender', ...Object.entries(this.genderStats).map(([k, v]) => `${k}: ${v}`)].join(','),
      ['Age Groups', ...Object.entries(this.ageGroups).map(([k, v]) => `${k}: ${v}`)].join(','),
      ['Ethnicity', ...Object.entries(this.ethnicityStats).map(([k, v]) => `${k}: ${v}`)].join(','),
      ['Languages', ...Object.entries(this.languageStats).map(([k, v]) => `${k}: ${v}`)].join(','),
      ['Common Conditions', ...Object.entries(this.commonConditions).map(([k, v]) => `${k}: ${v}`)].join(',')
    ];
    const csv = analytics.join('\r\n');
    this.downloadCSV(csv, 'analytics.csv');
  }

  private downloadCSV(csv: string, filename: string) {
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    a.click();
    window.URL.revokeObjectURL(url);
  }
}
