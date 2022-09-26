using System;

namespace EcomzExercise.Models.View_Models
{
    public class AdminVM
    {
    }
    public class AdminListVM
    {
        public string FullName { get; set; }
        public long AdminId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
    }

    public class CheckLoginTokenVM
    {
        public string Email { get; set; }
        public Guid LoginToken { get; set; }
    }

    public class ManageAdminVM
    {
        public long AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

    public class AdminLoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordVM
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
