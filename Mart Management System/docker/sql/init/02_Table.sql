USE MartDB;
GO

-- Employee
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbEmployee')
BEGIN
    CREATE TABLE [dbo].[tbEmployee](
        [EmpID] [char](5) NOT NULL,
	    [EmpKhName] [nvarchar](max) NOT NULL,
	    [EmpEnName] [varchar](max) NOT NULL,
	    [Sex] [char](1) NOT NULL,
	    [DOB] [date] NOT NULL,
	    [EmpAdd] [nvarchar](max) NOT NULL,
	    [EmpPos] [varchar](30) NOT NULL,
	    [Salary] [money] NOT NULL,
	    [Phone] [varchar](10) NOT NULL,
	    [Photo] [varbinary](max) NOT NULL,
	    [HireDate] [date] NOT NULL,
	    [Stopwork] [bit] NULL,
        CONSTRAINT [pk_emp] PRIMARY KEY CLUSTERED ([EmpID])
    );
END;

-- Customer
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbCustomer')
BEGIN
    CREATE TABLE [dbo].[tbCustomer](
        [CusID] [int] IDENTITY(1,1) NOT NULL,
	    [CusKhName] [nvarchar](max) NULL,
	    [CusEnName] [varchar](max) NOT NULL,
	    [CusAdd] [nvarchar](max) NOT NULL,
	    [CusContact] [varchar](12) NOT NULL,
        CONSTRAINT [pk_cus] PRIMARY KEY CLUSTERED ([CusID])
    );
END;

-- Supplier
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbSupplier')
BEGIN
    CREATE TABLE [dbo].[tbSupplier](
        [SupID] [int] IDENTITY(1,1) NOT NULL,
		[Supplier] [nvarchar](max) NOT NULL,
		[SupAdd] [nvarchar](max) NOT NULL,
		[SupContact] [nvarchar](max) NOT NULL,
        CONSTRAINT [pk_sup] PRIMARY KEY CLUSTERED ([SupID])
    );
END;

-- Category
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbCategory')
BEGIN
    CREATE TABLE [dbo].[tbCategory](
        [CatID] [varchar](5) NOT NULL,
	    [Category] [nvarchar](max) NOT NULL,
        CONSTRAINT [pk_cat] PRIMARY KEY CLUSTERED ([CatID])
    );
END;

-- User
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbUser')
BEGIN
    CREATE TABLE [dbo].[tbUser](
        [UserID] [char](5) NOT NULL,
	    [EmpUserName] [varchar](max) NOT NULL,
	    [EmpPassword] [varchar](max) NOT NULL,
	    [EmpID] [char](5) NOT NULL,
        CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED ([UserID])
    );
END;

-- Product
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbProduct')
BEGIN
    CREATE TABLE [dbo].[tbProduct](
        [ProID] [varchar](13) NOT NULL,
	    [ProName] [nvarchar](max) NOT NULL,
	    [ProQty] [int] NOT NULL,
	    [UPIS] [money] NOT NULL,
	    [SUP] [money] NOT NULL,
	    [CatID] [varchar](5) NOT NULL,
        CONSTRAINT [pk_pro] PRIMARY KEY CLUSTERED ([ProID])
    );
END;

-- Import
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbImport')
BEGIN
    CREATE TABLE [dbo].[tbImport](
        [ImpID] [int] IDENTITY(1,1) NOT NULL,
	    [ImpDate] [datetime] NOT NULL,
	    [SupID] [int] NOT NULL,
	    [Supplier] [nvarchar](max) NOT NULL,
	    [EmpID] [char](5) NOT NULL,
	    [EmpKhName] [nvarchar](max) NOT NULL,
	    [EmpEnName] [varchar](max) NOT NULL,
	    [ImpTotal] [money] NOT NULL,
        CONSTRAINT [pk_imp] PRIMARY KEY CLUSTERED ([ImpID])
    );
END;

-- ImportDetail
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbImportDetail')
BEGIN
    CREATE TABLE [dbo].[tbImportDetail](
        [ProID] [varchar](13) NOT NULL,
	    [ProName] [nvarchar](max) NOT NULL,
	    [ImpID] [int] NOT NULL,
	    [ImpQty] [int] NOT NULL,
	    [ImpPrice] [money] NOT NULL,
	    [Amount] [money] NOT NULL,
        CONSTRAINT [pk_impDetail] PRIMARY KEY CLUSTERED ([ProID], [ImpID])
    );
END;

-- Sale
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbSale')
BEGIN
    CREATE TABLE [dbo].[tbSale](
        [SaleID] [int] IDENTITY(1,1) NOT NULL,
	    [SaleDate] [datetime] NOT NULL,
	    [CusID] [int] NOT NULL,
	    [CusKhName] [nvarchar](max) NOT NULL,
	    [CusEnName] [varchar](max) NOT NULL,
	    [EmpID] [char](5) NOT NULL,
	    [EmpKhName] [nvarchar](max) NOT NULL,
	    [EmpEnName] [varchar](max) NOT NULL,
	    [SaleTotal] [money] NOT NULL,
        CONSTRAINT [pk_sale] PRIMARY KEY CLUSTERED ([SaleID])
    );
END;

-- SaleDetail
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tbSaleDetail')
BEGIN
    CREATE TABLE [dbo].[tbSaleDetail](
        [ProID] [varchar](13) NOT NULL,
		[ProName] [nvarchar](max) NOT NULL,
		[SaleID] [int] NOT NULL,
		[SaleQty] [int] NOT NULL,
		[SalePrice] [money] NOT NULL,
		[Amount] [money] NOT NULL,
        CONSTRAINT [pk_saleDetail] PRIMARY KEY CLUSTERED ([ProID], [SaleID])
    );
END;

-- 🔗 Foreign Keys
ALTER TABLE [dbo].[tbImport]       ADD CONSTRAINT [fk_imp_emp]         FOREIGN KEY([EmpID]) REFERENCES [dbo].[tbEmployee]([EmpID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbImport]       ADD CONSTRAINT [fk_sup]             FOREIGN KEY([SupID]) REFERENCES [dbo].[tbSupplier]([SupID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbImportDetail] ADD CONSTRAINT [fk_imp]             FOREIGN KEY([ImpID]) REFERENCES [dbo].[tbImport]([ImpID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbImportDetail] ADD CONSTRAINT [fk_pro_impDetail]   FOREIGN KEY([ProID]) REFERENCES [dbo].[tbProduct]([ProID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbProduct]      ADD CONSTRAINT [fk_cat]             FOREIGN KEY([CatID]) REFERENCES [dbo].[tbCategory]([CatID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbSale]         ADD CONSTRAINT [fk_cus]             FOREIGN KEY([CusID]) REFERENCES [dbo].[tbCustomer]([CusID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbSale]         ADD CONSTRAINT [fk_sale_emp]        FOREIGN KEY([EmpID]) REFERENCES [dbo].[tbEmployee]([EmpID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbSaleDetail]   ADD CONSTRAINT [fk_pro_saleDetail]  FOREIGN KEY([ProID]) REFERENCES [dbo].[tbProduct]([ProID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbSaleDetail]   ADD CONSTRAINT [fk_sale]            FOREIGN KEY([SaleID]) REFERENCES [dbo].[tbSale]([SaleID]) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE [dbo].[tbUser]         ADD CONSTRAINT [fk_user]            FOREIGN KEY([EmpID]) REFERENCES [dbo].[tbEmployee]([EmpID]) ON UPDATE CASCADE ON DELETE CASCADE;
