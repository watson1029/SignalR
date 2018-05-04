using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalR.MonitorViewTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        [HttpPost]
        public JsonResult GetData()
        {
            DataAccess.SignalData dal = new DataAccess.SignalData();
            var model = dal.GetSignalData();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}