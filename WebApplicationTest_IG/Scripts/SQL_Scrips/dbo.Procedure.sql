
CREATE PROCEDURE [dbo].[Database_Init]

	AS 
	DECLARE @name nvarchar(50);
	DECLARE @from int;
	DECLARE @productQuantity int;
	 
	DELETE FROM Products;
	
	SET @name = (N'Товар ');
	SET @from = 1;
	SET @productQuantity = 5000;
	
	WHILE @from <= @productQuantity
	BEGIN
		INSERT INTO [dbo].[Products] ([Name]) 
		VALUES (@Name + CAST(@from as nvarchar(5)))
		SET @from = @from + 1;
	END

