

USE IGSTORE_c2f90a21cafb4466822e3d7db2d8ca1b
GO

/****** Object:  StoredProcedure [dbo].[Database_Init1]    Script Date: 03.10.2017 11:08:47 ******/
DROP PROCEDURE [dbo].[Database_Init]
GO

/****** Object:  StoredProcedure [dbo].[Database_Init1]    Script Date: 03.10.2017 11:08:47 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO


CREATE PROCEDURE [dbo].[Database_Init]

	AS 
	DECLARE @counter int;
	DECLARE @productName nvarchar(50);
	DECLARE @productsQuantity int;

	DECLARE @clientsQuantity int;
	DECLARE @clientName nvarchar(50);
	DECLARE @clientAddress nvarchar(50);
	DECLARE @clientCategory nvarchar(10);
	Declare @currentClientId int;
    Declare @minOrderQuantity int;
	Declare @maxOrderQuantity int;
	Declare @CurrentOrderQuantity int;
	Declare @CurrentOrderRowsQuantity int;
	Declare @CurrentProductsQuantity int;
	DECLARE @currentOrderNumber int;
	DECLARE @currentOrderId int;
	DECLARE @currentOrderRowNumber int;
	DECLARE @randomProductId int;
	DECLARE @randomPrice float;
	DECLARE @maxOrderRows int;
	DECLARE @maxProductsInOrder int;
	DECLARE @maxPrice int;
	 
	DELETE FROM Products;
	DELETE FROM Clients;
	DELETE FROM Orders;
	DELETE FROM OrderRows;
	
	SET @productName = (N'Товар ');
	SET @counter = 1;
	SET @productsQuantity = 5000;

	SET @clientsQuantity = 100;
	SET @clientName =  N'Клиент ';
	SET @clientAddress = N'Днепр';
	SET @clientCategory = N'Обычный';
	SET @minOrderQuantity = 5;
	SET @maxOrderQuantity = 50;
	SET @maxOrderRows = 100;
	SET @maxProductsInOrder = 100;
	SET @maxPrice = 100;
	
	WHILE @counter <= @productsQuantity
	BEGIN
		INSERT INTO [dbo].[Products] ([Name]) 
		VALUES (@productName + CAST(@counter as nvarchar(5)))
		SET @counter = @counter + 1;

	END;
	SET @counter = 1;
	WHILE @counter <= @clientsQuantity
	BEGIN
		INSERT INTO [dbo].[Clients] ([Name],[Adress],[Category]) 
		VALUES ((@clientName + CAST(@counter as nvarchar(5))), (@clientAddress), (@clientCategory))

		SET @currentClientId = (SELECT IDENT_CURRENT('Clients'));
		SET @CurrentOrderQuantity = FLOOR(RAND()*(@maxOrderQuantity-@minOrderQuantity+1)+@minOrderQuantity);
		SET @currentOrderNumber =1;
		WHILE @currentOrderNumber <= @CurrentOrderQuantity
		BEGIN
			INSERT INTO Orders (ClientId, Date) VALUES (@currentClientId,GETDATE());
			SET @currentOrderId = (SELECT IDENT_CURRENT('Clients'));
			SET @CurrentOrderRowsQuantity = FLOOR(RAND()*(@maxOrderRows)+1);
			SET @currentOrderRowNumber =1;
			
			WHILE @currentOrderRowNumber <= @CurrentOrderRowsQuantity
			BEGIN
				SET @randomProductId = FLOOR(RAND()*(@productsQuantity)+1);
				SET @randomPrice = ROUND(Rand()*@maxPrice,2);
				SET @CurrentProductsQuantity = FLOOR(RAND()*(@productsQuantity)+1);

				INSERT INTO [dbo].[OrderRows] ([OrderId],[ProductId],[Price],[Quantity],[Sum]) 
				VALUES (@currentOrderId, @randomProductId, @randomPrice,@CurrentProductsQuantity,Round(@randomPrice*@CurrentProductsQuantity, 2));
				SET @currentOrderRowNumber = @currentOrderRowNumber +1;
			END 
			SET @currentOrderNumber = @currentOrderNumber +1;		
		END
		SET @counter = @counter + 1;
	END;	
GO
DECLARE @RC int;
EXECUTE @RC = [dbo].[Database_Init]; 
GO


