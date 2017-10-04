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
            #region Params for init DB
            //Random rnd = new Random();
            //const int ProductsQuantity = 5000; //Колличество товаров
            //const int ClientsQuantity = 100;  //Колличество генерирууемых клиентов
            //const int minOrders = 5; //мин. кол. заказов на клиента
            //const int maxOrders = 50;//макс.кол. заказов на клиента
            //const int minProductsQuant = 1;  //мин. кол. товаров в заказе
            //const int maxProductsQuant = 100;//макс.кол. товаров в заказе
            #endregion

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
                +       "SET @currentOrderId = (SELECT IDENT_CURRENT('Clients'));"
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


            //var createProcProdInsert = db.Database.ExecuteSqlCommand("CREATE PROCEDURE [dbo].[Database_Init] AS DECLARE @name nvarchar(50); DECLARE @from int; DECLARE @productQuantity int; DELETE FROM Products; SET @name = (N'Товар '); SET @from = 1; SET @productQuantity = 5000;   WHILE @from <= @productQuantity  BEGIN  INSERT INTO[dbo].[Products]([Name]) VALUES(@Name + CAST(@from as nvarchar(5))) SET @from = @from + 1;  END;");
            var insertProducts = db.Database.ExecuteSqlCommand("DECLARE @RC int; EXECUTE @RC = [dbo].[Database_Init];");
                       
            base.Seed(db);

        }       
    }
}

/*
var orId = SessionDbProvider.Current.OrganizationID;
string query = "select US.Name as Employee " +
    ", US.ID as EmployeeID " +
    ", SUM(PO.Payed) as Payed " +
    ", SUM(PO.Profit) as Profit " +
    ", SUM(PO.CountTicked) as CountTicked " +
    ", AVG(PO.AverageTicked) as AverageTicked " +
    ", SUM(PO.AverageTime) as AverageTime " +
    " from " +
    " (select PO.EmployeeID as Employeer " +
    ", PO.Payment as Payed " +
    ", 0.0 as Profit " +
    ", 1 as CountTicked " +
    ", PO.Payment as AverageTicked " +
    ", 0.0 as AverageTime " +
    " from dbo.ProductOperations as PO " +
    " where PO.IsDeleted = 'False' AND PO.OperationType = 0 AND PO.OrganizationID = @orgID AND PO.Date >= @dateFrom AND PO.Date < @dateTo ) as PO " +
    "  left join dbo.Users as US on PO.Employeer = US.ID " +
    " where <UsName> " +
    " GROUP BY US.Name, US.ID ";
query = query.Replace("<UsName>", string.IsNullOrEmpty(name) ? "1=1" : "US.Name Like @name ");
                result = dbL.Database.SqlQuery<ListStatisticEmployeer>(
                    query
                    , new SqlParameter("orgID", orId)
                    , new SqlParameter("name", "%" + name + "%")
                    , new SqlParameter("dateFrom", GetDateFrom(dateFrom))
                    , new SqlParameter("dateTo", GetDateTo(dateTo))
                    ).ToList();
                    */