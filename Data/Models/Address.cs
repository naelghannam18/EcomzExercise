using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            RideRideDestinationAddressNavigations = new HashSet<Ride>();
            RideRideStartingAddressNavigations = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public int AddressStreetNumber { get; set; }
        public string AddressStreetName { get; set; }
        public int AddressZipPostal { get; set; }
        public int CityId { get; set; }
        public int CustomerId { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual City City { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Ride> RideRideDestinationAddressNavigations { get; set; }
        public virtual ICollection<Ride> RideRideStartingAddressNavigations { get; set; }
    }
}
