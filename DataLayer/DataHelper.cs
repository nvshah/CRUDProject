using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        public static DataTable CreateMarksTable(Dictionary<string, int> marks)
        {
            DataTable marksTable = new DataTable();
            marksTable.Columns.Add("Subject", typeof(string));
            marksTable.Columns.Add("Marks", typeof(int));
            foreach (var item in marks)
            {
                marksTable.Rows.Add(item.Key, item.Value);
            }
            return marksTable;
        }

        public static DataTable CreateStudentMarksTable(JToken Marks)
        {
            DataTable StudentMarksTable = new DataTable();
            StudentMarksTable.Columns.Add("StudentId", typeof(string));
            StudentMarksTable.Columns.Add("Subject", typeof(string));
            StudentMarksTable.Columns.Add("Mark", typeof(int));
            foreach(JObject record in Marks)
            {
                var id = record["StudentId"].ToString();
                var marks = record["Marks"].ToObject<Dictionary<string, int>>();
                foreach(var subject in marks)
                {
                    StudentMarksTable.Rows.Add(id, subject.Key, subject.Value);
                }
                //JsonConvert.DeserializeObject<Dictionary<string, int>>(record["Marks"].ToString());
            }
            //StudentMarksTable.Rows.Add(StudentId, marks);
            return StudentMarksTable;
        }

        public static MarksDTO GetStudentMarks(SqlDataReader reader)
        {
            return new MarksDTO
            {
                StudentId = reader.GetString(reader.GetOrdinal("Student_Id")),
                StudentName = reader.GetString(reader.GetOrdinal("Name")),
                Marks = Enumerable.Range(1, reader.FieldCount - 2)
                                  .ToDictionary(key => reader.GetName(key), value => (int)reader.GetByte(value))
            };

            //var Id = reader.GetString(reader.GetOrdinal("Student_Id"));
            //var marks = new Dictionary<string, int>();

            //for (int i = 1; i <= reader.FieldCount - 1; i++)
            //{
            //    var key = reader.GetName(i);
            //    var val = reader.GetByte(i);

            //    marks.Add(key, val);
            //}

            //return new MarksDTO
            //{
            //    StudentId = Id,
            //    Marks = marks
            //};
        }
            

            //var marksDTO = new MarksDTO();
            //marksDTO.StudentId = StudentId;
            //marksDTO.Marks = Marks;
            //return marksDTO;
    }
}
