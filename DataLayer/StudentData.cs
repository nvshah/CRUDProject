using DataLayer.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class StudentData
    {
        private TutuorDbEntities dbContext;

        public StudentData()
        {
            dbContext = new TutuorDbEntities();
        }

        public bool InsertData(StudentDTO studentRecord)
        {
            TableMapper<StudentDTO, Student_2> mapObject = new TableMapper<StudentDTO, Student_2>();
            var DbStudentRecord = mapObject.Translate(studentRecord);
            //var DbStudentRecord = new Student_2()
            //{
            //    Name = studentRecord.Name,
            //    Standard = studentRecord.Standard,
            //    Phone = studentRecord.Phone,
            //    Dob = studentRecord.Dob,
            //    Medium = studentRecord.Medium
            //};
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
            //if (students.Count == 0)
            //{
            //    return null;
            //}
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
            //if (students.Count == 0)
            //{
            //    return null;
            //}
            return students;

        }



        //public StudentDTO ViewData(int Id)
        //{
        //    Student_2 studentRecord = (Student_2)dbContext.Student_2.Where(student => student.Id == Id).FirstOrDefault();

        //    TableMapper<Student_2, StudentDTO> mapObject = new TableMapper<Student_2, StudentDTO>();
        //    var displayStudentRecord = mapObject.Translate(studentRecord);
        //    //var displayStudentRecord = new StudentDTO()
        //    //{
        //    //    Name = studentRecord.Name,
        //    //    Standard = studentRecord.Standard,
        //    //    Phone = studentRecord.Phone,
        //    //    Dob = studentRecord.Dob,
        //    //    Medium = studentRecord.Medium
        //    //};

        //    return displayStudentRecord;
        //}



    }
}
