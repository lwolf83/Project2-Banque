IF(db_id(N'Project2-Banque') IS NULL)
	BEGIN
		CREATE DATABASE [Project2-Banque]
	END
GO

USE [Project2-Banque]
GO

SET DATEFORMAT ymd
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

IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Transfert'))
BEGIN
	DROP TABLE [Transfert]
END

CREATE TABLE [Customer](
	[idCustomer] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
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
    [isDebitAuthorized] [bit] DEFAULT 0,
	[creationDate] [datetime] DEFAULT getdate(),
	[ceiling] [money] DEFAULT 0,
	[overdraft] [money] DEFAULT -200,
	[savingsRate] [decimal] (2,2) DEFAULT 0.75
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
	[transactionType] [varchar](25) NOT NULL,
	[transactionDate] DATETIME NOT NULL DEFAULT GETDATE(),
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
	[transferDate] DATETIME NOT NULL,
	[isDone] [bit] DEFAULT 0,
	[idTransaction] [int] NOT NULL
	) 
GO

-- Set of test 

INSERT INTO [Customer] (name, login, password, location) VALUES ('laure c' , 'laure', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('elif' , 'elif', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('jean' , 'jean', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('laurent' , 'laurent', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('john' , 'john', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')
INSERT INTO [Customer] (name, login, password, location) VALUES ('bob' , 'bob', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Strasbourg')

INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (1, 'FR1', 500.00, 'SA', 0, '62400', 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (1, 'FR2', 10000.00, 'CA', 0, 0, -200, 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (2, 'FR3', 120.00, 'SA', 0,  62400, 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (2, 'FR4', 134.00, 'CA', 0,  0, -200, 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (3, 'FR5', 120.00, 'SA', 0,  62400, 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (3, 'FR6', 134.00, 'CA', 0,  0, -200, 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (4, 'FR7', 120.00, 'SA', 0,  62400, 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (4, 'FR8', 10.00, 'CA', 0, 0, -200, 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (5, 'FR9', 0.00, 'SA', 0,  62400, 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (5, 'FR10', 90.10, 'CA', 0,  0, -200, 0)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (6, 'FR11', 10.99, 'SA', 0,  62400, 0, 0.01)
INSERT INTO [Account] (idCustomer, accountNumber, amount, type, isDebitAuthorized, ceiling, overdraft, savingsrate) VALUES (6, 'FR12', -12.00, 'CA', 0, 0, -200, 0)

INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (1, 2)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (1, 4)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (3, 6)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 1)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 2)
INSERT INTO [AccountAuthorizedCustomers] (idAccount, idCustomer) VALUES (5, 3)

INSERT INTO [Transaction] (idOriginAccount,idDestinationAccount,amount,transactionType,transactionDate,beginDate,endDate,periodicity) VALUES (1, 2, 10, 'DeferredTransaction','2020-02-22','','','')
INSERT INTO [Transaction] (idOriginAccount,idDestinationAccount,amount,transactionType,transactionDate,beginDate,endDate,periodicity) VALUES (1, 2, 10, 'DeferredTransaction','2020-02-24','','','')
INSERT INTO [Transaction] (idOriginAccount,idDestinationAccount,amount,transactionType,transactionDate,beginDate,endDate,periodicity) VALUES (1, 2, 10, 'DeferredTransaction','2020-02-26','','','')

INSERT INTO [Transfert] (idOriginAccount,idDestinationAccount,amount,transferDate, isDone, idTransaction) VALUES (1, 2, 10, '2020-02-22', 0, 1)
INSERT INTO [Transfert] (idOriginAccount,idDestinationAccount,amount,transferDate, isDone, idTransaction) VALUES (1, 2, 10, '2020-02-24', 0, 2)
INSERT INTO [Transfert] (idOriginAccount,idDestinationAccount,amount,transferDate, isDone, idTransaction) VALUES (1, 2, 10, '2020-02-26', 0, 3)

GO

USE [Project2-Banque]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (1, 1, N'FR1', 500.0000, N'SA', 0, CAST(N'2020-02-27T10:37:01.713' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (2, 1, N'FR2', 10000.0000, N'CA', 0, CAST(N'2020-02-27T10:37:01.713' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (3, 2, N'FR3', 120.0000, N'SA', 0, CAST(N'2020-02-27T10:37:01.717' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (4, 2, N'FR4', 134.0000, N'CA', 0, CAST(N'2020-02-27T10:37:01.733' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (5, 3, N'FR5', 120.0000, N'SA', 1, CAST(N'2020-02-27T10:37:01.733' AS DateTime), 60000.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (6, 3, N'FR6', 134.0000, N'CA', 0, CAST(N'2020-02-27T10:37:01.737' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (7, 4, N'FR7', 120.0000, N'SA', 0, CAST(N'2020-02-27T10:37:01.737' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (8, 4, N'FR8', 10.0000, N'CA', 0, CAST(N'2020-02-27T10:37:01.740' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (9, 5, N'FR9', 0.0000, N'SA', 0, CAST(N'2020-02-27T10:37:01.740' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (10, 5, N'FR10', 90.1000, N'CA', 0, CAST(N'2020-02-27T10:37:01.740' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (11, 6, N'FR11', 10.9900, N'SA', 0, CAST(N'2020-02-27T10:37:01.743' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
INSERT [dbo].[Account] ([idAccount], [idCustomer], [accountNumber], [amount], [type], [isDebitAuthorized], [creationDate], [ceiling], [overdraft], [savingsRate]) VALUES (12, 6, N'FR12', -12.0000, N'CA', 0, CAST(N'2020-02-27T10:37:01.743' AS DateTime), 0.0000, -200.0000, CAST(0.75 AS Decimal(2, 2)))
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (1, 2)
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (1, 4)
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (3, 6)
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (5, 1)
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (5, 2)
GO
INSERT [dbo].[AccountAuthorizedCustomers] ([idAccount], [idCustomer]) VALUES (5, 3)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (1, N'laure c', N'laure', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.700' AS DateTime))
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (2, N'elif', N'elif', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.710' AS DateTime))
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (3, N'jean', N'jean', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.710' AS DateTime))
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (4, N'laurent', N'laurent', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.713' AS DateTime))
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (5, N'john', N'john', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.713' AS DateTime))
GO
INSERT [dbo].[Customer] ([idCustomer], [name], [login], [password], [location], [creationDate]) VALUES (6, N'bob', N'bob', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', N'Strasbourg', CAST(N'2020-02-27T10:37:01.713' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (1, 1, 2, 10.0000, N'deffered', CAST(N'2020-02-22T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (2, 1, 2, 10.0000, N'deffered', CAST(N'2020-02-24T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (3, 1, 2, 10.0000, N'deffered', CAST(N'2020-02-26T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (4, 6, 5, 5.0000, N'DeferredTransaction', CAST(N'2020-02-27T11:18:05.827' AS DateTime), NULL, CAST(N'2020-02-28T00:00:00.000' AS DateTime), CAST(N'2020-02-28T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (5, 6, 5, 5.0000, N'PermanentTransaction', CAST(N'2020-02-27T11:21:43.270' AS DateTime), NULL, CAST(N'2020-01-12T00:00:00.000' AS DateTime), CAST(N'2020-03-13T00:00:00.000' AS DateTime), 10)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (6, 6, 5, 5.0000, N'PermanentTransaction', CAST(N'2020-02-27T11:30:29.013' AS DateTime), NULL, CAST(N'2020-02-28T00:00:00.000' AS DateTime), CAST(N'2020-03-13T00:00:00.000' AS DateTime), 10)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (7, 6, 5, 5.0000, N'InstantTransaction', CAST(N'2020-02-28T09:40:15.450' AS DateTime), NULL, CAST(N'2020-02-28T09:40:15.430' AS DateTime), CAST(N'2020-02-28T09:40:15.430' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (8, 6, 5, 200.0000, N'InstantTransaction', CAST(N'2020-02-28T09:55:01.837' AS DateTime), NULL, CAST(N'2020-02-28T09:55:01.833' AS DateTime), CAST(N'2020-02-28T09:55:01.833' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (9, 5, 6, 10.0000, N'InstantTransaction', CAST(N'2020-02-28T10:23:22.653' AS DateTime), NULL, CAST(N'2020-02-28T10:23:22.640' AS DateTime), CAST(N'2020-02-28T10:23:22.640' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (10, 6, 2, 20.0000, N'InstantTransaction', CAST(N'2020-02-28T10:29:04.837' AS DateTime), NULL, CAST(N'2020-02-28T10:29:04.830' AS DateTime), CAST(N'2020-02-28T10:29:04.830' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (11, 6, 2, 20.0000, N'InstantTransaction', CAST(N'2020-02-28T10:44:17.030' AS DateTime), NULL, CAST(N'2020-02-28T10:44:17.027' AS DateTime), CAST(N'2020-02-28T10:44:17.027' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (12, 5, 2, 20.0000, N'InstantTransaction', CAST(N'2020-02-28T10:49:17.057' AS DateTime), NULL, CAST(N'2020-02-28T10:49:17.050' AS DateTime), CAST(N'2020-02-28T10:49:17.050' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (13, 6, 5, 10.0000, N'DeferredTransaction', CAST(N'2020-02-28T10:55:26.417' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (14, 6, 5, 400.0000, N'DeferredTransaction', CAST(N'2020-02-28T10:57:27.343' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (15, 6, 5, 200.0000, N'DeferredTransaction', CAST(N'2020-02-28T10:58:52.530' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (16, 5, 6, 10.0000, N'DeferredTransaction', CAST(N'2020-02-28T11:00:26.027' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (17, 5, 6, 200.0000, N'DeferredTransaction', CAST(N'2020-02-28T11:02:16.800' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (18, 6, 2, 20.0000, N'DeferredTransaction', CAST(N'2020-02-28T11:03:34.140' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (19, 6, 2, 600.0000, N'DeferredTransaction', CAST(N'2020-02-28T11:04:35.407' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (20, 6, 5, 10.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:21:32.673' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (21, 6, 5, 400.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:22:14.090' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (22, 6, 5, 200.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:23:08.260' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (23, 5, 6, 10.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:24:13.857' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (24, 5, 6, 200.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:24:41.073' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
INSERT [dbo].[Transaction] ([idTransaction], [idOriginAccount], [idDestinationAccount], [amount], [transactionType], [transactionDate], [transferDate], [beginDate], [endDate], [periodicity]) VALUES (25, 6, 2, 20.0000, N'PermanentTransaction', CAST(N'2020-02-28T11:26:15.353' AS DateTime), NULL, CAST(N'2020-03-10T00:00:00.000' AS DateTime), CAST(N'2020-03-12T00:00:00.000' AS DateTime), 5)
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
SET IDENTITY_INSERT [dbo].[Transfert] ON 
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (1, 1, 2, 10.0000, CAST(N'2020-02-22T00:00:00.000' AS DateTime), 0, 1)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (2, 1, 2, 10.0000, CAST(N'2020-02-24T00:00:00.000' AS DateTime), 0, 2)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (3, 1, 2, 10.0000, CAST(N'2020-02-26T00:00:00.000' AS DateTime), 0, 3)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (4, 6, 5, 5.0000, CAST(N'2020-02-27T11:18:05.773' AS DateTime), 0, 4)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (5, 6, 5, 5.0000, CAST(N'2020-01-12T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (6, 6, 5, 5.0000, CAST(N'2020-01-22T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (7, 6, 5, 5.0000, CAST(N'2020-02-01T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (8, 6, 5, 5.0000, CAST(N'2020-02-11T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (9, 6, 5, 5.0000, CAST(N'2020-02-21T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (10, 6, 5, 5.0000, CAST(N'2020-03-02T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (11, 6, 5, 5.0000, CAST(N'2020-03-12T00:00:00.000' AS DateTime), 0, 5)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (12, 6, 5, 5.0000, CAST(N'2020-02-28T00:00:00.000' AS DateTime), 0, 6)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (13, 6, 5, 5.0000, CAST(N'2020-03-09T00:00:00.000' AS DateTime), 0, 6)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (14, 6, 5, 5.0000, CAST(N'2020-02-28T09:40:15.430' AS DateTime), 0, 7)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (15, 6, 5, 200.0000, CAST(N'2020-02-28T09:55:01.833' AS DateTime), 0, 8)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (16, 5, 6, 10.0000, CAST(N'2020-02-28T10:23:22.640' AS DateTime), 0, 9)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (17, 6, 2, 20.0000, CAST(N'2020-02-28T10:29:04.830' AS DateTime), 0, 10)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (18, 6, 2, 20.0000, CAST(N'2020-02-28T10:44:17.027' AS DateTime), 0, 11)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (19, 5, 2, 20.0000, CAST(N'2020-02-28T10:49:17.050' AS DateTime), 0, 12)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (20, 6, 5, 10.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 13)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (21, 6, 5, 400.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 14)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (22, 6, 5, 200.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 15)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (23, 5, 6, 10.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 16)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (24, 5, 6, 200.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 17)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (25, 6, 2, 20.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 18)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (26, 6, 2, 600.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 19)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (27, 6, 5, 10.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 20)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (28, 6, 5, 400.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 21)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (29, 6, 5, 200.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 22)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (30, 5, 6, 10.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 23)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (31, 5, 6, 200.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 24)
GO
INSERT [dbo].[Transfert] ([idTransfert], [idOriginAccount], [idDestinationAccount], [amount], [transferDate], [isDone], [idTransaction]) VALUES (32, 6, 2, 20.0000, CAST(N'2020-03-10T00:00:00.000' AS DateTime), 0, 25)
GO
SET IDENTITY_INSERT [dbo].[Transfert] OFF
GO
