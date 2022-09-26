using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CabController : ControllerBase
    {
        private readonly CabService _cabService;
        public CabController(CabService cabService)
        {
            _cabService = cabService;
        }

        /// <summary>
        /// List All Cabs
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("allCabs")]

        public IActionResult GetAllCabs()
        {
            var res = _cabService.ListCabs();
            if(res!= null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All Car Models
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("CarModels")]

        public IActionResult GetAllCarModels()
        {
            var res = _cabService.ListCabModels();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

    }
}
