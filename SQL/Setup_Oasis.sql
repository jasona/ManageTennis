CREATE DATABASE [Oasis]
GO
USE [Oasis]
GO
CREATE TABLE [Page]
(
	[PageId] INT IDENTITY(0,1) NOT NULL
		PRIMARY KEY NONCLUSTERED(PageId),
	[PageName] VARCHAR(128) NOT NULL,
	[Title] VARCHAR(128) NOT NULL,
	[Content] TEXT NOT NULL,
	[Images] VARCHAR(256) NULL,
	[ShowMap] TINYINT NOT NULL
		DEFAULT 0,
	[CreateDate] DATETIME NOT NULL
		DEFAULT GETDATE(),
	[CreatedBy] VARCHAR(256) NOT NULL
)
GO
CREATE CLUSTERED INDEX IDX_Page_PageName
   ON [Page](PageName)
GO
CREATE TABLE [User]
(
	[UserId] INT IDENTITY(0,1) NOT NULL
		PRIMARY KEY NONCLUSTERED(UserId),
	[EmailAddress] VARCHAR(255) NOT NULL,
	[Password] VARCHAR(20) NOT NULL,
	[FirstName] VARCHAR(128) NULL,
	[LastName] VARCHAR(128) NULL,
	[Rating] FLOAT NULL,
	[RegisterDate] DATETIME NOT NULL
		DEFAULT GETDATE(),
	[Status] INT NOT NULL
		DEFAULT 2
)
GO