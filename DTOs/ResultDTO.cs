using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ResultDTO<T>
    {
        public bool IsSucceed { get; set; }
        public List<string> ErrorMessages { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }
    }
    //public class StudentDTO
    //{
    //    public string Student_ID { get; set; }
    //    public string Name { get; set; }
    //    public int Standard { get; set; }
    //    public string Phone { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    //    public Nullable<System.DateTime> Dob { get; set; }
    //    public string Medium { get; set; }
    //    public string Year { get; set; }
    //}
    //public class UserRecord
    //{
    //    public string email { get; set; }
    //    public string name { get; set; }
    //    public string password { get; set; }
    //}

}
