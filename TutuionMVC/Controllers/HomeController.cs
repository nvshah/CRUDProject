using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutuionMVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            TutuorDbEntities db = new TutuorDbEntities();
            List<Student_2> students = db.Student_2.ToList();
            if (students.Count == 0)
            {
                students.Add(new Student_2());
            }
            return View(students);
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