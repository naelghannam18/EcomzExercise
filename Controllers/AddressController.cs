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
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult AddAddress([FromBody] AddAddressVM addAddressVm)
        {
            var res = _addressService.AddAddress(addAddressVm);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All User Addresses
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllAddress()
        {
            var res = _addressService.ListAllAddresses();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All Countries
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countries/all")]
        public IActionResult GetAllCountries()
        {
            var res = _addressService.ListCountries();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All Cities
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("cities/all")]
        public IActionResult GetAllCities()
        {
            var res = _addressService.ListCities();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /// <summary>
        /// List All Address Types
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("addressTypes/all")]
        public IActionResult GetAllAddressTypes()
        {
            var res = _addressService.ListAddressTypes();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        /// <summary>
        /// List User Specific Addresses
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("user/{email}")]
        public IActionResult GetAllAddress(string email)
        {
            var res = _addressService.ListUserAddresses(email);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

    }
}
