using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Cab
    {
        public Cab()
        {
            Shifts = new HashSet<Shift>();
        }

        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int CarModelId { get; set; }
        public bool CabIsActive { get; set; }

        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
