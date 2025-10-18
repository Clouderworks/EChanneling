import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { PatientRegistration } from '../models/patient-profile.model';

@Component({
  selector: 'app-patient-registration',
  templateUrl: './patient-registration.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})

export class PatientRegistrationComponent {
  registrationForm;
  submitted = false;
  error: string | null = null;
  nhiLookupResult: any = null;
  nhiLookupError: string | null = null;
  lookupNhi() {
    const nhiNumber = this.registrationForm.get('nhiNumber')?.value;
    if (!nhiNumber) {
      this.nhiLookupError = 'Please enter an NHI number.';
      this.nhiLookupResult = null;
      return;
    }
    this.nhiLookupError = null;
    this.nhiLookupResult = null;
    this.regService.lookupNhi(nhiNumber).subscribe({
      next: (result) => {
        this.nhiLookupResult = result;
        // Autofill fields if found
        this.registrationForm.patchValue({
          firstName: result.firstName,
          lastName: result.lastName,
          dateOfBirth: result.dateOfBirth?.substring(0, 10) // yyyy-MM-dd
        });
      },
      error: (err) => {
        this.nhiLookupError = err.error?.message || 'NHI not found.';
        this.nhiLookupResult = null;
      }
    });
  }

  constructor(private fb: FormBuilder, private regService: RegistrationService) {
    this.registrationForm = this.fb.group({
      nhiNumber: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      ethnicity: [''],
      language: [''],
      address: [''],
      phone: [''],
      email: ['', [Validators.required, Validators.email]],
      familyHistory: this.fb.array([]),
      geneticRisks: this.fb.array([]),
      socialDeterminants: this.fb.group({
        occupation: [''],
        education: [''],
        housing: [''],
        incomeLevel: [''],
        maritalStatus: [''],
        notes: ['']
      })
    });
  }

  get familyHistory() {
    return this.registrationForm.get('familyHistory') as FormArray;
  }
  get geneticRisks() {
    return this.registrationForm.get('geneticRisks') as FormArray;
  }

  addFamilyHistory() {
    this.familyHistory.push(this.fb.group({
      relation: ['', Validators.required],
      condition: ['', Validators.required],
      notes: ['']
    }));
  }
  removeFamilyHistory(i: number) {
    this.familyHistory.removeAt(i);
  }

  addGeneticRisk() {
    this.geneticRisks.push(this.fb.group({
      gene: ['', Validators.required],
      riskLevel: ['', Validators.required],
      notes: ['']
    }));
  }
  removeGeneticRisk(i: number) {
    this.geneticRisks.removeAt(i);
  }

  private coerceToString(obj: any): any {
    if (Array.isArray(obj)) {
      return obj.map((item) => this.coerceToString(item));
    } else if (obj && typeof obj === 'object') {
      const result: any = {};
      for (const key of Object.keys(obj)) {
        const val = obj[key];
        result[key] = val == null ? '' : this.coerceToString(val);
      }
      return result;
    } else {
      return obj == null ? '' : String(obj);
    }
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      const socialDeterminants = this.registrationForm.get('socialDeterminants')?.value || {};
      const value: PatientRegistration = {
        ...this.coerceToString(this.registrationForm.value),
        familyHistory: this.coerceToString(this.familyHistory.value),
        geneticRisks: this.coerceToString(this.geneticRisks.value),
        socialDeterminants: this.coerceToString(socialDeterminants)
      };
      this.regService.registerPatient(value).subscribe({
        next: () => {
          this.submitted = true;
          this.error = null;
        },
        error: err => {
          this.error = err.error?.message || 'Registration failed.';
        }
      });
    }
  }
}
