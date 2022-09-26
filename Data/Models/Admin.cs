using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Admin
    {
        public long Id { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public Guid AdminLoginToken { get; set; }
        public DateTime AdminLoginTokenExpiry { get; set; }
        public bool AdminIsLocked { get; set; }
        public string AdminRoleName { get; set; }
    }
}
