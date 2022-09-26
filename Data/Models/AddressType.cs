using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string AddressTypeDescription { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
