USE MartDB;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetUser')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetUser](@ID CHAR(5), @Position VARCHAR(30))
RETURNS @result TABLE
(
	EmpID			CHAR(5)									NOT NULL,
	EmpKhName		NVARCHAR(MAX) COLLATE KHMER_100_BIN		NOT NULL,
	EmpEnName		VARCHAR(MAX)							NOT NULL,
	EmpPos			VARCHAR(30)								NOT NULL,
	EmpUserName		VARCHAR (MAX)							NOT NULL,
    EmpPassword		VARCHAR (MAX)							NOT NULL
)

AS
BEGIN
	IF @Position = ''Admin''
		INSERT INTO @result
			SELECT tbEmployee.EmpID, EmpKhName, EmpEnName, EmpPos, EmpUserName, EmpPassword
				FROM tbEmployee INNER JOIN tbUser ON tbEmployee.EmpID = tbUser.EmpID WHERE tbEmployee.EmpID<>''0''
	ELSE
		INSERT INTO @result
			SELECT tbEmployee.EmpID, EmpKhName, EmpEnName, EmpPos, EmpUserName, EmpPassword
				FROM tbEmployee INNER JOIN tbUser ON tbEmployee.EmpID = tbUser.EmpID WHERE tbEmployee.EmpID=@ID
	RETURN
END
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetSupplier')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetSupplier]()
RETURNS TABLE AS RETURN
(
	SELECT SupID AS [Supplier ID], Supplier, SupAdd AS [Supplier Address], SupContact AS [Supplier Contect] FROM tbSupplier
)
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetCustomer')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetCustomer]()
RETURNS TABLE AS RETURN
(
	SELECT CusID AS [Customer ID], CusKhName AS [Name in Khmer], CusEnName AS [Name in English], CusAdd AS [Customer Address], CusContact AS [Customer Contect] FROM tbCustomer
)
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetEmployee')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetEmployee]()
RETURNS TABLE AS RETURN
(
	SELECT EmpID AS [ID], EmpKhName AS [Khmer Name], EmpEnName AS [English Name] , Sex AS [Sex], DOB AS [Date Of Birth],
			EmpPos AS [Position], Salary, EmpAdd AS [Address], Phone AS [Contact],
			HireDate AS [Hired Date], Photo AS [Photo]
			FROM tbEmployee
	WHERE (stopwork = ''false'' AND EmpID <> ''0'')
)
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetCatagory')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetCatagory]()
RETURNS TABLE AS RETURN
(
	SELECT CatID as [Category ID], Category FROM tbCategory
)
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetNonUser')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetNonUser]()
RETURNS TABLE AS RETURN
(
	SELECT * FROM tbEmployee WHERE EmpID NOT IN (SELECT EmpID FROM tbUser)
)
');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'GetProduct')
BEGIN
EXEC('
CREATE FUNCTION [dbo].[GetProduct]()
RETURNS TABLE AS RETURN
(
	SELECT ProID AS [Product ID], ProName AS [Product Name], ProQty AS [QTY], UPIS AS [Unit Price In Stock], SUP AS [Sale Unit Price], Category
	FROM tbProduct INNER JOIN tbCategory ON tbProduct.CatID = tbCategory.CatID
)
');
END
GO
