import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PatientRegistration {
  nhiNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  ethnicity: string;
  language: string;
  address: string;
  phone: string;
  email: string;
}

@Injectable({ providedIn: 'root' })
export class RegistrationService {
  private apiUrl = '/api/registration';
  private nhiUrl = '/api/nhi';

  constructor(private http: HttpClient) {}

  registerPatient(data: PatientRegistration): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  lookupNhi(nhiNumber: string): Observable<any> {
    return this.http.get(`${this.nhiUrl}/${nhiNumber}`);
  }
}
