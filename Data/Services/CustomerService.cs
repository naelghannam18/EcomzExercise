using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models;
using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcomzExercise.Models.View_Models.CustomerVM;

namespace EcomzExercise.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbContext;
        private readonly IJwtAuth _jwtAuth;


        public CustomerService(TaxiOperatorDbContext taxiOperatorDbContext, IJwtAuth jwtAuth)
        {
            _taxiOperatorDbContext = taxiOperatorDbContext;
            _jwtAuth = jwtAuth;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="addCustomerVM"></param>
        /// <returns></returns>
        public string AddCustomer(AddCustomerVM addCustomerVM)
        {
            try
            {

                var c = _taxiOperatorDbContext.Customers.FirstOrDefault(customer => customer.CustomerEmail == addCustomerVM.CustomerEmail);

                if (c == null)
                {

                    Customer customer = new()
                    {
                        CustomerAccountDisabled = false,
                        CustomerDob = addCustomerVM.CustomerDob,
                        CustomerEmail = addCustomerVM.CustomerEmail,
                        CustomerFailedLogins = 0,
                        CustomerFirstName = addCustomerVM.CustomerFirstName,
                        CustomerLastName = addCustomerVM.CustomerLastName,
                        CustomerGender = addCustomerVM.CustomerGender,
                        CustomerLastLogin = DateTime.Now,
                        CustomerLoginToken = Guid.NewGuid(),
                        CustomerLoginTokenExpiry = DateTime.MinValue,
                        CustomerPassword = Utilities.Utilities.HashText(addCustomerVM.CustomerPassword),
                        CustomerPoints = 0,

                    };
                    _taxiOperatorDbContext.Add(customer);
                    _taxiOperatorDbContext.SaveChanges();
                    return "Success";

                }
                else
                {
                    return "Customer Exists";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Add points to customer on Rides
        /// </summary>
        /// <param name="addCustomerPointsVM"></param>
        public void AddCustomerPoints(AddCustomerPointsVM addCustomerPointsVM)
        {
            try
            {

                var c = _taxiOperatorDbContext.Customers.FirstOrDefault(customer => customer.CustomerEmail == addCustomerPointsVM.Email);
                if (c != null)
                {
                    c.CustomerPoints += addCustomerPointsVM.Points;
                    var x = c.CustomerPoints;
                    _taxiOperatorDbContext.SaveChanges();

                }
                else
                {
                    Console.WriteLine($"User{addCustomerPointsVM.Email} Does Not Exist");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public AuthVM GetLoginResponse(string email)
        {
            try
            {
                var customer = _taxiOperatorDbContext.Customers.FirstOrDefault(c => c.CustomerEmail == email);
                // test permissions

                AdminRole permissions = new AdminRole
                {
                    AdminRoleName = "Customer",
                    AdminRoles = 1,
                    ManageRole = false,
                    ManageUser = false,
                    ViewRoles = false,
                    ViewUser = true
                };

                string token = _jwtAuth.Authentication(customer.CustomerEmail, "Customer");
                AuthVM response = new AuthVM { AdminId = customer.Id, Email = customer.CustomerEmail, LoginToken = customer.CustomerLoginToken, Token = token, Permissions = permissions };
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// List All Users
        /// </summary>
        /// <returns></returns>

        public List<ListCustomersVM> ListCustomers()
        {
            try
            {
                var c = _taxiOperatorDbContext.Customers.ToList();
                List<ListCustomersVM> listCustomers = new List<ListCustomersVM>();
                foreach (var customer in c)
                {
                    ListCustomersVM listCustomersVM = new()
                    {
                        CustomerDob = customer.CustomerDob,
                        CustomerEmail = customer.CustomerEmail,
                        CustomerFirstName = customer.CustomerFirstName,
                        CustomerLastName = customer.CustomerLastName,
                        CustomerGender = customer.CustomerGender,
                        CustomerPoints = customer.CustomerPoints,
                    };
                    listCustomers.Add(listCustomersVM);
                }
                return listCustomers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// return Cupons That Belong To Customer
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<ListCuponVM> ListUserCupons(string email)
        {
            try
            {
                var cust = _taxiOperatorDbContext.Customers.FirstOrDefault(customer => customer.CustomerEmail == email);
                if (cust != null)
                {
                    var cupons = (from c in _taxiOperatorDbContext.Cupons
                                  where c.CuponCustomerId == cust.Id
                                  select c).ToList();
                    List<ListCuponVM> list = new List<ListCuponVM>();
                    foreach (var cupon in cupons)
                    {
                        list.Add(new ListCuponVM()
                        {
                            CuponCode = cupon.CuponCode,
                            CuponDateExpiry = cupon.CuponDateExpiry,
                            CuponDateIssued = cupon.CuponDateIssued,
                            CuponDiscount = cupon.CuponDiscount
                        });
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginUserVM"></param>
        /// <returns></returns>
        public string LoginUser(AdminLoginVM loginUserVM)
        {
            try
            {
                var email = _taxiOperatorDbContext.Customers.FirstOrDefault(c => c.CustomerEmail == loginUserVM.Email);
                if (email != null)
                {

                    if (Utilities.Utilities.ValidateHash(loginUserVM.Password, email.CustomerPassword))
                    {
                        if (email.CustomerAccountDisabled)
                        {
                            return "Your Account Is Locked, Please Contact Your administrator";
                        }
                        else
                        {
                            email.CustomerLoginToken = Guid.NewGuid();
                            email.CustomerLoginTokenExpiry = DateTime.Now.AddHours(1);
                            _taxiOperatorDbContext.SaveChanges();
                            return "Success";
                        }
                    }
                    else
                    {
                        return "Invalid Credentials";
                    }


                }
                return "Invalid Credientials";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Updating a Customer
        /// </summary>
        /// <param name="updateCustomerVM"></param>
        /// <returns></returns>
        public string UpdateCustomer(UpdateCustomerVM updateCustomerVM)
        {
            try
            {

                var c = _taxiOperatorDbContext.Customers.FirstOrDefault(customer => customer.CustomerEmail == updateCustomerVM.CustomerEmail);
                if (c != null)
                {

                    c.CustomerEmail = updateCustomerVM.CustomerEmail;
                    c.CustomerFirstName = updateCustomerVM.CustomerFirstName;
                    c.CustomerLastName = updateCustomerVM.CustomerLastName;
                    c.CustomerPassword = Utilities.Utilities.HashText(updateCustomerVM.CustomerPassword);

                    _taxiOperatorDbContext.SaveChanges();
                    return "Success";

                }
                else
                {
                    return "Invalid Customer";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





        // Todo
        // Add Customer
        // Customer Login
        // Update Customer
        // Get Points 
    }
}
