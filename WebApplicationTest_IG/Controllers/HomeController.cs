using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest_IG.Models;
using WebApplicationTest_IG.Models.HelperClasses;
using WebApplicationTest_IG.Models.DBProvider;

namespace WebApplicationTest_IG.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Clients = DBProvider.GetAllClients();
            return View();
        }
        

        public ActionResult Orders(int id)
        {
            ViewBag.Client = DBProvider.GetClientById(id);
            return View();
        }


        public ActionResult Statistic()
        {
            ViewBag.StatByCategories = DBProvider.GetStatistic(); ;
            return View();
        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public JsonResult OrderList(string data)
        {
            int id = Convert.ToInt32(data);
            JsonResponseInfo res = new JsonResponseInfo
            {
                BodyHtml = JsonConvert.SerializeObject(DBProvider.OrdersByID(id)),
                Message = "",
                Code = "200"
            };
            return Json(res);
        }

        [HttpPost]
        public JsonResult DetailOrderList(string data)
        {
            int id = Convert.ToInt32(data);
            JsonResponseInfo res = new JsonResponseInfo
            {
                BodyHtml = JsonConvert.SerializeObject(DBProvider.DetailOrdersByID(id)),
                Message = "",
                Code = "200"
            };
            return Json(res);
        }
    }
}