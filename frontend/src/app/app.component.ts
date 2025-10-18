

import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { NotificationComponent } from './shared/notification.component';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, NotificationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'frontend';
  constructor(public auth: AuthService) {}

  ngOnInit(): void {
    this.auth.fetchSession();
  }

  logout() {
    this.auth.logout();
  }
}
