
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/23/2011 16:42:33
-- Generated from EDMX file: C:\MyData\FTW\JasonA\OasisClub\branches\razor\Oasis.Lib\OasisModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Oasis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Page]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Page];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Pages'
CREATE TABLE [dbo].[Pages] (
    [PageId] int IDENTITY(1,1) NOT NULL,
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
    [UserId] int IDENTITY(1,1) NOT NULL,
    [EmailAddress] varchar(255)  NOT NULL,
    [Password] varchar(20)  NOT NULL,
    [FirstName] varchar(128)  NULL,
    [LastName] varchar(128)  NULL,
    [Rating] float  NULL,
    [RegisterDate] datetime  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PageId] in table 'Pages'
ALTER TABLE [dbo].[Pages]
ADD CONSTRAINT [PK_Pages]
    PRIMARY KEY CLUSTERED ([PageId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------