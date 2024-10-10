CREATE DATABASE IF NOT EXISTS dbaderezosweb;

USE dbaderezosweb;

CREATE TABLE IF NOT EXISTS Clients (
	Id VARCHAR(36) PRIMARY KEY NOT NULL,
  ClientName VARCHAR(80),
  ClientEmail VARCHAR(80),
  ClientPhone INT
);