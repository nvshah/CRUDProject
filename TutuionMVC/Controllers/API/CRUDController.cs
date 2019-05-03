using DataLayer;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TutuionMVC.Controllers.API
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
        [Route("Insert")]
        public bool InsertStudent(StudentDTO studentRecord)
        {
            bool status = false;
            status = data.InsertData(studentRecord);
            return status;
        }

        [HttpPost]
        [Route("Update")]
        public bool UpdateStudent(StudentDTO studentRecord)
        {
            bool status = false;
            status = data.UpdateData(studentRecord);
            return status;
        }
        
        [HttpGet]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteStudent([FromUri]int id)
        {
            bool status = false;
            status = data.DeleteData(id);
            return Request.CreateResponse(HttpStatusCode.OK,new { status = status });
        }

    }
}
