using DataLayer;
using DataLayer.DTOs;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class TutionBiz
    {
        TutionData data;

        public TutionBiz()
        {
            data = new TutionData();
        }
        public bool ValidateAdminByUsernameAndPassword(dynamic userLoginDetails)
        {
            var userRecord = data.fetchRecord(userLoginDetails);
            if (userRecord != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertUserRecord(UserRecord userRecord)
        {
            var status = data.InsertUserRecord(userRecord);
            return status;
        }

        public bool InsertStudentRecord(StudentDTO studentRecord)
        {
            var status = data.InsertData(studentRecord);
            return status;
        }

        public bool UpdateStudentRecord(StudentDTO studentRecord)
        {
            var status = data.UpdateData(studentRecord);
            return status;
        }

        public bool DeleteStudentRecord(string id)
        {
            var status = data.DeleteData(id);
            return status;
        }

        public List<StudentDTO> ViewStudentRecords()
        {
            var students = data.ViewStudents();
            return students;
        }

        public List<StudentDTO> ViewStudentRecordsByStandard(int std, string year)
        {
            var students = data.ViewStudentsByStandard(std, year);
            return students;
        }
    }
}
