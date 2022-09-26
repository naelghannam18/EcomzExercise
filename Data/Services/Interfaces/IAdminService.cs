using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface IAdminService
    {
        public ManageAdminVM GetAdminDetails(long adminId);
        public AuthVM GetLoginResponse(string email);
        public string AdminLogin(AdminLoginVM vm);
        public string CheckLoginToken(CheckLoginTokenVM vm);
        public string AddNewAdmin(ManageAdminVM vm);
        public string UpdateAdmin(ManageAdminVM vm);
        public string ResetPassword(ResetPasswordVM vm);
        public string LockUnclock(long adminId);

    }
}
