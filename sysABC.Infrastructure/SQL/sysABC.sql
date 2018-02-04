CREATE DATABASE sysABC

USE sysABC

CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    Salt NVARCHAR(200) NOT NULL,
    NickName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(10) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL
)


-- sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=sysABC!strongSQLpa$$' -p 1401:1433 --name sqlsysabc -d microsoft/mssql-server-linux:2017-latest
-- Password:
-- 7fb609534f236dc187dac7866be6c0d3a8abeb65961884e8083271da4c14019e
-- MSSQL_SA_PASSWORD=sysABC!strongSQLpa$$