USE MartDB;
GO

IF NOT EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'ImportDetail')
BEGIN
CREATE TYPE [dbo].[ImportDetail] AS TABLE(
  [proID] [varchar](13) NOT NULL,
  [proName] [nvarchar](max) NOT NULL,
  [qty] [int] NOT NULL,
  [upis] [money] NOT NULL,
  [catID] [varchar](5) NOT NULL
)
END
GO

IF NOT EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'ImportMaster')
BEGIN
CREATE TYPE [dbo].[ImportMaster] AS TABLE(
  [impDate] [datetime] NOT NULL,
  [supID] [varchar](5) NOT NULL,
  [supplier] [nvarchar](max) NOT NULL,
  [supAdd] [nvarchar](max) NOT NULL,
  [supCon] [nvarchar](max) NOT NULL,
  [empID] [char](5) NOT NULL,
  [empKhName] [nvarchar](max) NOT NULL,
  [empEnName] [varchar](max) NOT NULL,
  [amount] [money] NOT NULL
  )
END
GO
