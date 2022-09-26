using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;

        }

        /// <summary>
        /// Add Driver
        /// </summary>
        /// <param name="addDriverVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult AddDriver([FromBody] AddDriverVM addDriverVM)
        {
            var result = _driverService.AddDriver(addDriverVM);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// List All Drivers
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult ListDrivers()
        {
            var result = _driverService.ListDrivers();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Something Went Wrong.");
            }
        }

        /// <summary>
        /// Driver Login
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginDriver([FromBody] AdminLoginVM loginVM)
        {
            var res = _driverService.LoginDriver(loginVM);
            if (res.ToLower() == "success")
            {
                var res1 = _driverService.GetDriverLoginResponse(loginVM.Email);
                return Ok(res1);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Update Driver
        /// </summary>
        /// <param name="driverVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("update")]
        public IActionResult UpdateDriver([FromBody] UpdateDriverVM driverVM)
        {
            var res = _driverService.UpdateDriver(driverVM);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        /// <summary>
        /// Get Driver shifts
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{email}/shifts")]
        public IActionResult GetDriverShifts(string email)
        {
            var res = _driverService.ListDriverShifts(email);

            if(res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
