using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using System.Collections.Generic;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface IDriverService
    {

        /// <summary>
        /// Add Driver
        /// </summary>
        /// <param name="tempDriver"></param>
        /// <returns></returns>
        public string AddDriver(AddDriverVM tempDriver);

        /// <summary>
        /// Update Driver
        /// </summary>
        /// <param name="updateDriver"></param>
        /// <returns></returns>
        public string UpdateDriver(UpdateDriverVM updateDriver);

        /// <summary>
        /// Login Driver
        /// </summary>
        /// <param name="adminLoginVM"></param>
        /// <returns></returns>
        public string LoginDriver(AdminLoginVM adminLoginVM);

        /// <summary>
        /// Return Auth Object After Successful Login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AuthVM GetDriverLoginResponse(string email);

        /// <summary>
        /// List All Drivers
        /// </summary>
        /// <returns></returns>
        public List<ListDriversVm> ListDrivers();

        /// <summary>
        /// Check Driver Login Token
        /// </summary>
        /// <param name="checkLoginTokenVM"></param>
        /// <returns></returns>
        public string CheckLoginToken(CheckLoginTokenVM checkLoginTokenVM);

        /// <summary>
        /// List Driver Shifts
        /// </summary>
        /// <param name="driverEmail"></param>
        /// <returns></returns>
        public List<ListUserShiftsVM> ListDriverShifts(string driverEmail);
        

    }
}
