
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2015 19:07:01
-- Generated from EDMX file: C:\RYurik\Projects\.Net1-Epam-\DataModel\Sale.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ArticleSet'
CREATE TABLE [dbo].[ArticleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SaleSet'
CREATE TABLE [dbo].[SaleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] date  NOT NULL,
    [Sum] money  NOT NULL,
    [Customer_Id] int  NOT NULL,
    [Article_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArticleSet'
ALTER TABLE [dbo].[ArticleSet]
ADD CONSTRAINT [PK_ArticleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [PK_SaleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_Id] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [FK_CustomerSale]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerSale'
CREATE INDEX [IX_FK_CustomerSale]
ON [dbo].[SaleSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [Article_Id] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [FK_ArticleSale]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[ArticleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleSale'
CREATE INDEX [IX_FK_ArticleSale]
ON [dbo].[SaleSet]
    ([Article_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------