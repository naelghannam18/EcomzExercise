using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using System.Collections.Generic;
using static EcomzExercise.Models.View_Models.CustomerVM;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Adds Customer
        /// </summary>
        /// <param name="addCustomerVM"></param>
        /// <returns></returns>
        public string AddCustomer(AddCustomerVM addCustomerVM);

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="updateCustomerVM"></param>
        /// <returns></returns>
        public string UpdateCustomer(UpdateCustomerVM updateCustomerVM);

        /// <summary>
        /// Login Customer
        /// </summary>
        /// <param name="loginUserVM"></param>
        /// <returns></returns>
        public string LoginUser(AdminLoginVM loginUserVM);

        /// <summary>
        /// Return Auth Object After Successful Login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AuthVM GetLoginResponse(string email);

        /// <summary>
        /// Lists Customer
        /// </summary>
        /// <returns></returns>
        public List<ListCustomersVM> ListCustomers();

        /// <summary>
        /// Add Reward Points To Customer
        /// </summary>
        /// <param name="addCustomerPointsVM"></param>
        public void AddCustomerPoints(AddCustomerPointsVM addCustomerPointsVM);

        /// <summary>
        /// List User Cupons
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<ListCuponVM> ListUserCupons(string email);

    }
}
