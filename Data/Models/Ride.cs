using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Ride
    {
        public int Id { get; set; }
        public int RideShiftId { get; set; }
        public DateTime RideStartTime { get; set; }
        public DateTime RideEndTime { get; set; }
        public int RideStartingAddress { get; set; }
        public int RideDestinationAddress { get; set; }
        public bool RideCanceled { get; set; }
        public bool RideDone { get; set; }
        public decimal RideRewardPoints { get; set; }
        public decimal RidePrice { get; set; }
        public int RidePaymentType { get; set; }
        public int? RideCuponId { get; set; }

        public virtual Cupon RideCupon { get; set; }
        public virtual Address RideDestinationAddressNavigation { get; set; }
        public virtual PaymentType RidePaymentTypeNavigation { get; set; }
        public virtual Shift RideShift { get; set; }
        public virtual Address RideStartingAddressNavigation { get; set; }
    }
}
