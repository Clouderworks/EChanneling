import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, Notification } from './notification.service';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div *ngIf="notification" [ngClass]="'notification ' + notification.type" role="alert" tabindex="0" aria-live="assertive">
      {{ notification.message }}
    </div>
  `,
  styles: [`
    .notification {
      position: fixed;
      top: 1.5rem;
      right: 1.5rem;
      min-width: 220px;
      padding: 1rem 1.5rem;
      border-radius: 6px;
      font-size: 1rem;
      z-index: 2000;
      box-shadow: 0 2px 8px rgba(0,0,0,0.12);
      outline: none;
    }
    .notification.success { background: #e8f5e9; color: #388e3c; border: 1px solid #388e3c; }
    .notification.error { background: #ffebee; color: #b71c1c; border: 1px solid #b71c1c; }
    .notification.info { background: #e3f2fd; color: #1565c0; border: 1px solid #1565c0; }
    .notification.warning { background: #fffde7; color: #fbc02d; border: 1px solid #fbc02d; }
  `]
})
export class NotificationComponent implements OnInit {
  notification: Notification | null = null;
  private timeoutId: any;

  constructor(private notificationService: NotificationService) {}

  ngOnInit() {
    this.notificationService.notifications$.subscribe(n => {
      this.notification = n;
      clearTimeout(this.timeoutId);
      setTimeout(() => {
        this.notification = null;
      }, 3500);
    });
  }
}
