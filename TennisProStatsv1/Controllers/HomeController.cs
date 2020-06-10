using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TennisProStatsv1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de TennisProStats";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto";

            return View();
        }

        public ActionResult AccessDenied()
        {
            ViewBag.Message = "Kindly login first to access your dashboard";
            return View();
        }

        public ActionResult Dashboard()
        {
            if (this.islogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
        }

        public bool islogin()
        {
            if (Session["Email"] == null)
            {
                return false;
            }
            return true;
        }
        public ActionResult AddPicker()
        {
            if (this.islogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
        }
        public ActionResult PindingPicK()
        {
           
                return View();
           
        }
    }
}