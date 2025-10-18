import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserManagementService } from './user-management.service';
import { User, Role } from './user-management.model';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../shared/notification.service';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  roles: Role[] = [];
  loading = false;
  error: string | null = null;
  showCreate = false;
  newUser: Partial<User> = { username: '', roles: [], isActive: true };

  // Pagination/filtering
  filter = '';
  page = 1;
  pageSize = 10;

  constructor(
    private userService: UserManagementService,
    public auth: AuthService,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {
    if (this.auth.isAdmin()) {
      this.fetchUsers();
      this.fetchRoles();
    }
  }

  fetchUsers() {
    this.loading = true;
    this.userService.getUsers().subscribe({
      next: users => {
        this.users = users;
        this.page = 1;
        this.loading = false;
      },
      error: err => {
        this.notification.show('Failed to load users.', 'error');
        this.loading = false;
      }
    });
  }

  get filteredUsers(): User[] {
    let filtered = this.users;
    if (this.filter) {
      const f = this.filter.toLowerCase();
      filtered = filtered.filter(u => u.username.toLowerCase().includes(f));
    }
    return filtered;
  }

  get pagedUsers(): User[] {
    const start = (this.page - 1) * this.pageSize;
    return this.filteredUsers.slice(start, start + this.pageSize);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredUsers.length / this.pageSize) || 1;
  }

  setPage(p: number) {
    if (p >= 1 && p <= this.totalPages) this.page = p;
  }

  fetchRoles() {
    this.userService.getRoles().subscribe({
      next: roles => this.roles = roles,
      error: () => {
        this.roles = [];
        this.notification.show('Failed to load roles.', 'error');
      }
    });
  }

  openCreate() {
    this.showCreate = true;
    this.newUser = { username: '', roles: [], isActive: true };
  }

  createUser() {
    this.userService.createUser(this.newUser).subscribe({
      next: user => {
        this.users.push(user);
        this.showCreate = false;
        this.notification.show('User created successfully.', 'success');
      },
      error: err => this.notification.show('Failed to create user.', 'error')
    });
  }

  updateUser(user: User) {
    this.userService.updateUser(user.id, user).subscribe({
      next: updated => {
        Object.assign(user, updated);
        this.notification.show('User updated.', 'success');
      },
      error: err => this.notification.show('Failed to update user.', 'error')
    });
  }

  deactivateUser(user: User) {
    if (!confirm('Deactivate this user?')) return;
    this.userService.deactivateUser(user.id).subscribe({
      next: () => {
        user.isActive = false;
        this.notification.show('User deactivated.', 'info');
      },
      error: err => this.notification.show('Failed to deactivate user.', 'error')
    });
  }
}
