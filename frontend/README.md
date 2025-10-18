# Frontend

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.1.1.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.


---

## Role-Based Access Control (RBAC)

The frontend enforces RBAC in the UI:

- **Admin**: Full access, including user management and audit logs.
- **Doctor**: Access to patient registration, profiles, and analytics.
- **Receptionist**: Access to registration and basic patient info.

Role checks are performed using the `AuthService` and are reflected in navigation and page access.

## Security Features

- **JWT in HttpOnly Cookies**: Auth tokens are never stored in localStorage. All API requests use credentials.
- **CSRF Protection**: All unsafe HTTP methods are protected by anti-forgery tokens.
- **Audit Logging**: Sensitive actions are logged and viewable by Admins.

## In-App Help

Tooltips and help dialogs are available throughout the UI to explain RBAC and security features. Admins can view audit logs and manage users from the dashboard.

---

For more information, see the main project README or contact the maintainers.
