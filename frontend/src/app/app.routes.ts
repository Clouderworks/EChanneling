
import { Routes } from '@angular/router';
import { PatientRegistrationComponent } from './patient-registration/patient-registration.component';
import { PatientDashboardComponent } from './patient-dashboard/patient-dashboard.component';
import { PatientDashboardEditComponent } from './patient-dashboard/patient-dashboard-edit.component';

import { AuthGuard } from './services/auth.guard';
import { RoleGuard } from './services/role.guard';
import { LoginComponent } from './login/login.component';

// Audit log component (lazy loaded)
import { AuditLogComponent } from './audit-log/audit-log.component';
import { UserManagementComponent } from './user-management/user-management.component';

export const routes: Routes = [
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: 'audit-logs',
		component: AuditLogComponent,
		canActivate: [AuthGuard, RoleGuard],
		data: { roles: ['Admin'] }
	},
	{
		path: 'user-management',
		component: UserManagementComponent,
		canActivate: [AuthGuard, RoleGuard],
		data: { roles: ['Admin'] }
	},
	{
		path: '',
		redirectTo: 'dashboard',
		pathMatch: 'full',
	},
	{
		path: 'register',
		component: PatientRegistrationComponent,
		canActivate: [AuthGuard, RoleGuard],
		data: { roles: ['Admin', 'Receptionist'] }
	},
	{
		path: 'dashboard',
		component: PatientDashboardComponent,
		canActivate: [AuthGuard]
	},
	{
		path: 'dashboard/edit',
		component: PatientDashboardEditComponent,
		canActivate: [AuthGuard, RoleGuard],
		data: { roles: ['Admin', 'Doctor'] }
	},
	{
		path: '**',
		redirectTo: 'dashboard',
	},
];
