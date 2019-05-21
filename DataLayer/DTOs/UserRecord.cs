using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs
{
    public class UserRecord
    {
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }
}
