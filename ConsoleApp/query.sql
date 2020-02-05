USE [Project2-Banque]
GO

DROP TABLE [dbo].[Customer]
GO
DROP TABLE [dbo].[Account]
GO

CREATE TABLE [dbo].[Customer](
	[idCustomer] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[location] [varchar](50) NULL,
	[creationDate] [datetime] DEFAULT getdate()
  )

  GO
  
CREATE TABLE [dbo].[Account](
	[idAccount] [int] PRIMARY KEY IDENTITY(1,1),
	[idCustomer] [int] NOT NULL,
	[accountNumber] [varchar](250) NOT NULL,
	[amount] [money] NOT NULL,
	[type] [varchar](2) NOT NULL,
	[creationDate] [datetime] DEFAULT getdate()
  )

  GO

  INSERT INTO [Customer] (name, login, password, location) VALUES ('laure c' , 'laure', '1234', 'Strasbourg')
  INSERT INTO [Customer] (name, login, password, location) VALUES ('elif' , 'elif', '1234', 'Strasbourg')
  INSERT INTO [Customer] (name, login, password, location) VALUES ('jean' , 'jean', '1234', 'Strasbourg')
  INSERT INTO [Customer] (name, login, password, location) VALUES ('laurent' , 'laurent', '1234', 'Strasbourg')
  INSERT INTO [Customer] (name, login, password, location) VALUES ('john' , 'john', '1234', 'Strasbourg')
  INSERT INTO [Customer] (name, login, password, location) VALUES ('bob' , 'bob', '1234', 'Strasbourg')

  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (1, 'FR1', 500.00, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (1, 'FR2', 10000.00, 'CA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (2, 'FR3', 120.00, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (2, 'FR4', 134.00, 'CA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (3, 'FR5', 120.00, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (3, 'FR6', 134.00, 'CA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (4, 'FR7', 120.00, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (4, 'FR8', 10.00, 'CA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (5, 'FR9', 0.00, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (5, 'FR10', 90.10, 'CA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (6, 'FR11', 10.99, 'SA')
  INSERT INTO [Account] (idCustomer, accountNumber, amount, type) VALUES (7, 'FR12', -12.00, 'CA')

  GO
