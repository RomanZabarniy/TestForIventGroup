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
                  (from cls in
                          (from c in db.Clients
                           join o in
                                 (from o in db.Orders
                                  join or in db.OrderRows on o.OrderId equals or.OrderId into orSum
                                  select new
                                  {
                                      ClientId = o.ClientId,
                                      OrderSum = orSum.Sum(s => s.Sum)
                                  })
                           on c.ClientId equals o.ClientId into cl
                           select new
                           {
                               category = c.Category,
                               SumOrders = cl.Sum(x => x.OrderSum),
                               ClientId = c.ClientId
                           })                                              
                   group cls by new {
                       cls.category
                   } into cs
                   let sumByCat = cs.Sum(s => s.SumOrders)
                   let csQuantity = cs.Count()
                   select new StatisticModel {
                       Category = cs.Key.category,
                       SumByClientOrders = sumByCat,
                       ClientsByCatQuantity = csQuantity
               }).OrderBy(x=>x.SumByClientOrders).ToList();
            return stat;
        }

        public static IEnumerable<ClientModel> GetAllClients()
        {
            StoreContext db = new StoreContext();

            // Собираем список клиентов для первой страницы

            IEnumerable<ClientModel> result = 
                          (from c in db.Clients
                          join o in
                                (from o in db.Orders
                                 join or in db.OrderRows on o.OrderId equals or.OrderId into orSum
                                 select new
                                 {
                                     ClientId = o.ClientId,
                                     OrderSum = orSum.Sum(s => s.Sum)
                                 })
                          on c.ClientId equals o.ClientId into cl
                          select new ClientModel {
                              ClientId = c.ClientId,
                              Name = c.Name,
                              Adress = c.Adress,
                              SumOrders = cl.Sum(x => x.OrderSum )
                          }).ToList();
            
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