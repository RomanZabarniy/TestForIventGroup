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
            Random rnd = new Random();
            const int ProductsQuantity = 5000; //Колличество товаров
            const int ClientsQuantity = 100;  //Колличество генерирууемых клиентов
            const int minOrders = 5; //мин. кол. заказов на клиента
            const int maxOrders = 50;//макс.кол. заказов на клиента
            //const int minProductsQuant = 1;  //мин. кол. товаров в заказе
            //const int maxProductsQuant = 100;//макс.кол. товаров в заказе

            db.Products.Add(new Product { Name = "Продукт1 "  });
            db.Products.Add(new Product { Name = "Продукт2 " });
            Guid clGuid = Guid.NewGuid();
            db.Clients.Add(new Client { ClientId = clGuid, Name = "Клиент2 " , Adress = "г. Днепр" });
            

            //for (int i = 1; i < ProductsQuantity + 1; i++)
            //{
            //    db.Products.Add(new Product { Name = "Продукт " + i });
            //}
            //for (int i = 1; i < ClientsQuantity + 1; i++)
            //{
            //    Guid clGuid = Guid.NewGuid();
            //    db.Clients.Add(new Client { ClientId = clGuid, Name = "Клиент " + i, Adress = "г. Днепр" });
            //    int ordersQuantity = rnd.Next(minOrders, maxOrders);
            //    for (int j = 0; j < ordersQuantity; j++)
            //    {
            //        //db.Orders.Add(new Order { })
            //    }
            //}

            base.Seed(db);
        }

        //private OrderRow generateOrderRow(int min, int max, int id)
        //{
        //    int productsInOrderRow = rnd.Next(min,max);
        //    for (int i = 0; i < productsInOrderRow; i++)
        //    {

        //    }
        //    OrderRow orderRow = new OrderRow {OrderId = 1, ProductId = 1, Price = 1.0, Quantity = 1, Sum = 1};
        //    return orderRow;
        //}

        //private Order generateOrder()
        //{

        //    Order order = new Order {ClientId = 1, Date = DateTime.Now };
        //    return order;
        //}
    }
}