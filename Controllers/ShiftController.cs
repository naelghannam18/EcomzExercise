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
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("add")]
        
        public IActionResult AddShift([FromBody] AddShiftVM addShiftVM)
        {
            var res = _shiftService.AddShift(addShiftVM);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Update Shift
        /// </summary>
        /// <param name="updateShiftvm"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("update")]
        public IActionResult UpdateShift([FromBody] AddShiftVM updateShiftvm)
        {
            var res = _shiftService.UpdateShift(updateShiftvm);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Start Shift
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("start")]
        public IActionResult StartShift([FromBody] ToggleShiftVM email)
        {
            var res = _shiftService.StartShift(email);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        /// <summary>
        /// End Shift
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("end")]
        public IActionResult EndShift([FromBody] ToggleShiftVM email)
        {
            var res = _shiftService.EndShift(email);
            if (res.ToLower() == "success")
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All Shifts
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllShifts()
        {
            var res = _shiftService.ListShifts();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

    }
}
