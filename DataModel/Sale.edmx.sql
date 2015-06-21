
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/22/2015 01:25:16
-- Generated from EDMX file: C:\RYurik\Projects\.Net1-Epam-\Checkpoint04\DataModel\Sale.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Sales];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ArticleSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleSets] DROP CONSTRAINT [FK_ArticleSale];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSaleSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleSets] DROP CONSTRAINT [FK_ClientSaleSet];
GO
IF OBJECT_ID(N'[dbo].[FK_FilesLogSaleSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleSets] DROP CONSTRAINT [FK_FilesLogSaleSet];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerSetFilesLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FileLogSets] DROP CONSTRAINT [FK_ManagerSetFilesLog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ArticleSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleSets];
GO
IF OBJECT_ID(N'[dbo].[ClientSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSets];
GO
IF OBJECT_ID(N'[dbo].[FileLogSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileLogSets];
GO
IF OBJECT_ID(N'[dbo].[ManagerSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerSets];
GO
IF OBJECT_ID(N'[dbo].[SaleSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleSets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FileLogs'
CREATE TABLE [dbo].[FileLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [Manager_Id] int  NOT NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [SecondName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Sum] decimal(18,0)  NOT NULL,
    [Article_Id] int  NOT NULL,
    [Client_Id] int  NOT NULL,
    [FileLog_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileLogs'
ALTER TABLE [dbo].[FileLogs]
ADD CONSTRAINT [PK_FileLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Article_Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_ArticleSale]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[Articles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleSale'
CREATE INDEX [IX_FK_ArticleSale]
ON [dbo].[Sales]
    ([Article_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_ClientSaleSet]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSaleSet'
CREATE INDEX [IX_FK_ClientSaleSet]
ON [dbo].[Sales]
    ([Client_Id]);
GO

-- Creating foreign key on [FileLog_Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_FilesLogSaleSet]
    FOREIGN KEY ([FileLog_Id])
    REFERENCES [dbo].[FileLogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FilesLogSaleSet'
CREATE INDEX [IX_FK_FilesLogSaleSet]
ON [dbo].[Sales]
    ([FileLog_Id]);
GO

-- Creating foreign key on [Manager_Id] in table 'FileLogs'
ALTER TABLE [dbo].[FileLogs]
ADD CONSTRAINT [FK_ManagerSetFilesLog]
    FOREIGN KEY ([Manager_Id])
    REFERENCES [dbo].[Managers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerSetFilesLog'
CREATE INDEX [IX_FK_ManagerSetFilesLog]
ON [dbo].[FileLogs]
    ([Manager_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------