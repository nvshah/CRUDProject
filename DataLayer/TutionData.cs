using DataLayer.DTOs;
using DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class TutionData
    {
        SqlConnection connection;
        TutuorDbEntities dbContext;

        public TutionData()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TutuorDb;Integrated Security=True";
            dbContext = new TutuorDbEntities();
        }

        public bool InsertUserRecord(UserRecord UserRecord)
        {
            var sql = "Insert into [dbo].[User] values(@email, @name, @password)";
            int affectedRow = 0;
            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@email", UserRecord.email);
                    cmd.Parameters.AddWithValue("@name", UserRecord.name);
                    cmd.Parameters.AddWithValue("@password", UserRecord.password);
                    affectedRow = cmd.ExecuteNonQuery();

                    cmd.Dispose();
                }
                if (affectedRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public UserRecord fetchRecord(dynamic userCredential)
        {
            var sql = "Select * from [User] where email = @email and password = @password";
            UserRecord fetchedRecord = new UserRecord();

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@email", userCredential.UserName.ToString());
                    cmd.Parameters.AddWithValue("@password", userCredential.Password.ToString());
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            fetchedRecord = null;
                        }
                        else
                        {
                            reader.Read();
                            fetchedRecord = DataHelper.GetUserData(reader);
                        }
                    }
                    cmd.Dispose();
                }
            }
            finally
            {
                connection.Close();
            }
            return fetchedRecord;
        }

        public bool InsertData(StudentDTO studentRecord)
        {
            //var sql = "SELECT CONCAT(SUBSTRING(@Year, 3, 2)" +
            //           ", RIGHT('0' + CONVERT(VARCHAR, @Standard), 2)" +
            //           ", Right('00' + Convert(Varchar, Count(*) + 1), 3)" +
            //           ", SUBSTRING(@Medium, 1, 1)) as Student_Id" +
            //           " FROM [Student_2]" +
            //           " GROUP BY Standard, Year, Medium" +
            //           " HAVING Standard = @Standard AND Year = @Year AND Medium = @Medium";

            //UserRecord fetchedRecord = new UserRecord();
            //try
            //{
            //    connection.Open();

            //    using (SqlCommand cm  d = new SqlCommand(sql, connection))
            //    {
            //        cmd.Parameters.AddWithValue("@Year", studentRecord.Year);
            //        cmd.Parameters.AddWithValue("@Standard", studentRecord.Standard);
            //        cmd.Parameters.AddWithValue("@Medium", studentRecord.Medium);

            //        var StudentId = cmd.ExecuteScalar();
            //        if(StudentId != null)
            //        {
            //            studentRecord.Student_ID = StudentId.ToString();
            //        }
            //        else
            //        {
            //            studentRecord.Student_ID = studentRecord.Year.Substring(2, 2) + 
            //                                       studentRecord.Standard.ToString("00") +
            //                                       "001" +
            //                                       studentRecord.Medium[0];

            //        }
            //        cmd.Dispose();
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine( e.Message.ToString());
            //}
            //finally
            //{
            //    connection.Close();
            //}

            //var count = dbContext.Student_2.Where(student =>
            //                           student.Year == studentRecord.Year &&
            //                           student.Medium == studentRecord.Medium &&
            //                           student.Standard == studentRecord.Standard).Count() + 1;

            var LastStudent_Id = dbContext.Student_2.Where(student =>
                                                            student.Year == studentRecord.Year &&
                                                            student.Medium == studentRecord.Medium &&
                                                            student.Standard == studentRecord.Standard)
                                                    .OrderByDescending(student => student.Student_ID)
                                                    .Select(student => student.Student_ID).FirstOrDefault();
            //studentRecord.Student_ID = LastStudent_Id.Remove(4, 3).Insert(4, (int.Parse(LastStudent_Id.Substring(4, 3)) + 1).ToString("000"));

            var count = 1;
            if (LastStudent_Id != null)
            {
                count = int.Parse(LastStudent_Id.ToString().Substring(4, 3)) + 1;
            }

            studentRecord.Student_ID = studentRecord.Year.Substring(2, 2) +
                                        studentRecord.Standard.ToString("00") +
                                        count.ToString("000") +
                                        studentRecord.Medium[0];

            //studentRecord.Student_ID = studentRecord.Year.Substring(2, 2) +
            //                           studentRecord.Standard.ToString("00") +
            //                           count.ToString("000") +
            //                           studentRecord.Medium[0];

            TableMapper<StudentDTO, Student_2> mapObject = new TableMapper<StudentDTO, Student_2>();
            var DbStudentRecord = mapObject.Translate(studentRecord);
  
            dbContext.Student_2.Add(DbStudentRecord);
            dbContext.SaveChanges();

            return true;
        }

        public bool UpdateData(StudentDTO studentRecord)
        {
            Student_2 updatedStudent = (Student_2)dbContext.Student_2.SingleOrDefault(student => student.Student_ID == studentRecord.Student_ID);

            if (updatedStudent == null)
            {
                return false;
            }
            updatedStudent.Name = studentRecord.Name;
            //updatedStudent.Standard = studentRecord.Standard;
            updatedStudent.Phone = studentRecord.Phone;
            updatedStudent.Dob = studentRecord.Dob;
            //updatedStudent.Medium = studentRecord.Medium;

            //var count = dbContext.Student_2.Where(student =>
            //                           student.Year == studentRecord.Year &&
            //                           student.Medium == studentRecord.Medium &&
            //                           student.Standard == studentRecord.Standard).Count()+1;

            //updatedStudent.Student_ID = studentRecord.Year.Substring(2, 2) +
            //                            studentRecord.Standard.ToString("00") +
            //                            count.ToString("000") +
            //                            studentRecord.Medium[0];

            dbContext.SaveChanges();
            return true;

        }

        public bool DeleteData(string id)
        {
            Student_2 deletedStudent = (Student_2)dbContext.Student_2.SingleOrDefault(student => student.Student_ID == id);

            if (deletedStudent == null)
            {
                return false;
            }

            dbContext.Student_2.Remove(deletedStudent);
            dbContext.SaveChanges();
            return true;

        }

        public List<StudentDTO> ViewStudents()
        {
            TableMapper<Student_2, StudentDTO> mapObject = new TableMapper<Student_2, StudentDTO>();
            List<StudentDTO> students = new List<StudentDTO>();

            foreach (var student in dbContext.Student_2.ToList())
            {
                students.Add(mapObject.Translate(student));
            }
            
            return students;
        }

        public List<StudentDTO> ViewStudentsByStandard(int std, string year)
        {
            TableMapper<Student_2, StudentDTO> mapObject = new TableMapper<Student_2, StudentDTO>();
            List<StudentDTO> students = new List<StudentDTO>();

            var collectedStudents = dbContext.Student_2.Where(student => student.Standard == (std == 0 ? student.Standard : std) && student.Year == (year == null ? student.Year : year)).ToList();
            foreach (var student in collectedStudents)
            {
                students.Add(mapObject.Translate(student));
            }
            
            return students;
        }

        //public bool AddStudentMarks(MarksDTO marks)
        //{
        //    try
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand("AddStudentMarks", connection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("StudentId", marks.StudentId);
        //        command.Parameters.AddWithValue("Marks", DataHelper.CreateMarksTable(marks.Marks));
        //        var result = (int)command.ExecuteScalar();
        //        if (bool.Parse(result.ToString()))
        //            return true;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return false;
        //}

        public bool AddMarks(JToken Marks)
        {
            //List<string> temp = new List<string>();
            //foreach(JObject item in Marks)
            //{
            //    temp.Add(item["StudentId"].ToString());
            //    item.GetValue("Marks").ToObject<Dictionary<string, int>>();
            //}
            int std = int.Parse(Marks["BatchIdentifier"].ToString().Substring(2,2));
            var marks = Marks["marks"];
            if(marks != null)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AddStudentMarks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Standard", std);
                        command.Parameters.AddWithValue("Marks", DataHelper.CreateStudentMarksTable(marks));
                        var result = command.ExecuteScalar();
                        if (bool.Parse(result.ToString()))
                            return true;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            
            return false;
        }

        public List<MarksDTO> fetchMarks(int std, string year)
        {
            //var marksTableName = std > 10 ? "[dbo].[HSTest1]" : "[dbo].[BHSTest1]";
            //var studentTableName = "Student_2";
            //var batchIdentifier = year.Substring(year.Length - 2, 2) + std.ToString("00") + "%";
            

            ///Query To Get ISNULL Check On All Column In Table 
            //var query = $"DECLARE @query VARCHAR(100) " +
            //                  $" SET @query = 'SELECT' +" +
            //                  $" STUFF((SELECT  ', ISNULL(' + COLUMN_NAME + ', 255) as ' + COLUMN_NAME" +
            //                  $" FROM INFORMATION_SCHEMA.COLUMNS" +
            //                  $" WHERE TABLE_NAME = '{marksTableName}'" +
            //                  $" for xml path('')), 1, 1, '') +" +
            //                  $" ' FROM HSTest1';" +
            //                  $" EXEC(@query)";

            var fetchedMarks = new List<MarksDTO>();
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FetchStudentMarks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@standard", std);
                    command.Parameters.AddWithValue("@year", year);
                    //command.Parameters.AddWithValue("@StudentTableName", "Student_2");
                    //command.Parameters.AddWithValue("@BatchIdentifier", year.Substring(year.Length - 2, 2) + std.ToString("00") + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            fetchedMarks = null;
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                fetchedMarks.Add(DataHelper.GetStudentMarks(reader));
                            }
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return fetchedMarks;
        }


    }
}
