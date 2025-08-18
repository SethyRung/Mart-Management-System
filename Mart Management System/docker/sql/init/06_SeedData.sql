USE MartDB;
GO

IF NOT EXISTS (SELECT 1 FROM tbEmployee WHERE EmpID = 'E0001')
BEGIN
INSERT INTO tbEmployee (
  EmpID, EmpKhName, EmpEnName, Sex, DOB, EmpAdd, EmpPos, Salary, Phone, Photo, HireDate, Stopwork
)
VALUES (
         'E0001',
         N'អ្នកគ្រប់គ្រង',
         'Admin User',
         'M',
         '2002-01-01',
         N'Head Office',
         'admin',
         0,
         '01234567',
         0x, -- empty binary for photo
         GETDATE(),
         0
       );
END
GO

IF NOT EXISTS (SELECT 1 FROM tbUser WHERE UserID = 'U0001')
BEGIN
INSERT INTO tbUser (
  UserID, EmpUserName, EmpPassword, EmpID
)
VALUES (
         'U0001',
         'admin',
         'admin',
         'E0001'
       );
END
GO
