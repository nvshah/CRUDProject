using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class DataHelper
    {
        public static UserRecord GetUserData(SqlDataReader reader)
        {
            return new UserRecord
            {
                name = reader.GetString(reader.GetOrdinal("name")),
                email = reader.GetString(reader.GetOrdinal("email")),
                password = reader.GetString(reader.GetOrdinal("password")),
            };
        }
    }
}
