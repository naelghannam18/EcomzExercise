using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using System.Collections.Generic;
using static EcomzExercise.Models.View_Models.CustomerVM;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface ICustomerService
    {
        public string AddCustomer(AddCustomerVM addCustomerVM);
        public string UpdateCustomer(UpdateCustomerVM updateCustomerVM);
        public string LoginUser(AdminLoginVM loginUserVM);
        public AuthVM GetLoginResponse(string email);

        public List<ListCustomersVM> ListCustomers();
        public void AddCustomerPoints(AddCustomerPointsVM addCustomerPointsVM);

        public List<ListCuponVM> ListUserCupons(string email);

    }
}
