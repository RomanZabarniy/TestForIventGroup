using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest_IG.Models;

namespace WebApplicationTest_IG.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();

        public ActionResult Index()
        {
            //IEnumerable<Client> clients = db.Clients.OrderBy(i => i.ClientId);
            IEnumerable<ClientModel> clients = (from c in db.Clients
                                                join ord in db.Orders on c.ClientId equals ord.ClientId
                                                let sum = Math.Round(db.OrderRows.Where(x => (x.OrderId  == ord.OrderId )).Sum(x => x.Sum), 2)
                                                group c by new {
                                                    c.Name,
                                                    c.Adress,
                                                    c.ClientId,
                                                    sum
                                                } into clt
                                                orderby clt.Key.ClientId
                                                select new ClientModel()
                                                {
                                                    ClientId = clt.Key.ClientId,
                                                    Name = clt.Key.Name,
                                                    Adress = clt.Key.Adress,
                                                    SumOrders = clt.Key.sum
                                                }
                           ).ToList();
           //var cl = (from c in clients
           //                      group c by new
           //                      {
           //                          c.ClientId,
           //                          c.Adress,
           //                          c.Name,
           //                      } into cl
           //                      let summ = cl.Sum(x => x.SumOrders)
           //                      select new 
           //                      {
           //                          cl = cl.Key,
           //                          summ = summ 
           //                      }
           //                ).ToList();

            ViewBag.Clients = clients;
            return View();
        }
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Statistic()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}