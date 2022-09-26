using EcomzExercise.Data.Models.View_Models;

namespace EcomzExercise.Data.Services.Interfaces
{
    /// <summary>
    /// Interface For Rides
    /// </summary>
    public interface IRideService
    {
        /// <summary>
        /// Requesting A ride
        /// </summary>
        /// <param name="requestRideVM"></param>
        /// <returns></returns>
        public string RequestRide(RequestRideVM requestRideVM);

        /// <summary>
        /// Calculating Distance Between 2 Coordinates
        /// </summary>
        /// <param name="StartingLat"></param>
        /// <param name="startingLong"></param>
        /// <param name="endingLat"></param>
        /// <param name="endingLong"></param>
        /// <returns></returns>
        public decimal GetDistanceGoogleMaps(decimal StartingLat, decimal startingLong, decimal endingLat, decimal endingLong);
        
        /// <summary>
        /// Cancelling a Ride
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>
        public string CancelRide(int rideId);

        /// <summary>
        /// Finishing a ride
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>
        public string RideDone(int rideId);
    }
}
