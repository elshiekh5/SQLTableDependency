USE [SQLTableDependency ] 
GO 
/********************************
Performance test 
add 1000 row 
********************************/
Declare @INDEX Int = 0 ;
WHILE	@INDEX <1000
BEGIN 
INSERT INTO dbo.Customers
(
    [Name],
    Surname
)
VALUES(  N'Name '+CAST(@INDEX AS NVARCHAR(3)), N'SureName  '+CAST(@INDEX AS NVARCHAR(3))  );
SET @INDEX = @INDEX+1 
PRINT (FORMAT(getdate(), 'yyyy-MM-dd HH:mm:ss.fff'))
END 
/********************************/