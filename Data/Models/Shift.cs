using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Shift
    {
        public Shift()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public int DriverId { get; set; }
        public int ShiftCabId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public DateTime ShiftLoginTime { get; set; }
        public DateTime ShiftLogoutTime { get; set; }
        public bool ShiftIsActive { get; set; }
        public bool ShiftIsAvailable { get; set; }
        public decimal ShiftLongitude { get; set; }
        public decimal ShiftLatitude { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Cab ShiftCab { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
    }
}
