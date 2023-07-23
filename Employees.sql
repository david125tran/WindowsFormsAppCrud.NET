CREATE DATABASE Employees;
GO

USE Employees;
GO

DROP TABLE IF EXISTS Business.Employees
GO

DROP SCHEMA IF EXISTS Business
GO

CREATE SCHEMA Business
GO

CREATE TABLE Business.Employees(
    EmployeeId INT IDENTITY(1, 1) PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Position NVARCHAR(255),
    Salary NVARCHAR(255)
);
GO

-- See server name:
-- SELECT CONCAT(CONVERT(nvarchar(50),SERVERPROPERTY('ServerName')),'.database.windows.net') as FullServerName;