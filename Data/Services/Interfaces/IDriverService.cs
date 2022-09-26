using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using System.Collections.Generic;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface IDriverService
    {
        public string AddDriver(AddDriverVM tempDriver);
        public string UpdateDriver(UpdateDriverVM updateDriver);
        public string LoginDriver(AdminLoginVM adminLoginVM);
        public AuthVM GetDriverLoginResponse(string email);
        public List<ListDriversVm> ListDrivers();
        public string CheckLoginToken(CheckLoginTokenVM checkLoginTokenVM);
        public List<ListUserShiftsVM> ListDriverShifts(string driverEmail);
        

    }
}
