using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string PaymentTypeName { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
