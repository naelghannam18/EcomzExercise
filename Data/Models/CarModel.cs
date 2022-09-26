using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            Cabs = new HashSet<Cab>();
        }

        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

        public virtual ICollection<Cab> Cabs { get; set; }
    }
}
