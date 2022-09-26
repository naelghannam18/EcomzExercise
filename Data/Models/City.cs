using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
