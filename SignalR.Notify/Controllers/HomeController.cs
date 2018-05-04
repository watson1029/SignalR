using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalR.Notify.Controllers
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

        public ActionResult Send()
        {
            return View();
        }

        public ActionResult FlyPlan()
        {
            return View();
        }

        public ActionResult RepetPlan()
        {
            return View();
        }

        public bool SendFlyPlan()
        {
            Hubs.NotifyHub.FlyPlanNotify();
            return true;
        }

        public bool SendRepetPlan()
        {
            Hubs.NotifyHub.RepetPlanNotify();
            return true;
        }
    }
}