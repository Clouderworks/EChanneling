
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PatientRegistration } from '../models/patient-profile.model';

@Injectable({ providedIn: 'root' })
export class PatientProfileService {
  private apiUrl = '/api/patients';

  constructor(private http: HttpClient) {}

  getPatient(id: string): Observable<PatientRegistration> {
    return this.http.get<PatientRegistration>(`${this.apiUrl}/${id}`);
  }

  updatePatient(id: string, data: PatientRegistration): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  getAllPatients(): Observable<PatientRegistration[]> {
    return this.http.get<PatientRegistration[]>(this.apiUrl);
  }

  getGenderAnalytics(): Observable<any> {
    return this.http.get(`${this.apiUrl}/analytics/gender`);
  }

  getAgeGroupAnalytics(): Observable<any> {
    return this.http.get(`${this.apiUrl}/analytics/age-groups`);
  }

  getEthnicityAnalytics(): Observable<any> {
    return this.http.get(`${this.apiUrl}/analytics/ethnicity`);
  }

  getCommonConditionsAnalytics(): Observable<any> {
    return this.http.get(`${this.apiUrl}/analytics/common-conditions`);
  }

  getLanguageAnalytics(): Observable<any> {
    return this.http.get(`${this.apiUrl}/analytics/languages`);
  }
}
