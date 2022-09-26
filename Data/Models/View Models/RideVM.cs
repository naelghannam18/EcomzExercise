using System;

namespace EcomzExercise.Data.Models.View_Models
{
    public class RideVM
    {
    }

    public class RequestRideVM
    {
        public int ShiftId { get; set; }
        public DateTime RideStartTime { get; set; }
        public int RideStartingAddress { get; set; }
        public int RideEndingAddress { get; set; }
        public int RidePaymentType { get; set; }
        public int MyProperty { get; set; }
        public string? CuponCode { get; set; }
        public decimal StartingLatitude { get; set; }
        public decimal StartingLongitude { get; set; }
        public decimal EndingLatitude { get; set; }
        public decimal EndingLongitude { get; set; }
        public int PricingId { get; set; }
        public int CustomerId { get; set; }

    }

    public class RideDistanceAndTimeVM
    {
        public RideDistanceAndTimeRowsVM[] rows { get; set; }
    }

    public class RideDistanceAndTimeRowsVM
    {
        public RideDistanceAndTimeRowsElementsVM[] elements { get; set; }
    }

    public class RideDistanceAndTimeRowsElementsVM
    {
        public RideDistanceAndTimeRowsElementsDistanceVM distance { get; set; }
        public RideDistanceAndTimeRowsElementsdurationVM duration { get; set; }

    }

    public class RideDistanceAndTimeRowsElementsDistanceVM
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class RideDistanceAndTimeRowsElementsdurationVM
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class DistanceVM
    {
        public decimal StartingLat { get; set; }
        public decimal StartingLong { get; set; }
        public decimal EndingLat { get; set; }
        public decimal EndingLong { get; set; }
    }

    

}
