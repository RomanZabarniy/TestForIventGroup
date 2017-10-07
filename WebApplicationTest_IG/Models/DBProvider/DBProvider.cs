using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models.DBProvider
{
    public static class DBProvider
    {
        public static IEnumerable<DetailOrder> DetailOrdersByID(int id)
        {
            StoreContext db = new StoreContext();
            //Детализированные заказы
            IEnumerable<DetailOrder> result =
                (from o in db.Orders
                 join or in db.OrderRows on o.OrderId equals or.OrderId
                 join p in db.Products on or.ProductId equals p.ProductId
                 where o.ClientId == id
                 orderby o.Date
                 select new DetailOrder
                 {
                     OrderID = o.OrderId,
                     OrderDate = o.Date.ToString(),
                     Sum = or.Sum,
                     ProductName = p.Name,
                     Price = or.Price,
                     Quantity = or.Quantity
                 }
                 ).ToList();

            return result;
        }

        public static IEnumerable<OrderForTable> OrdersByID(int id)
        {
            IEnumerable<DetailOrder> orders = DetailOrdersByID(id);
            //Список заказов клиента
            IEnumerable<OrderForTable> result =
                (from or in orders
                 group or by new
                 {
                     or.OrderID,
                     or.OrderDate,
                 } into ordl
                 let allSum = Math.Round(ordl.Sum(x => x.Sum), 2)
                 orderby ordl.Key.OrderID
                 select new OrderForTable
                 {
                     OrderId = ordl.Key.OrderID,
                     Date = ordl.Key.OrderDate,
                     SumByOrder = allSum
                 }).ToList();

            return result;
        }

        public static IEnumerable <StatisticModel> GetStatistic()
        {
            StoreContext db = new StoreContext();
            //Статистика
            IEnumerable<StatisticModel> stat =
                (from c in db.Clients
                 join o in db.Orders on c.ClientId equals o.ClientId
                 join or in db.OrderRows on o.OrderId equals or.OrderRowId
                 group or by new
                 {
                     c.Category,
                     clQant = db.Clients.Where(x => x.Category == c.Category).Count()
                 } into cat
                 let sumByCat = Math.Round(cat.Sum(x => x.Sum), 2)
                 orderby sumByCat
                 select new StatisticModel
                 {
                     Category = cat.Key.Category,
                     SumByClientOrders = sumByCat,
                     ClientsByCatQuantity = cat.Key.clQant
                 }
                  ).ToList();
            return stat;
        }

        public static IEnumerable<ClientModel> GetAllClients()
        {
            StoreContext db = new StoreContext();

            // Собираем список клиентов для первой страницы
            IEnumerable<ClientModel> result =
                       (from c in db.Clients
                        join o in db.Orders on c.ClientId equals o.ClientId
                        join or in db.OrderRows on o.OrderId equals or.OrderRowId
                        group or by new
                        {
                            c.ClientId,
                            c.Name,
                            c.Adress,
                        } into clts
                        let sumBycl = Math.Round(clts.Sum(x => x.Sum), 2)
                        select new ClientModel
                        {
                            ClientId = clts.Key.ClientId,
                            Name = clts.Key.Name,
                            Adress = clts.Key.Adress,
                            SumOrders = sumBycl
                        }
                        ).ToList();
            return result;
        }

        public static Client GetClientById(int id) {
            StoreContext db = new StoreContext();
            Client client = new Client
            {
                ClientId = id,
                Name = (from c in db.Clients where (c.ClientId == id) select c.Name).First().ToString()
            };
            return client;
        }
    }
}