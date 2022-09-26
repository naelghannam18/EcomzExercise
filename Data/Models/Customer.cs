using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
            Cupons = new HashSet<Cupon>();
        }

        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerDob { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerPassword { get; set; }
        public DateTime CustomerLastLogin { get; set; }
        public int CustomerFailedLogins { get; set; }
        public bool CustomerAccountDisabled { get; set; }
        public decimal CustomerPoints { get; set; }
        public Guid CustomerLoginToken { get; set; }
        public DateTime CustomerLoginTokenExpiry { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cupon> Cupons { get; set; }
    }
}
