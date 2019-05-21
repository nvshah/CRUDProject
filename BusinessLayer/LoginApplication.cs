using DataLayer;
using DataLayer.DTOs;

namespace BusinessLayer
{
    public class LoginApplication
    {
        TutionData data;
        StudentData studentData;

        public LoginApplication()
        {
            data = new TutionData();
            studentData = new StudentData(); 
        }
        public bool GetAdminByUsernameAndPassword(dynamic userLoginDetails)
        {
            var userRecord = data.fetchRecord(userLoginDetails);
            if(userRecord != null)
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
    }
}
