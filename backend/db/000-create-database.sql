-- Run this script connected to master (Default / system database).
-- Creates EvaluaClientes before running 001–003 against that database.

IF DB_ID(N'EvaluaClientes') IS NULL
BEGIN
    CREATE DATABASE EvaluaClientes;
END;
GO
