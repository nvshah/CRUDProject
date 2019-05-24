using BusinessLayer;
using DTOs;
using DataLayer;
using DataLayer.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        TutionBiz biz;

        public CRUDController()
        { 
            biz = new TutionBiz();
        }

        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult InsertStudent([FromBody]StudentDTO studentRecord)
        {
            try
            {
                if (studentRecord != null)
                {
                    var status = biz.InsertStudentRecord(studentRecord);
                    return Ok(status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("InsertUser")]
        public IHttpActionResult InsertUserRecord([FromBody]UserRecord userRecord)
        {
            try
            {
                if (userRecord != null)
                {
                    var status = biz.InsertUserRecord(userRecord);
                    return Ok(status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public IHttpActionResult AuthenticateUserRecord([FromBody]object userValidateRecord)
        {
            try
            {
                if(userValidateRecord != null)
                {
                    var status = biz.ValidateAdminByUsernameAndPassword(userValidateRecord);
                    return Ok(status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult UpdateStudent(StudentDTO studentRecord)
        {
            try
            {
                if(studentRecord != null)
                {
                    var status = biz.UpdateStudentRecord(studentRecord);
                    return Ok(status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteStudent([FromUri]string id)
        {
            try
            {
                if (id != null)
                {
                    var status = biz.DeleteStudentRecord(id);
                    return Ok(status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ViewAll")]
        public IHttpActionResult ViewAllStudent()
        {
            try
            {
                return Ok(biz.ViewStudentRecords());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("ViewByStandard")]
        public IHttpActionResult GetStudentsByStandard([FromUri]int std, [FromUri]string year)
        {
            try
            {
                return Ok(biz.ViewStudentRecordsByStandard(std, year));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
