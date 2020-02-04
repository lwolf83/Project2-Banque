USE [Project2-Banque]
GO

CREATE TABLE [dbo].[Customer](
	[idCustomer] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[location] [varchar](50) NULL,
	[creationDate] [datetime] DEFAULT getdate()
  )
  
  
CREATE TABLE [dbo].[Account](
	[idAccount] [int] PRIMARY KEY IDENTITY(1,1),
	[idCustomer] [int] NOT NULL,
	[accountNumber] [varchar](250) NOT NULL,
	[amount] [money] NOT NULL,
	[type] [varchar](2) NOT NULL,
	[creationDate] [datetime] NULL
  )
