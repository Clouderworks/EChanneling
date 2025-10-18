

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

export interface UserSession {
  roles: string[];
  username: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  isAdmin(): boolean {
    return this.hasRole('Admin');
  }
  isDoctor(): boolean {
    return this.hasRole('Doctor');
  }
  isReceptionist(): boolean {
    return this.hasRole('Receptionist');
  }
  private user: UserSession | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  // Call this after login to fetch user session
  fetchSession(): Promise<UserSession | null> {
    return this.http.get<UserSession>('/api/auth/me', { withCredentials: true })
      .toPromise()
      .then(user => {
        if (user) {
          this.user = user;
          return user;
        } else {
          this.user = null;
          return null;
        }
      })
      .catch(() => {
        this.user = null;
        return null;
      });
  }

  isAuthenticated(): boolean {
    return !!this.user;
  }

  getUser(): UserSession | null {
    return this.user;
  }

  getRoles(): string[] {
    return this.user?.roles || [];
  }

  hasRole(role: string): boolean {
    return this.getRoles().includes(role);
  }

  hasAnyRole(roles: string[]): boolean {
    return roles.some(r => this.hasRole(r));
  }

  login(): Promise<boolean> {
    // After login, fetch session
    return this.fetchSession().then(user => !!user);
  }

  logout(): void {
    this.http.post('/api/auth/logout', {}, { withCredentials: true }).subscribe({
      next: () => {
        this.user = null;
        this.router.navigate(['/login']);
      },
      error: () => {
        this.user = null;
        this.router.navigate(['/login']);
      }
    });
  }
}
