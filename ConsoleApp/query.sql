IF(db_id(N'Project2-Banque') IS NULL)
	BEGIN
		CREATE DATABASE [Project2-Banque]
	END
GO

USE [Project2-Banque]
GO

IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customer'))
BEGIN
	DROP TABLE [Customer]
END

IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Account'))
BEGIN
	DROP TABLE [Account]
END

IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccountAuthorizedCustomers'))
BEGIN
	DROP TABLE [AccountAuthorizedCustomers]
END

IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Transaction'))
BEGIN
	DROP TABLE [Transaction]
END

CREATE TABLE [Customer](
	[idCustomer] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[location] [varchar](50) NULL,
	[creationDate] [datetime] DEFAULT getdate()
  )
GO
  
CREATE TABLE [Account](
	[idAccount] [int] PRIMARY KEY IDENTITY(1,1),
	[idCustomer] [int] NOT NULL,
	[accountNumber] [varchar](250) NOT NULL,
	[amount] [money] NOT NULL,
	[type] [varchar](2) NOT NULL,
    [isDebitAuthorized] [bit] NOT NULL,
	[creationDate] [datetime] DEFAULT getdate()
  )
GO

CREATE TABLE [AccountAuthorizedCustomers](
	[idAccount] [int] NOT NULL,
	[idCustomer] [int] NULL
) ON [PRIMARY]
GO

CREATE TABLE [Transaction](
	[idTransaction] [int] PRIMARY KEY IDENTITY(1,1),
	[idOriginAccount] [int] NOT NULL,
	[idDestinationAccount] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[transactionType] [VARCHAR](10) NOT NULL,
	[creationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[transactionDate] DATETIME NOT NULL,
	[beginDate] DATETIME NULL,
	[endDate] DATETIME NULL,
	[periodicity] INT NULL
) 
GO

CREATE TABLE [Transfert](
	[idTransfert] [int] PRIMARY KEY IDENTITY(1,1),
	[idOriginAccount] [int] NOT NULL, 
	[idDestinationAccount] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[transferDate] DATETIME NOT NULL
	) 
GO



-- Set of test 

INSERT INTO [Customer] (name, login, password, location) VALUES ('laure c' , 'laure', '1234', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('elif' , 'elif', '1234', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('jean' , 'jean', '1234', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('laurent' , 'laurent', '1234', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('john' , 'john', '1234', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('bob' , 'bob', '1234', 'Strasbourg')

INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (1, 'FR1', 500.00, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (1, 'FR2', 10000.00, 'CA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (2, 'FR3', 120.00, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (2, 'FR4', 134.00, 'CA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (3, 'FR5', 120.00, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (3, 'FR6', 134.00, 'CA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (4, 'FR7', 120.00, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (4, 'FR8', 10.00, 'CA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (5, 'FR9', 0.00, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (5, 'FR10', 90.10, 'CA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (6, 'FR11', 10.99, 'SA', 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized) VALUES (6, 'FR12', -12.00, 'CA', 0)

INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (1, 2)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (1, 4)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (3, 6)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 1)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 2)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 3)

GO
