using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Shifts = new HashSet<Shift>();
        }

        public int DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public DateTime DriverDob { get; set; }
        public string PhoneNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime DrivingLicenseExpiry { get; set; }
        public bool DriverIsActive { get; set; }
        public string DriverUsername { get; set; }
        public string DriverPassword { get; set; }
        public string DriverEmail { get; set; }
        public DateTime DriverLastLogin { get; set; }
        public int DriverFailedLogins { get; set; }
        public bool DriverAccountDisabled { get; set; }
        public Guid DriverLoginToken { get; set; }
        public DateTime DriverLoginTokenExpiry { get; set; }

        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
