using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Cupon
    {
        public Cupon()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public int CuponCustomerId { get; set; }
        public DateTime CuponDateIssued { get; set; }
        public DateTime CuponDateExpiry { get; set; }
        public string CuponCode { get; set; }
        public int CuponDiscount { get; set; }

        public virtual Customer CuponCustomer { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
    }
}
