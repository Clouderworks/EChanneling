-- This file contains all seed INSERT statements for the New Zealand Hospital Doctor Channeling System.
-- Run after tables.sql is executed.

-- Insert default roles
INSERT INTO [Role] (Name) VALUES ('Admin'), ('Doctor'), ('Receptionist');

-- Insert default admin user (password: Admin@123, hash below is for example only)
INSERT INTO [UserAccount] (Username, PasswordHash, DisplayName, IsActive)
VALUES ('admin', '$2b$10$u1Q9Qw1Q9Qw1Q9Qw1Q9QwOeQw1Q9Qw1Q9Qw1Q9Qw1Q9Qw1Q9Qw1Q', 'System Administrator', 1);

-- Assign Admin role to admin user
INSERT INTO [UserRole] (UserId, RoleId)
SELECT u.UserId, r.RoleId
FROM [UserAccount] u, [Role] r
WHERE u.Username = 'admin' AND r.Name = 'Admin';
-- (All seed INSERT statements go here, deduplicated and cleaned)
-- ...existing code...
