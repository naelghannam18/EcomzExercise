using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EcomzExercise.Models.View_Models.CustomerVM;

namespace EcomzExercise.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


    /// <summary>
    /// Add Customer
    /// </summary>
    /// <param name="addCustomerVM"></param>
    /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult AddCustomer([FromBody] AddCustomerVM addCustomerVM)
        {
            var res = _customerService.AddCustomer(addCustomerVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="updateCustomerVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("update")]

        public IActionResult UpdateCustomer([FromBody] UpdateCustomerVM updateCustomerVM)
        {
            var res = _customerService.UpdateCustomer(updateCustomerVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Customer Login
        /// </summary>
        /// <param name="adminLoginVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult AdminLogin([FromBody] AdminLoginVM adminLoginVM)
        {
            var res = _customerService.LoginUser(adminLoginVM);
            if(res.ToLower() == "success")
            {
                var res1 = _customerService.GetLoginResponse(adminLoginVM.Email);
                return Ok(res1);
            }
            return BadRequest(res);
        }


        /// <summary>
        /// List All Customers
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllCustomers()
        {
            var res = _customerService.ListCustomers();
            if(res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// Add rewards for certain customers
        /// </summary>
        /// <param name="addCustomerPointsVM"></param>
        /// <returns></returns>
        [HttpPut("addReward")]
        public IActionResult AddReward([FromBody] AddCustomerPointsVM addCustomerPointsVM)
        {
            _customerService.AddCustomerPoints(addCustomerPointsVM);
            return Ok();
        }


    }
}
