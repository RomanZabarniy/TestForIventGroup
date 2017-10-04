using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models
{
    public class StoreDbInitializer : DropCreateDatabaseAlways<StoreContext>
    {
        protected override void Seed(StoreContext db)
        {
            #region add triggers
            var addInsertTrigger = db.Database.ExecuteSqlCommand("CREATE TRIGGER SetClientsStatusForInsert " +
                "ON Orders " +
                "FOR INSERT " +
                "AS " +
                "DECLARE @clientId int;" +
                "DECLARE @quantity int;" +
                "DECLARE @ordinaryStatus nvarchar(10); " +
                "DECLARE @averageStatus nvarchar(10); " +
                "DECLARE @topStatus nvarchar(10);" +
                "DECLARE @VIPStatus nvarchar(10);" +
                "SET @ordinaryStatus = (N'Обычный');" +
                "SET @averageStatus = (N'Средний');" +
                "SET @topStatus = (N'Топ');" +
                "SET @VIPStatus = (N'ВИП');" +
                "SELECT @clientId = ClientId  FROM inserted;" +
                "SELECT @quantity = Count(*)  FROM Orders WHERE ClientId = @clientId;" +
                "IF(@quantity <= 5) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @ordinaryStatus WHERE ClientId = @clientId; " +
                "END " +
                "IF(@quantity > 5 AND @quantity <= 30) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @averageStatus WHERE ClientId = @clientId;" +
                "END " +
                "IF(@quantity > 30 AND @quantity <= 40) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @topStatus WHERE ClientId = @clientId;" +
                "END " +
                "IF(@quantity > 40) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @VIPStatus WHERE ClientId = @clientId;" +
                "END ");

            var addDelTrigger = db.Database.ExecuteSqlCommand("CREATE TRIGGER SetClientsStatusForDelete " +
                "ON Orders " +
                "FOR DELETE " +
                "AS " +
                "DECLARE @clientId int;" +
                "DECLARE @quantity int;" +
                "DECLARE @ordinaryStatus nvarchar(10); " +
                "DECLARE @averageStatus nvarchar(10); " +
                "DECLARE @topStatus nvarchar(10);" +
                "DECLARE @VIPStatus nvarchar(10);" +
                "SET @ordinaryStatus = (N'Обычный');" +
                "SET @averageStatus = (N'Средний');" +
                "SET @topStatus = (N'Топ');" +
                "SET @VIPStatus = (N'ВИП');" +
                "SELECT @clientId = ClientId  FROM deleted;" +
                "SELECT @quantity = Count(*)  FROM Orders WHERE ClientId = @clientId;" +
                "IF(@quantity <= 5) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @ordinaryStatus WHERE ClientId = @clientId; " +
                "END " +
                "IF(@quantity > 5 AND @quantity <= 30) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @averageStatus WHERE ClientId = @clientId;" +
                "END " +
                "IF(@quantity > 30 AND @quantity <= 40) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @topStatus WHERE ClientId = @clientId;" +
                "END " +
                "IF(@quantity > 40) " +
                "BEGIN " +
                "   UPDATE Clients SET Category = @VIPStatus WHERE ClientId = @clientId;" +
                "END ");
            #endregion

            #region  generate Stored Procedure 
            var createStoredProcedure = db.Database.ExecuteSqlCommand(
                "CREATE PROCEDURE [dbo].[Database_Init] "
                + "AS "
                + "DECLARE @counter int;"
                + "DECLARE @productName nvarchar(50);"
                + "DECLARE @productsQuantity int;"

                + "DECLARE @clientsQuantity int;"         
                + "DECLARE @clientName nvarchar(50);"     
                + "DECLARE @clientAddress nvarchar(50);"  
                + "DECLARE @clientCategory nvarchar(10);" 
                + "Declare @currentClientId int;"         
                + "Declare @minOrderQuantity int;"        
                + "Declare @maxOrderQuantity int;"        
                + "Declare @CurrentOrderQuantity int;"    
                + "Declare @CurrentOrderRowsQuantity int;"
                + "Declare @CurrentProductsQuantity int;" 
                + "DECLARE @currentOrderNumber int;"      
                + "DECLARE @currentOrderId int;"          
                + "DECLARE @currentOrderRowNumber int;"   
                + "DECLARE @randomProductId int;"         
                + "DECLARE @randomPrice float;"           
                + "DECLARE @maxOrderRows int;"            
                + "DECLARE @maxProductsInOrder int;"      
                + "DECLARE @maxPrice int;"                
                + "DELETE FROM Products;"                 
                + "DELETE FROM Clients;"                  
                + "DELETE FROM Orders;"                   
                + "DELETE FROM OrderRows;"                

                + "SET @productName = (N'Товар ');"       
                + "SET @counter = 1;"                     
                + "SET @productsQuantity = 5000;"         

                + "SET @clientsQuantity = 100;"           
                + "SET @clientName = N'Клиент ';"         
                + "SET @clientAddress = N'Днепр';"        
                + "SET @clientCategory = N'Обычный';"     
                + "SET @minOrderQuantity = 5;"            
                + "SET @maxOrderQuantity = 50;"           
                + "SET @maxOrderRows = 100;"              
                + "SET @maxProductsInOrder = 100;"        
                + "SET @maxPrice = 100;"                  

                + "WHILE @counter <= @productsQuantity "   
                + "BEGIN "
                +   "INSERT INTO[dbo].[Products]([Name]) "
                +   "VALUES(@productName + CAST(@counter as nvarchar(5))) "
                +   "SET @counter = @counter + 1;"
                + "END;"
                + "SET @counter = 1;"
                + "WHILE @counter <= @clientsQuantity "
                + "BEGIN "
                +   "INSERT INTO[dbo].[Clients]([Name],[Adress],[Category]) "
                +   "VALUES((@clientName + CAST(@counter as nvarchar(5))), (@clientAddress), (@clientCategory)) "
                +   "SET @currentClientId = (SELECT IDENT_CURRENT('Clients'));"
                +   "SET @CurrentOrderQuantity = FLOOR(RAND() * (@maxOrderQuantity - @minOrderQuantity + 1) + @minOrderQuantity);"
                +   "SET @currentOrderNumber = 1;"
                +   "WHILE @currentOrderNumber <= @CurrentOrderQuantity "
                +   "BEGIN "
                +       "INSERT INTO Orders(ClientId, Date) VALUES(@currentClientId, GETDATE());"
                +       "SET @currentOrderId = (SELECT IDENT_CURRENT('Orders'));"
                +       "SET @CurrentOrderRowsQuantity = FLOOR(RAND() * (@maxOrderRows) + 1);"
                +       "SET @currentOrderRowNumber = 1;"
                +       "WHILE @currentOrderRowNumber <= @CurrentOrderRowsQuantity "
                +       "BEGIN "
                +           "SET @randomProductId = FLOOR(RAND() * (@productsQuantity) + 1);"
                +           "SET @randomPrice = ROUND(Rand() * @maxPrice, 2);"
                +           "SET @CurrentProductsQuantity = FLOOR(RAND() * (@productsQuantity) + 1);"
                +           "INSERT INTO[dbo].[OrderRows] "
                +           "([OrderId],[ProductId],[Price],[Quantity],[Sum]) "
                +           "VALUES(@currentOrderId, @randomProductId, @randomPrice, @CurrentProductsQuantity, Round(@randomPrice* @CurrentProductsQuantity, 2));"
                +           "SET @currentOrderRowNumber = @currentOrderRowNumber + 1;"
                +       "END "
                +       "SET @currentOrderNumber = @currentOrderNumber +1;"
                +   "END "
                +   "SET @counter = @counter + 1;"
                + "END;");
#endregion
            var insertProducts = db.Database.ExecuteSqlCommand("DECLARE @RC int; EXECUTE @RC = [dbo].[Database_Init];");
                       
            base.Seed(db);

        }       
    }
}

