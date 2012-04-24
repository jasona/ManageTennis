
SET QUOTED_IDENTIFIER OFF;
GO
USE [OasisTennis];
GO


IF OBJECT_ID(N'[dbo].[Pages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pages];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO

-- Creating table 'Pages'
CREATE TABLE [dbo].[Pages] (
    [PageId] int IDENTITY(1,1) NOT NULL
		PRIMARY KEY NONCLUSTERED(PageId),
	[ParentPageId] INT NULL,
    [PageName] varchar(128)  NOT NULL,
    [Title] varchar(128)  NOT NULL,
    [Content] varchar(max)  NOT NULL,
    [Images] varchar(256)  NULL,
    [ShowMap] tinyint  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreatedBy] varchar(256)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL
		PRIMARY KEY NONCLUSTERED(UserId),
    [EmailAddress] varchar(255)  NOT NULL,
    [Password] varchar(20)  NOT NULL,
    [FirstName] varchar(128)  NULL,
    [LastName] varchar(128)  NULL,
    [Rating] float  NULL,
    [RegisterDate] datetime  NOT NULL,
    [Status] int  NOT NULL
);
GO

CREATE TABLE [Posts]
(
	PostId INT IDENTITY(1,1) NOT NULL
		PRIMARY KEY NONCLUSTERED(PostId),
	Title VARCHAR(255) NOT NULL,
	TitleEncoded VARCHAR(255) NOT NULL,
	Body TEXT NOT NULL,
	PostDate DATETIME NOT NULL
		DEFAULT GETDATE(),
	Category VARCHAR(255) NULL
)
GO

