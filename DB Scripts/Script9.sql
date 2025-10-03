use yanze080520
--use yanze0324

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'BatchBox')
BEGIN
	CREATE TABLE [dbo].[BatchBox] (
		[Id] INT IDENTITY(1,1) NOT NULL,
		[Number] INT NOT NULL,
		[BatchId] INT NOT NULL

		CONSTRAINT [PK_dbo.BatchBox] PRIMARY KEY CLUSTERED ([Id] ASC)
	);

	ALTER TABLE [dbo].[BatchBox] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.BatchBox_dbo.BatchId_BatchId] FOREIGN KEY ([BatchId]) REFERENCES [dbo].[Batch] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.BatchBox_BatchId] ON [dbo].[BatchBox] ([BatchId])
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'BatchBoxOrderMap')
BEGIN
	CREATE TABLE [dbo].[BatchBoxOrderMap] (
		[OrderId] INT NOT NULL,
		[BatchBoxId] INT NOT NULL

		CONSTRAINT [PK_dbo.BatchBoxOrderMap] PRIMARY KEY CLUSTERED ([BatchBoxId], [OrderId])
	);

	ALTER TABLE [dbo].[BatchBoxOrderMap] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.BatchBoxOrderMap_dbo.Batch_BatchId] FOREIGN KEY ([BatchBoxId]) REFERENCES [dbo].[BatchBox] ([Id])

	ALTER TABLE [dbo].[BatchBoxOrderMap] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.BatchBoxOrderMap_dbo.Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.BatchBoxOrderMap_BatchId] ON [dbo].[BatchBoxOrderMap] ([BatchBoxId])

	CREATE NONCLUSTERED INDEX [IX_dbo.BatchBoxOrderMap_OrderId] ON [dbo].[BatchBoxOrderMap] ([OrderId])
END

IF COL_LENGTH('dbo.Route', 'SupportWechat') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD SupportWechat nvarchar(50)
END

IF COL_LENGTH('dbo.Order', 'DistrictAdditionalCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD DistrictAdditionalCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Batch', 'InsuranceFee') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD InsuranceFee money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Batch', 'HeBaoCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD HeBaoCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Batch', 'TotalExpense') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD TotalExpense money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Batch', 'Stage') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD Stage int NOT NULL default(0)
END

IF COL_LENGTH('dbo.Warehouse', 'Photo') IS NULL
BEGIN
    ALTER TABLE [dbo].[Warehouse]
	ADD Photo nvarchar(max) NULL 
END

IF COL_LENGTH('dbo.Route', 'Photo') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD Photo nvarchar(max) NULL 
END

IF COL_LENGTH('dbo.Route', 'Description') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD [Description] nvarchar(max) NULL 
END

IF COL_LENGTH('dbo.Route', 'SupportDescription') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD SupportDescription nvarchar(max)
END

IF COL_LENGTH('dbo.Batch', 'TargetWeightKg') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD TargetWeightKg decimal(16, 2) NULL
END

IF COL_LENGTH('dbo.Route', 'DisplaySequence') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD DisplaySequence int
END

IF COL_LENGTH('dbo.Warehouse', 'DisplaySequence') IS NULL
BEGIN
    ALTER TABLE [dbo].[Warehouse]
	ADD DisplaySequence int
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SystemPhoto')
BEGIN
	CREATE TABLE [dbo].[SystemPhoto] (
		[Id] INT IDENTITY(1,1) NOT NULL,
		[Type] INT NOT NULL DEFAULT(1),
		[Url] nvarchar(max) NOT NULL

		CONSTRAINT [PK_dbo.SystemPhoto] PRIMARY KEY CLUSTERED ([Id] ASC)
	);
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'OrderActionHistory')
BEGIN
	CREATE TABLE [dbo].[OrderActionHistory] (
		[Id] INT IDENTITY(1,1) NOT NULL,
		[OrderId] INT NOT NULL,
		[UserId] INT NOT NULL,
		[Date] DATETIME NOT NULL,
		[Description] NVARCHAR(MAX)

		CONSTRAINT [PK_dbo.OrderActionHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
	);

	ALTER TABLE [dbo].[OrderActionHistory] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.OrderActionHistory_dbo.Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.OrderActionHistory_OrderId] ON [dbo].[OrderActionHistory] ([OrderId])

	ALTER TABLE [dbo].[OrderActionHistory] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.OrderActionHistory_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.OrderActionHistory_UserId] ON [dbo].[OrderActionHistory] ([UserId])
END

IF COL_LENGTH('dbo.User', 'PickUpPhoneNumber') IS NULL
BEGIN
    ALTER TABLE [dbo].[User]
	ADD PickUpPhoneNumber nvarchar(50)
END

IF COL_LENGTH('dbo.User', 'PickUpAddress') IS NULL
BEGIN
    ALTER TABLE [dbo].[User]
	ADD PickUpAddress nvarchar(max)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'BannedUserRoute')
BEGIN
	CREATE TABLE [dbo].[BannedUserRoute] (		
		[UserId] INT NOT NULL,
		[RouteId] INT NOT NULL,

		CONSTRAINT [PK_dbo.BannedUserRoute] PRIMARY KEY CLUSTERED ([UserId], [RouteId])
	);

	ALTER TABLE [dbo].[BannedUserRoute] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.BannedUserRoute_dbo.Route_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.BannedUserRoute_RouteId] ON [dbo].[BannedUserRoute] ([RouteId])

	ALTER TABLE [dbo].[BannedUserRoute] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.BannedUserRoute_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.BannedUserRoute_UserId] ON [dbo].[BannedUserRoute] ([UserId])
END

IF COL_LENGTH('dbo.Order', 'ActionReason') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD ActionReason nvarchar(max)
END

