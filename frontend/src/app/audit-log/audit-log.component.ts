import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuditLogService } from './audit-log.service';
import { AuditLog } from './audit-log.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-audit-log',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './audit-log.component.html',
  styleUrls: ['./audit-log.component.scss']
})
export class AuditLogComponent implements OnInit {
  auditLogs: AuditLog[] = [];
  loading = false;
  error: string | null = null;

  constructor(private auditLogService: AuditLogService, public auth: AuthService) {}

  ngOnInit(): void {
    if (this.auth.isAdmin()) {
      this.fetchLogs();
    }
  }

  fetchLogs() {
    this.loading = true;
    this.error = null;
    this.auditLogService.getAuditLogs().subscribe({
      next: logs => {
        this.auditLogs = logs;
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load audit logs.';
        this.loading = false;
      }
    });
  }
}
