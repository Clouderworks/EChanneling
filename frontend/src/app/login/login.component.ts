import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [CommonModule, FormsModule]
})
export class LoginComponent {
  username = '';
  password = '';
  error: string | null = null;
  loading = false;

  constructor(private auth: AuthService, private http: HttpClient, private router: Router) {}

  login() {
    this.error = null;
    this.loading = true;
    this.http.post('/api/auth/login', { username: this.username, password: this.password }, { withCredentials: true })
      .subscribe({
        next: () => {
          this.auth.login().then(success => {
            if (success) {
              this.router.navigate(['/dashboard']);
            } else {
              this.error = 'Failed to fetch user session.';
            }
            this.loading = false;
          });
        },
        error: (err) => {
          if (err.status === 0) {
            this.error = 'Network error: Unable to reach server.';
          } else if (err.status === 401) {
            this.error = err.error?.message || 'Invalid username or password.';
          } else if (err.status === 500) {
            this.error = err.error?.message || 'Server error. Please try again later.';
          } else {
            this.error = err.error?.message || 'Login failed.';
          }
          this.loading = false;
        }
      });
  }
}
