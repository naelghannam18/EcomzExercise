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
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Add Address
        /// </summary>
        /// <param name="addAddressVm"></param>
        /// <returns>Status Code</returns>
        /// <response code="200">Shift Ended Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles ="Customer")]
        [HttpPost("add")]
        public IActionResult AddAddress([FromBody] AddAddressVM addAddressVm)
        {
            var res = _addressService.AddAddress(addAddressVm);
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }

        /// <summary>
        /// List All User Addresses
        /// </summary>
        /// <returns>List Of All Addresses</returns>
        /// <response code="200">Addreseses Returned Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllAddress()
        {
            var res = _addressService.ListAllAddresses();
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }

        /// <summary>
        /// List All Countries
        /// </summary>
        /// <returns>List Of Countries</returns>
        /// <response code="200">Countries Returned Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [AllowAnonymous]
        [HttpGet("countries/all")]
        public IActionResult GetAllCountries()
        {
            var res = _addressService.ListCountries();
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }

        /// <summary>
        /// List All Cities
        /// </summary>
        /// <returns>List Of Cities</returns>
        /// <response code="200">Cities Returned Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [AllowAnonymous]
        [HttpGet("cities/all")]
        public IActionResult GetAllCities()
        {
            var res = _addressService.ListCities();
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }

        /// <summary>
        /// List All Address Types
        /// </summary>
        /// <returns>List Of Address types</returns>
        /// <response code="200">Types Returned Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [AllowAnonymous]
        [HttpGet("addressTypes/all")]
        public IActionResult GetAllAddressTypes()
        {
            var res = _addressService.ListAddressTypes();
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }


        /// <summary>
        /// List User Specific Addresses
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns Addresses That Belong To A Customer</returns>
        /// <response code="200">Addresses Returned Successfully</response>
        /// <response code="500">Internal Server Error. Please Report To Devs</response>
        [Authorize(Roles ="Customer")]
        [HttpGet("user/{email}")]
        public IActionResult GetAllAddress(string email)
        {
            var res = _addressService.ListUserAddresses(email);
            if (res != null)
            {
                return Ok(res);
            }
            return StatusCode(500, res);
        }

    }
}
