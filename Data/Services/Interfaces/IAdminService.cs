using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;

namespace EcomzExercise.Data.Services.Interfaces
{
    /// <summary>
    /// Admin Interface
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Returns Admin Details
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ManageAdminVM GetAdminDetails(long adminId);

        /// <summary>
        /// Returns Auth Object After Successful Login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AuthVM GetLoginResponse(string email);

        /// <summary>
        /// Handles Admin Login Logic
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string AdminLogin(AdminLoginVM vm);

        /// <summary>
        /// Checks Addmin Login Token
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string CheckLoginToken(CheckLoginTokenVM vm);

        /// <summary>
        /// Add New Admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string AddNewAdmin(ManageAdminVM vm);

        /// <summary>
        /// update Admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string UpdateAdmin(ManageAdminVM vm);

        /// <summary>
        /// Reset Admin Password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string ResetPassword(ResetPasswordVM vm);

        /// <summary>
        /// Locks/Unlocks Admin Account
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public string LockUnclock(long adminId);

    }
}
