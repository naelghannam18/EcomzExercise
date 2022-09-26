using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryInitials { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
