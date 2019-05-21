using DataLayer.DTOs;
using System.Collections.Generic;
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
            TableMapper<StudentDTO, Student_2> mapObject = new TableMapper<StudentDTO, Student_2>();
            var DbStudentRecord = mapObject.Translate(studentRecord);
  
            dbContext.Student_2.Add(DbStudentRecord);
            dbContext.SaveChanges();

            return true;
        }

        public bool UpdateData(StudentDTO studentRecord)
        {
            Student_2 updatedStudent = (Student_2)dbContext.Student_2.SingleOrDefault(student => student.Id == studentRecord.Id);

            if (updatedStudent == null)
            {
                return false;
            }
            updatedStudent.Name = studentRecord.Name;
            updatedStudent.Standard = studentRecord.Standard;
            updatedStudent.Phone = studentRecord.Phone;
            updatedStudent.Dob = studentRecord.Dob;
            updatedStudent.Medium = studentRecord.Medium;

            dbContext.SaveChanges();
            return true;

        }

        public bool DeleteData(int id)
        {
            Student_2 deletedStudent = (Student_2)dbContext.Student_2.SingleOrDefault(student => student.Id == id);

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

        public List<StudentDTO> ViewStudentsByStandard(int std)
        {
            TableMapper<Student_2, StudentDTO> mapObject = new TableMapper<Student_2, StudentDTO>();
            List<StudentDTO> students = new List<StudentDTO>();


            foreach (var student in dbContext.Student_2.Where(student => student.Standard == std).ToList())
            {
                students.Add(mapObject.Translate(student));
            }
            
            return students;
        }


    }
}
