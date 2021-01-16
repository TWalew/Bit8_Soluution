using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Disciplines()
        {
            return View();
        }

        public ActionResult Semesters()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View();
        }

        public ActionResult TopTen()
        {
            return View();
        }

        public ActionResult StudentsForExam()
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