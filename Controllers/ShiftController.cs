using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        /// <summary>
        /// Shift Controller Constructor
        /// </summary>
        /// <param name="shiftService"></param>
        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        /// <summary>
        /// Add Shift
        /// </summary>
        /// <param name="addShiftVM"></param>
        /// <returns>Status Code</returns>
        /// <response code="201">Shift Added Suucessfully</response>
        /// <response code="409">Added Shift Conflicts With Other Shifts.</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        
        public IActionResult AddShift([FromBody] AddShiftVM addShiftVM)
        {
            var res = _shiftService.AddShift(addShiftVM);
            if (res.ToLower() == "success")
                return StatusCode(201, "shift Added Successfully.");
            else if (res.ToLower().Contains("conflict"))
                return StatusCode(409, res);

            return StatusCode(500, res);
            
            
        }

        /// <summary>
        /// Update Shift
        /// </summary>
        /// <param name="updateShiftvm"></param>
        /// <returns>Status Code</returns>
        /// <response code="200">Shift Updated Successfully</response>
        /// <response code="400">Invalid Shift Id</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateShift([FromBody] AddShiftVM updateShiftvm)
        {
            var res = _shiftService.UpdateShift(updateShiftvm);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            else if (res.ToLower().Contains("invalid"))
                return BadRequest(res);
            else
                return StatusCode(500, res);
        }

        /// <summary>
        /// Start Shift
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Status Code</returns>
        /// <response code="200">Shift Started Successfully</response>
        /// <response code="400">Invalid Shift Id</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Driver")]
        [HttpPut("start")]
        public IActionResult StartShift([FromBody] ToggleShiftVM email)
        {
            var res = _shiftService.StartShift(email);
            if (res.ToLower() == "success")
                return Ok(res);
            else if (res.ToLower().Contains("invalid"))
                return BadRequest(res);
            else
                return StatusCode(500,res);
        }


        /// <summary>
        /// End Shift
        /// </summary>
        /// <returns>Status Code</returns>
        /// <response code="200">Shift Ended Successfully</response>
        /// <response code="400">Invalid Shift Id</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Driver")]
        [HttpPut("end")]
        public IActionResult EndShift([FromBody] ToggleShiftVM email)
        {
            var res = _shiftService.EndShift(email);
            if (res.ToLower() == "success")
                return Ok(res);
            else if (res.ToLower().Contains("invalid"))
                return BadRequest(res);
            else
                return StatusCode(500, res);
        }

        /// <summary>
        /// List All Shifts
        /// </summary>
        /// <returns>
        /// 
        ///  List of All the Shifts   
        /// 
        /// </returns>
        /// <response code="200">Return List Of all Shifts</response>
        /// <response code="204">No Shifts</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllShifts()
        {
            var res = _shiftService.ListShifts();
            if(res == null)
                return StatusCode(500, "Something went wrong.");

            else if (res.Count > 0)
                return Ok(res);
            else if (res.Count == 0)
                return StatusCode(204, "No Shifts Are Available.");
            else
                return StatusCode(500, "Something went wrong.");

        }


        /// <summary>
        /// List Shifts that are Active and available
        /// </summary>
        /// <returns>
        /// 
        ///  List of All the available Shifts   
        /// 
        /// </returns>
        /// <response code="200">Return List Of all available Shifts</response>
        /// <response code="400">No Shifts</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Customer")]
        [HttpGet("available")]
        public IActionResult GetAvailableShifts()
        {
            var res = _shiftService.ListAvailableShifts();
            if (res.Count > 0)
                return Ok(res);
            else if (res.Count == 0)
                return StatusCode(400, "No Shifts Are Available At this time");
            else
                return StatusCode(500, "Something went wrong.");
        }

    }
}
