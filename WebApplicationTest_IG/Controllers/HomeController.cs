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

           IEnumerable <ClientModel> clients = (from c in db.Clients
                       join o in db.Orders on c.ClientId equals o.ClientId
                       join or in db.OrderRows on o.OrderId equals or.OrderRowId
                       group or by new {
                           c.ClientId,
                           c.Name,
                           c.Adress,
                       } into clts
                       let sumBycl = clts.Sum(x => x.Sum)
                       select new ClientModel
                       {
                            ClientId = clts.Key.ClientId,
                            Name = clts.Key.Name,
                            Adress = clts.Key.Adress,
                            SumOrders = sumBycl 
                       }                                   
                       ).ToList();
            //DataView view = new DataView(db.Clients);3
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