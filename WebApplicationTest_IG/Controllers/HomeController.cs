using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest_IG.Models;

namespace WebApplicationTest_IG.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           StoreContext db = new StoreContext();

           // собираем список клиентов для первой страницы
           IEnumerable <ClientModel> clients = (from c in db.Clients
                       join o in db.Orders on c.ClientId equals o.ClientId
                       join or in db.OrderRows on o.OrderId equals or.OrderRowId
                       group or by new {
                           c.ClientId,
                           c.Name,
                           c.Adress,
                       } into clts
                       let sumBycl = Math.Round(clts.Sum(x => x.Sum),2)
                       select new ClientModel
                       {
                            ClientId = clts.Key.ClientId,
                            Name = clts.Key.Name,
                            Adress = clts.Key.Adress,
                            SumOrders = sumBycl 
                       }                                   
                       ).ToList();
            ViewBag.Clients = clients;
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Statistic()
        {
            StoreContext db = new StoreContext();
            
            
            IEnumerable<StatisticModel> stat = (from c in db.Clients
                                                join o in db.Orders on c.ClientId equals o.ClientId
                                                join or in db.OrderRows on o.OrderId equals or.OrderRowId
                                                group or by new {
                                                    c.Category,
                                                    clQant = db.Clients.Where(x => x.Category == c.Category ).Count() 
                                                }  into cat
                                                let sumByCat = Math.Round(cat.Sum(x => x.Sum),2)
                                                orderby sumByCat
                                                select new StatisticModel {
                                                   Category = cat.Key.Category,
                                                   SumByClientOrders = sumByCat,
                                                   ClientsByCatQuantity =  cat.Key.clQant
                                               }
                                                ).ToList();
            ViewBag.StatByCategories = stat;
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