using DataLayer;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TutuionMVC.Controllers
{
    public class CRUDApiController : Controller
    {
        // GET: CRUDApi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(StudentDTO studentRecord)
        {

            if (studentRecord == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(studentRecord);
            //using (var db = new TutuorDbEntities())
            //{
            //    Student_2 stud = db.Student_2.SingleOrDefault(s => s.Id == _id);
            //    TableMapper<Student_2, StudentDTO> mapObject = new TableMapper<Student_2, StudentDTO>();
            //    StudentDTO student = mapObject.Translate(stud);
            //    Console.WriteLine(student);
            //    return View(student);
            //}      
        }        
    }
}