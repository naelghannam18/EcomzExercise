using EcomzExercise.Data.Models;
using System;
using System.Collections.Generic;

namespace EcomzExercise.Models.View_Models
{
    public class ShiftVM
    {

    }

    public class AddShiftVM
    {
        public int DriverId { get; set; }
        public int ShiftCabId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }


    }

    public class ListShiftsVM
    {
        public string ModelName { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public DateTime ShiftStarts { get; set; }
        public DateTime ShiftEnds { get; set; }
        public decimal ShiftLatitude { get; set; }
        public decimal ShiftLongitude { get; set; }
        public bool shiftIsActive { get; set; }
        public bool ShiftIsAvailable { get; set; }

    }

    public class ToggleShiftVM
    {
        public string Email { get; set; }
    }

    public class ShiftTimesVM
    {
        public int ShiftId { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

    }

}
