CREATE TRIGGER SETClientsStatusForInsert
	ON Orders
	FOR DELETE
	AS
	
	DECLARE @clientId int;
	DECLARE @quantity int;
	DECLARE @ordinaryStatus nvarchar(10);
	DECLARE @averageStatus nvarchar(10);
	DECLARE @topStatus nvarchar(10);
	DECLARE @VIPStatus nvarchar(10);

	SET @ordinaryStatus = (N'Обычный');
	SET @averageStatus = (N'Средний');
	SET @topStatus = (N'Топ');
	SET @VIPStatus = (N'ВИП');

	SELECT @clientId = ClientId  FROM inserted;
	SELECT @quantity = Count(*)  FROM Orders WHERE ClientId = @clientId;

	IF(@quantity <= 5)
	BEGIN
		UPDATE Clients SET Category = @ordinaryStatus WHERE ClientId = @clientId;
	END

	IF(@quantity > 5 AND @quantity <= 30)
	BEGIN
		UPDATE Clients SET Category = @averageStatus WHERE ClientId = @clientId;
	END

	IF(@quantity > 30 AND @quantity <= 40)
	BEGIN
		UPDATE Clients SET Category = @topStatus WHERE ClientId = @clientId;
	END

	IF(@quantity > 40)
	BEGIN
		UPDATE Clients SET Category = @VIPStatus WHERE ClientId = @clientId;
	END
