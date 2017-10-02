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
            IEnumerable<Client> clients = db.Clients.OrderBy(Client => Client.Name);
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