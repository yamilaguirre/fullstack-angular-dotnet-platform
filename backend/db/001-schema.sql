-- Evalúa practico: schema Clientes / País de origen
-- SQL Server: run on your target database (create DB first if needed).

IF OBJECT_ID(N'dbo.Clientes', N'U') IS NOT NULL
    DROP TABLE dbo.Clientes;
GO

IF OBJECT_ID(N'dbo.Paises', N'U') IS NOT NULL
    DROP TABLE dbo.Paises;
GO

CREATE TABLE dbo.Paises
(
    IdPais INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(120) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.Clientes
(
    IdCliente INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    NombreCompleto NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(32) NOT NULL,
    IdPais INT NOT NULL,
    CONSTRAINT FK_Clientes_Paises_IdPais
        FOREIGN KEY (IdPais) REFERENCES dbo.Paises (IdPais)
);
GO

CREATE NONCLUSTERED INDEX IX_Clientes_IdPais
    ON dbo.Clientes (IdPais);
GO
