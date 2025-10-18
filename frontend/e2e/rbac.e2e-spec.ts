import { test, expect } from '@playwright/test';

// Example users (these JWTs should be valid for your backend or mocked)
const adminJwt = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsInJvbGVzIjpbIkFkbWluIl0sImV4cCI6MTk5OTk5OTk5OX0.signature';
const doctorJwt = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkb2N0b3IiLCJyb2xlcyI6WyJEb2N0b3IiXSwiZXhwIjoxOTk5OTk5OTk5fQ.signature';
const receptionistJwt = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJyZXB0Iiwicm9sZXMiOlsiUmVjZXB0aW9uaXN0Il0sImV4cCI6MTk5OTk5OTk5OX0.signature';

function setJwt(page, jwt) {
  return page.addInitScript(token => {
    localStorage.setItem('jwt_token', token);
  }, jwt);
}

test.describe('RBAC navigation and access', () => {
  test('Admin can access user management and audit logs', async ({ page }) => {
    await setJwt(page, adminJwt);
    await page.goto('/');
    await expect(page.getByText('User Management')).toBeVisible();
    await expect(page.getByText('View Audit Logs')).toBeVisible();
    await page.click('text=User Management');
    await expect(page.getByRole('heading', { name: 'User Management' })).toBeVisible();
    await page.click('text=View Audit Logs');
    await expect(page.getByRole('heading', { name: 'Audit Logs' })).toBeVisible();
  });

  test('Doctor cannot access user management or audit logs', async ({ page }) => {
    await setJwt(page, doctorJwt);
    await page.goto('/');
    await expect(page.getByText('User Management')).toHaveCount(0);
    await expect(page.getByText('View Audit Logs')).toHaveCount(0);
    await page.goto('/user-management');
    await expect(page.getByText('You do not have permission')).toBeVisible();
    await page.goto('/audit-logs');
    await expect(page.getByText('You do not have permission')).toBeVisible();
  });

  test('Receptionist cannot access user management or audit logs', async ({ page }) => {
    await setJwt(page, receptionistJwt);
    await page.goto('/');
    await expect(page.getByText('User Management')).toHaveCount(0);
    await expect(page.getByText('View Audit Logs')).toHaveCount(0);
    await page.goto('/user-management');
    await expect(page.getByText('You do not have permission')).toBeVisible();
    await page.goto('/audit-logs');
    await expect(page.getByText('You do not have permission')).toBeVisible();
  });
});
