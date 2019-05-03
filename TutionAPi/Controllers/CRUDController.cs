using DataLayer;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TutionAPi.Controllers
{
    [RoutePrefix("api/CRUD")]
    public class CRUDController : ApiController
    {
        StudentData data;
        public CRUDController()
        {
            data = new StudentData();
        }

        [HttpPost]
        [Route("InsertStudent")]
        public bool InsertStudent(StudentDTO studentRecord)
        {
            bool status = false;
            status = data.InsertData(studentRecord);
            return status;
        }

    }
}
