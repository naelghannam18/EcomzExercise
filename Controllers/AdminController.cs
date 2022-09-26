using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

    
        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult AddAdmin([FromBody] ManageAdminVM manageAdminVM)
        {
            var res = _adminService.AddNewAdmin(manageAdminVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        
        
        [HttpPut("update")]

        public IActionResult UpdateAdmin([FromBody] ManageAdminVM manageAdminVM)
        {
            var res = _adminService.UpdateAdmin(manageAdminVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult AdminLogin([FromBody] AdminLoginVM adminLoginVM)
        {
            var res = _adminService.AdminLogin(adminLoginVM);
            if(res.ToLower() == "success")
            {
                var res1 = _adminService.GetLoginResponse(adminLoginVM.Email);
                return Ok(res1);
            }
            return BadRequest(res);
        }


    }
}
