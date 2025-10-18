# EChanneling Backend

This is the .NET 9 Web API backend for the New Zealand Hospital Doctor Channeling System.

## Getting Started

- Ensure you have .NET 9 SDK installed.
- Configure your connection string in `Config/appsettings.Development.json`.
- To run the backend:

```powershell
cd backend
 dotnet run
```

## Project Structure
- Controllers: API endpoints
- Models: Data models
- Services: Business logic

---

# EChanneling Frontend

This is the Angular 20 frontend for the New Zealand Hospital Doctor Channeling System.

## Getting Started

- Ensure you have Node.js and Angular CLI installed.
- To run the frontend:

```powershell
cd frontend
ng serve
```

## Project Structure
- src/app: Main application code
- src/environments: Environment configs

---


---

## Role-Based Access Control (RBAC)

The system uses robust RBAC to ensure only authorized users can access sensitive features:

- **Admin**: Full access to all features, including user management and audit logs.
- **Doctor**: Access to patient registration, profiles, and analytics.
- **Receptionist**: Access to registration and basic patient info.

Role checks are enforced both in the backend (API) and frontend (UI). Unauthorized actions are blocked and logged.

## Security Features

- **JWT Authentication**: Secure login with JWT stored in HttpOnly, Secure cookies (never in localStorage).
- **CSRF Protection**: All unsafe HTTP methods are protected by anti-forgery tokens.
- **Rate Limiting**: Backend APIs are protected against brute-force and abuse via IP rate limiting.
- **Audit Logging**: All sensitive actions (login, user management, patient edits) are logged and viewable by Admins.
- **Session Management**: User session is checked via `/api/auth/me` endpoint; logout clears the session cookie.

## In-App Help

The application includes in-app help and tooltips for RBAC and security features. Admins can view audit logs and user management screens for full visibility.

---

For more details, see the project documentation or contact the maintainers.
