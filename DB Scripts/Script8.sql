use Yanze8250_shrink

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Warehourse')
BEGIN
	CREATE TABLE [dbo].[Warehouse] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] nvarchar(200) NOT NULL,
		[Location] nvarchar(max) NOT NULL,
		[Contact] nvarchar(max) NULL,
		[Photo] nvarchar(max) NULL,

		CONSTRAINT [PK_dbo.Warehouse] PRIMARY KEY CLUSTERED ([Id] ASC)
	);
END

IF COL_LENGTH('dbo.Route', 'WarehouseId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD [WarehouseId] INT NULL

	ALTER TABLE [dbo].[Route] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Route_dbo.Warehouse_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[Warehouse] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Route_WarehouseId] ON [dbo].[Route] ([WarehouseId])
END

IF COL_LENGTH('dbo.Batch', 'IntNumber') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD IntNumber nvarchar(100) NULL
END

IF COL_LENGTH('dbo.Batch', 'IntCarrier') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD IntCarrier nvarchar(40) NULL
END

IF COL_LENGTH('dbo.Batch', 'Duty') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD Duty money NOT NULL DEFAULT(0)
END

IF COL_LENGTH('dbo.Batch', 'StorageCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD StorageCost money NOT NULL DEFAULT(0)
END

IF COL_LENGTH('dbo.Batch', 'Discount') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD Discount money NOT NULL DEFAULT(0)
END

IF COL_LENGTH('dbo.DeliverProgress', 'RouteId') IS NULL
BEGIN
    ALTER TABLE [dbo].[DeliverProgress]
	ADD [RouteId] INT NULL

	-- check route id
	--Update dbo.DeliverProgress Set RouteId = 30

	ALTER TABLE [dbo].[DeliverProgress] ALTER COLUMN [RouteId] INT NOT NULL

	ALTER TABLE [dbo].[DeliverProgress] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.DeliverProgresse_dbo.Route_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.DeliverProgress_RouteId] ON [dbo].[DeliverProgress] ([RouteId])
END

IF COL_LENGTH('dbo.Batch', 'ProgressId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD [ProgressId] INT NULL
		
	ALTER TABLE [dbo].[Batch] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Batch_dbo.DeliverProgress_ProgressId] FOREIGN KEY ([ProgressId]) REFERENCES [dbo].[DeliverProgress] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Batch_ProgressId] ON [dbo].[Batch] ([ProgressId])
END

IF COL_LENGTH('dbo.Batch', 'MasterBatchId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD [MasterBatchId] INT NULL
		
	ALTER TABLE [dbo].[Batch] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Batch_dbo.Batch_MasterBatchId] FOREIGN KEY ([MasterBatchId]) REFERENCES [dbo].[Batch] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Batch_MasterBatchId] ON [dbo].[Batch] ([MasterBatchId])
END

IF COL_LENGTH('dbo.Batch', 'GroupType') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD GroupType INT NOT NULL DEFAULT(1)

	CREATE NONCLUSTERED INDEX [IX_dbo.Batch_GroupType] ON [dbo].[Batch] ([GroupType])
END

IF COL_LENGTH('dbo.Batch', 'WarehouseId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD [WarehouseId] INT NULL
		
	ALTER TABLE [dbo].[Batch] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Batch_dbo.Warehouse_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[Warehouse] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Batch_WarehouseId] ON [dbo].[Batch] ([WarehouseId])
END

IF COL_LENGTH('dbo.Order', 'RouteId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD [RouteId] INT NULL

	ALTER TABLE [dbo].[Order] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Order_dbo.Route_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Order_RouteId] ON [dbo].[Order] ([RouteId])
END

IF COL_LENGTH('dbo.Order', 'Duty') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD Duty money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Route', 'Type4Price') IS NULL
BEGIN
    ALTER TABLE [dbo].[Route]
	ADD Type4Price decimal(16, 2) DEFAULT 0 NOT NULL
END

IF COL_LENGTH('dbo.Order', 'StorageCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD StorageCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Order', 'WarehouseCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD WarehouseCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Order', 'PortMisCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD PortMisCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Order', 'FumigationCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD FumigationCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Order', 'OversizeCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD OversizeCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Order', 'ItemCost') IS NULL
BEGIN
    ALTER TABLE [dbo].[Order]
	ADD ItemCost money NOT NULL default(0)
END

IF COL_LENGTH('dbo.Batch', 'RouteId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Batch]
	ADD [RouteId] INT NULL

	ALTER TABLE [dbo].[Batch] WITH CHECK 
	ADD CONSTRAINT [FK_dbo.Batch_dbo.Route_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Route] ([Id])

	CREATE NONCLUSTERED INDEX [IX_dbo.Batch_RouteId] ON [dbo].[Order] ([RouteId])
END