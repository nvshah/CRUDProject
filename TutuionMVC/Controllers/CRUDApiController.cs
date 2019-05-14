using BusinessLayer;
using DataLayer;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace TutuionMVC.Controllers
{
    public class CRUDApiController : Controller
    {

        //CRUDApiController()
        //{
        //    biz = new LoginApplication();
        //}
        // GET: CRUDApi/Index
        [HttpGet]
        [Route("CRUDApi/Index")]
        public ActionResult Index()
        {
            List<StudentDTO> Students = new List<StudentDTO>();
            using (var client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:62283/api/CRUD/ViewAll").Result;
                if (result.IsSuccessStatusCode)
                {
                    Students = result.Content.ReadAsAsync<List<StudentDTO>>().Result;
                }
            }
            return View(Students);
        }

        //[HttpGet]
        //[Route("CRUDApi/Index1/{std:int?}")]
        //public ActionResult Index1(int std = 0)
        //{

        //    ViewBag.standard = std;
        //    return View();
        //}

        [HttpGet]
        [Route("CRUDApi/Index1")]
        public ActionResult Index1()
        {
            return View();
        }


        //GET: CRUDApi/Create
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