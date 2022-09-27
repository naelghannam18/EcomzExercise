using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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

        /// <summary>
        /// Add Admin
        /// </summary>
        /// <param name="manageAdminVM"></param>
        /// <returns>Add Admin Status</returns>
        /// <response code="200">Admin added Successfully</response>
        /// <response code="400">Admin Already Exists</response>
        /// <response code="500">Internal Server Error. Please Contact Devs</response>

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult AddAdmin([FromBody] ManageAdminVM manageAdminVM)
        {
            var res = _adminService.AddNewAdmin(manageAdminVM);
            if (res.ToLower() == "success")
                return Ok(res);
            else if (res.Contains("Email"))
                return BadRequest(res);
            return StatusCode(500, res);
        }

        /// <summary>
        /// Update Admin
        /// </summary>
        /// <param name="manageAdminVM"></param>
        /// <returns>Add Admin Status</returns>
        /// <response code="200">Admin updated Successfully</response>
        /// <response code="400">Admin Id Does not Exist</response>
        /// <response code="500">Internal Server Error. Please Contact Devs</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateAdmin([FromBody] ManageAdminVM manageAdminVM)
        {
            var res = _adminService.UpdateAdmin(manageAdminVM);
            if (res.ToLower() == "success")
                return Ok(res);
            else if (res.Contains("exist"))
                return BadRequest(res);
            return StatusCode(500, res);
        }
        /// <summary>
        /// Login admin
        /// </summary>
        /// <param name="adminLoginVM"></param>
        /// <returns>
        /// AdminId
        /// Email
        /// JWT Token
        /// LoginToken
        /// Permissions
        /// </returns>
        ///<response code="401">Invalid Credentials</response>
        ///<response code="200">Login Successful</response>
        ///<response code="500">Internal Server Error. Please Contact Devs</response>

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
            else if (res.Contains("Credentials"))
                return Unauthorized(res);
            return StatusCode(500, res);
        }


    }
}
