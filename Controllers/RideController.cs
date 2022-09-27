using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RideController : ControllerBase
    {

        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
            _rideService = rideService;
        }

        /// <summary>
        /// Requests A ride
        /// </summary>
        /// <param name="requestRideVM"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpPost("request")]
        public IActionResult RequestRide([FromBody] RequestRideVM requestRideVM)
        {
            var res = _rideService.RequestRide(requestRideVM);
            if(res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Cancel A Ride
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpPut("{rideId}/cancel")]
        public IActionResult CancelRide(int rideId)
        {
            var res = _rideService.CancelRide(rideId);
            if(res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Finish A Ride
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Driver")]
        [HttpPut("{rideId}/finish")]
        public IActionResult FinishRide(int rideId)
        {
            var res = _rideService.RideDone(rideId);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Testing Distance Calculations
        /// </summary>
        /// <param name="distanceVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("calculateDistance")]

        public IActionResult CalculateDistance([FromBody] DistanceVM distanceVM)
        {
            var res = _rideService.GetDistanceGoogleMaps(distanceVM.StartingLat, distanceVM.StartingLong, distanceVM.EndingLat, distanceVM.EndingLong);
            if(res != 0)
            {
                return Ok(res);
            }
            return BadRequest(res);

        }

    }
}
