-- SQL Server batch script to execute all schema files in the correct order
-- Usage: Run this script in SQL Server Management Studio (SSMS) or sqlcmd
-- Adjust the file paths as needed for your environment

:r .\database\schema\tables.sql
:r .\database\schema\indexes.sql
:r .\database\schema\views.sql
:r .\database\schema\functions.sql
:r .\database\schema\procedures.sql
:r .\database\schema\triggers.sql
:r .\database\schema\security.sql
:r .\database\schema\seed.sql

-- End of batch
