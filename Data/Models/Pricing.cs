using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Pricing
    {
        public Pricing()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string PricingName { get; set; }
        public decimal PricingPerKm { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
