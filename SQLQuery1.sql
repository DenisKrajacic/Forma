CREATE DATABASE Forma;
USE Forma;

CREATE TABLE Filmovi(
  id INT IDENTITY(1, 1),
  naziv NVARCHAR(50) NOT NULL,
  trajanje NVARCHAR(50) NOT NULL,
  zanr NVARCHAR(50) NOT NULL,
  naziv_originala NVARCHAR(50) NOT NULL,
  zemlja NVARCHAR(50) NOT NULL,
);