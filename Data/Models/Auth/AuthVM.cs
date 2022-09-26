using System;

namespace EcomzExercise.Models.Auth
{
    public class AuthVM
    {
        public long AdminId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Guid LoginToken { get; set; }
        public AdminRole Permissions { get; set; }
    }

    public class UserAuthVM
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Guid LoginToken { get; set; }
        public string UID { get; set; }
    }
}
