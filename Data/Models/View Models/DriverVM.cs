using System;

namespace EcomzExercise.Models.View_Models
{
    public class DriverVM
    {

    }

    public class AddDriverVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DrivingLicenseExpiry { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ListDriversVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DrivingLicenseExpiry { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class UpdateDriverVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DrivingLicenseExpiry { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ListUserShiftsVM
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }

    }




}
