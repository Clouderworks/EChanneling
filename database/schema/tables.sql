-- This file contains all CREATE TABLE statements for the New Zealand Hospital Doctor Channeling System.
-- Indexes, views, procedures, functions, triggers, seed data, and security policies are in separate files.

-- =============================================
-- CORE SYSTEM TABLES
-- =============================================

CREATE TABLE SystemConfiguration (
    ConfigId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ConfigKey NVARCHAR(100) NOT NULL UNIQUE,
    ConfigValue NVARCHAR(MAX),
    Description NVARCHAR(500),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    CreatedBy UNIQUEIDENTIFIER,
    UpdatedBy UNIQUEIDENTIFIER
);

CREATE TABLE AuditLog (
    AuditId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TableName NVARCHAR(100) NOT NULL,
    RecordId UNIQUEIDENTIFIER NOT NULL,
    Action NVARCHAR(20) NOT NULL, -- INSERT, UPDATE, DELETE, SELECT
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    UserId UNIQUEIDENTIFIER,
    IPAddress NVARCHAR(45),
    UserAgent NVARCHAR(500),
    Timestamp DATETIME2 DEFAULT GETUTCDATE(),
    SessionId NVARCHAR(100)
);

-- User and Role tables for authentication and RBAC

CREATE TABLE [Role] (
    RoleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [UserAccount] (
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    DisplayName NVARCHAR(100),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE [UserRole] (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES [UserAccount](UserId),
    FOREIGN KEY (RoleId) REFERENCES [Role](RoleId)
);
-- (All other CREATE TABLE statements go here, deduplicated and cleaned)
-- ...existing code...
