USE MartDB;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProCategory')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProCategory]
	(@Type varchar(20),
	@CatID varchar(5),
	@Category nvarchar(max))
AS

BEGIN
	IF(@Type=''insert'')
		INSERT INTO tbCategory VALUES(@CatID, @Category)
	ELSE IF(@Type=''update'')
		UPDATE tbCategory
		SET Category = @Category
		WHERE CatID = @CatID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProCustomer')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProCustomer]
	(@Type varchar(20),
	@CusID int,
	@CusEnName varchar(max),
	@CusAddress nvarchar(max),
	@CusContect nvarchar(max))
AS

BEGIN
	IF(@Type=''insert'')
		INSERT INTO tbCustomer (CusEnName,CusAdd,CusContact) VALUES(@CusEnName, @CusAddress, @CusContect)
	ELSE IF(@Type=''update'')
		UPDATE tbCustomer
		SET CusEnName = @CusEnName,
			CusAdd = @CusAddress,
			CusContact = @CusContect
		WHERE CusID = @CusID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProEmployee')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProEmployee]
	(@Type varchar(20),
	@ID char(5),
	@KName nvarchar(max),
	@EName varchar(max),
	@Sex char,
	@DOB date,
	@Address nvarchar(max),
	@Position varchar(30),
	@Salary money,
	@Phone varchar(10),
	@Photo varbinary(max),
	@Hire date)
AS

BEGIN
	IF(@Type=''insert'')
		INSERT INTO tbEmployee VALUES(@ID, @KName, @EName, @Sex, @DOB, @Address, @Position, @Salary, @Phone, @Photo, @Hire, ''false'')
	ELSE IF(@Type=''update'')
UPDATE tbEmployee
SET EmpKhName = @KName,
    EmpEnName = @EName,
    Sex = @Sex,
    DOB = @DOB,
    EmpAdd = @Address,
    EmpPos = @Position,
    Salary = @Salary,
    Phone = @Phone,
    Photo = @Photo,
    HireDate = @Hire,
    Stopwork = ''false''
WHERE EmpID = @ID
  ELSE IF(@Type=''delete'')
UPDATE tbEmployee
SET Stopwork = ''true''
WHERE EmpID = @ID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProImport')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProImport](@isNewSupplier char, @IM as ImportMaster READONLY, @ID as ImportDetail READONLY)
AS
BEGIN
	DECLARE @supID varchar(5)
	DECLARE @impID INT

	SELECT @supID = supID FROM @IM

	IF(@isNewSupplier = ''1'')
		BEGIN
			BEGIN
				INSERT INTO tbSupplier (Supplier, SupAdd, SupContact) VALUES(Supplier, SupAdd, SupContact)
				SELECT supID, supplier, supAdd, supCon FROM @IM

				INSERT INTO tbImport(ImpDate, SupID, Supplier, EmpID, EmpKhName, EmpEnName, ImpTotal)
				SELECT impDate, supID, supplier, empID, empKhName, empEnName, amount FROM @IM
			END
		END

	ELSE
		BEGIN
			INSERT INTO tbImport(ImpDate, SupID, Supplier, EmpID, EmpKhName, EmpEnName, ImpTotal)
			SELECT impDate, supID, supplier, empId, empKhName, empEnName, amount FROM @IM
		END


	SELECT @impID = MAX(impID) FROM tbImport

	DECLARE @proID VARCHAR(13)
	DECLARE @proName NVARCHAR(MAX)
	DECLARE @qty INT
	DECLARE @upis MONEY
	DECLARE @sup MONEY
	DECLARE @catID varchar(5)

	DECLARE csDetail CURSOR SCROLL DYNAMIC
	FOR SELECT * FROM @ID

	OPEN csDetail

	FETCH FIRST FROM csDetail INTO @proID, @proName, @qty, @upis, @catID
	WHILE(@@FETCH_STATUS=0)
		BEGIN
			IF((SELECT COUNT(proID) FROM tbProduct WHERE proID=@proID)=0)
				BEGIN
					INSERT INTO tbProduct
					VALUES(@proID, @proName, @qty, @upis, @upis+2, @catID)
				END
			ELSE
				BEGIN
					UPDATE tbProduct
					SET ProQty=ProQty+@qty, upis=@upis
					WHERE proID=@proID
				END
			INSERT INTO tbImportDetail
			VALUES(@proID, @proName, @impID, @qty, @upis, @upis*@qty)

			FETCH NEXT FROM csDetail INTO @proID, @proName, @qty, @upis, @catID
		END
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProProduct')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProProduct]
	(@Type varchar(20),
	@ProID varchar(13),
	@ProName nvarchar(max),
	@ProQty int,
	@UPIS money,
	@SUP money,
	@CatID varchar(5))
AS

BEGIN
	IF(@Type=''update'')
		UPDATE tbProduct
		SET ProName = @ProName,
			ProQty = @ProQty,
			UPIS = @UPIS,
			SUP = @SUP,
			CatID = @CatID
		WHERE ProID = @ProID
	ELSE IF(@Type=''delete'')
		DELETE tbProduct WHERE ProID=@ProID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSearchCategory')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSearchCategory](@s NVARCHAR(MAX))
AS

BEGIN

	IF((SELECT COUNT(CatID) FROM tbCategory WHERE Category LIKE N''%''+@s+''%'')>0)
			SELECT CatID as [Category ID], Category FROM tbCategory WHERE Category LIKE N''%''+@s+''%''
	ELSE
		SELECT CatID as [Category ID], Category FROM tbCategory WHERE CatID LIKE ''%''+@s+''%''
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSearchCustomer')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSearchCustomer](@s NVARCHAR(MAX))
AS

BEGIN

	IF((SELECT COUNT(CusID) FROM tbCustomer WHERE (CusKhName LIKE N''%''+@s+''%''))>0)
			SELECT CusID AS [Customer ID], CusKhName AS [Name in Khmer], CusEnName AS [Name is English], CusAdd AS [Customer Address], CusContact AS [Customer Contect]
			FROM tbCustomer WHERE (CusKhName LIKE N''%''+@s+''%'')

	ELSE IF((SELECT COUNT(CusID) FROM tbCustomer WHERE (CusID LIKE ''%''+@s+''%''))>0)
		SELECT CusID AS [Customer ID], CusKhName AS [Name in Khmer], CusEnName AS [Name is English], CusAdd AS [Customer Address], CusContact AS [Customer Contect]
			FROM tbCustomer WHERE (CusID LIKE ''%''+@s+''%'')

	ELSE
		SELECT CusID AS [Customer ID], CusKhName AS [Name in Khmer], CusEnName AS [Name is English], CusAdd AS [Customer Address], CusContact AS [Customer Contect]
			FROM tbCustomer WHERE (CusEnName LIKE ''%''+@s+''%'')
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSearchEmployee')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSearchEmployee](@s NVARCHAR(MAX))
AS

BEGIN

	IF((SELECT COUNT(EmpID) FROM tbEmployee WHERE (stopwork = ''false'' AND EmpKhName LIKE N''%''+@s+''%''))>0)
			SELECT EmpID AS [ID], EmpKhName AS [Khmer Name], EmpEnName AS [English Name] , Sex AS [Sex], DOB AS [Date Of Birth],
				EmpPos AS [Position], Salary, EmpAdd AS [Address], Phone AS [Contact],
				HireDate AS [Hired Date], Photo AS [Photo]
				FROM tbEmployee WHERE (stopwork = ''false'' AND EmpKhName LIKE N''%''+@s+''%'')

	ELSE IF((SELECT COUNT(EmpID) FROM tbEmployee WHERE (stopwork = ''false'' AND EmpID LIKE ''%''+@s+''%''))>0)
		SELECT EmpID AS [ID], EmpKhName AS [Khmer Name], EmpEnName AS [English Name] , Sex AS [Sex], DOB AS [Date Of Birth],
			EmpPos AS [Position], Salary, EmpAdd AS [Address], Phone AS [Contact],
			HireDate AS [Hired Date], Photo AS [Photo]
			FROM tbEmployee WHERE (stopwork = ''false'' AND EmpID LIKE ''%''+@s+''%'')

	ELSE
		SELECT EmpID AS [ID], EmpKhName AS [Khmer Name], EmpEnName AS [English Name] , Sex AS [Sex], DOB AS [Date Of Birth],
				EmpPos AS [Position], Salary, EmpAdd AS [Address], Phone AS [Contact],
				HireDate AS [Hired Date], Photo AS [Photo]
				FROM tbEmployee WHERE (stopwork = ''false'' AND EmpEnName LIKE ''%''+@s+''%'')
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSearchProduct')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSearchProduct](@s NVARCHAR(MAX))
AS

BEGIN

	IF((SELECT COUNT(ProID) FROM tbProduct WHERE ProName LIKE N''%''+@s+''%'')>0)
			SELECT ProID AS [Product ID], ProName AS [Product Name], ProQty AS [QTY], UPIS AS [Unit Price In Stock], SUP AS [Sale Unit Price], Category
			FROM tbProduct INNER JOIN tbCategory ON tbProduct.CatID = tbCategory.CatID WHERE ProName LIKE N''%''+@s+''%''
	ELSE
		SELECT ProID AS [Product ID], ProName AS [Product Name], ProQty AS [QTY], UPIS AS [Unit Price In Stock], SUP AS [Sale Unit Price], Category
		FROM tbProduct INNER JOIN tbCategory ON tbProduct.CatID = tbCategory.CatID WHERE ProID LIKE ''%''+@s+''%''
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSearchSupplier')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSearchSupplier](@s NVARCHAR(MAX))
AS

BEGIN

	IF((SELECT COUNT(SupID) FROM tbSupplier WHERE Supplier LIKE N''%''+@s+''%'')>0)
			SELECT SupID AS [Supplier ID], Supplier, SupAdd AS [Supplier Address], SupContact AS [Supplier Contect] FROM tbSupplier WHERE Supplier LIKE N''%''+@s+''%''
	ELSE
		SELECT SupID AS [Supplier ID], Supplier, SupAdd AS [Supplier Address], SupContact AS [Supplier Contect] FROM tbSupplier WHERE SupID LIKE ''%''+@s+''%''
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSupplier')
BEGIN
EXEC('
Create PROCEDURE [dbo].[ProSupplier]
	(@Type varchar(20),
	@SupID int,
	@Supplier nvarchar(max),
	@SupAddress nvarchar(max),
	@SupContect nvarchar(max))
AS

BEGIN
	IF(@Type=''insert'')
		INSERT INTO tbSupplier (Supplier, SupAdd, SupContact) VALUES(@Supplier, @SupAddress, @SupContect)
	ELSE IF(@Type=''update'')
		UPDATE tbSupplier
		SET Supplier = @Supplier,
			SupAdd = @SupAddress,
			SupContact = @SupContect
		WHERE SupID = @SupID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProUser')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProUser]
	(@Type varchar(20),
	@ID char(5),
	@UserName varchar (max),
    @Password varchar (max))
AS

BEGIN

	DECLARE @UID char(5)
	SET @UID = ''U''+@ID
	IF(@Type=''insert'')
		INSERT INTO tbUser VALUES(@UID,@UserName,@Password,@ID)
	ELSE IF(@Type=''update'')
		UPDATE tbUser
		SET EmpUserName = @UserName,
			EmpPassword = @Password
		WHERE UserID = @UID
	ELSE IF(@Type=''delete'')
		DELETE FROM tbUSER
		WHERE UserID = @UID
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProSale')
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[ProSale](@isNewCustomer char, @SM as SaleMaster READONLY, @SD as SaleDetail READONLY)
AS
BEGIN
	DECLARE @cusID INT 
	DECLARE @saleID INT

	SELECT @cusID = cusID FROM @SM

	IF(@isNewCustomer = '1')
		BEGIN
			INSERT INTO tbCustomer(CusEnName, CusAdd, CusContact)
				SELECT cusName, cusAdd, cusCon FROM @SM
			
			SELECT @cusID=MAX(CusID) FROM tbCustomer

			INSERT INTO tbSale(SaleDate, CusID, CusEnName, EmpID, EmpKhName, EmpEnName, SaleTotal)
				SELECT saleDate, cusID, cusName, empID, empKhName, empEnName, amount FROM @SM
		END

	ELSE
		BEGIN
			INSERT INTO tbSale(SaleDate, CusID, CusEnName, EmpID, EmpKhName, EmpEnName, SaleTotal)
				SELECT saleDate, cusID, cusName, empID, empKhName, empEnName, amount FROM @SM
		END

	SELECT @saleID = MAX(SaleID) FROM tbSale

	DECLARE @proID VARCHAR(13)
	DECLARE @proName NVARCHAR(MAX)
	DECLARE @qty INT
	DECLARE @qtyInStock INT
	DECLARE @sup MONEY
	DECLARE @catID varchar(5)

	DECLARE csDetail CURSOR SCROLL DYNAMIC
	FOR SELECT * FROM @SD

	OPEN csDetail

	FETCH FIRST FROM csDetail INTO @proID, @proName, @qty, @sup, @catID
	WHILE(@@FETCH_STATUS=0)
		BEGIN
			IF((SELECT COUNT(proID) FROM tbProduct WHERE proID=@proID)=0)
				THROW 60001,'We do not have this product in stock',1
			ELSE
				BEGIN
					SELECT @qtyInStock=ProQty FROM tbProduct
					IF((@qtyInStock-@qty)<0)
						THROW 60001,'Not enoust product in stock',1
					ELSE
						UPDATE tbProduct 
						SET ProQty=ProQty-@qty
						WHERE ProID=@proID
				END
			INSERT INTO tbSaleDetail
			VALUES(@proID, @proName, @saleID, @qty, @sup, @sup*@qty)
			
			FETCH NEXT FROM csDetail INTO @proID, @proName, @qty, @sup, @catID
		END
END
');
END
GO