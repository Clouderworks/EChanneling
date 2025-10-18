
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PatientProfileService } from '../services/patient-profile.service';
import { PatientRegistration } from '../models/patient-profile.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-patient-dashboard',
  templateUrl: './patient-dashboard.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule]
})
export class PatientDashboardComponent implements OnInit {
  patients: PatientRegistration[] = [];
  selectedPatient: PatientRegistration | null = null;
  loading = false;
  error: string | null = null;

  constructor(private profileService: PatientProfileService, public auth: AuthService) {}

  ngOnInit() {
    this.loadPatients();
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

  selectPatient(patient: PatientRegistration) {
    this.selectedPatient = patient;
  }

  clearSelection() {
    this.selectedPatient = null;
  }
}
