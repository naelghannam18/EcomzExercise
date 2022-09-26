using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;

namespace EcomzExercise.Services
{
    public class AdminService : IAdminService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorContext;
        private readonly IJwtAuth _jwtAuth;
        public AdminService(TaxiOperatorDbContext taxiOperatorContext, IJwtAuth jwtAuth)
        {
            _taxiOperatorContext = taxiOperatorContext;
            _jwtAuth = jwtAuth;
        }

        #region Getter



        /// <summary>
        /// Get Admin Details
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ManageAdminVM GetAdminDetails(long adminId)
        {
            var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.Id == adminId);
            if (admin != null)
            {
                ManageAdminVM result = new ManageAdminVM { AdminId = admin.Id, FirstName = admin.AdminFirstName, LastName = admin.AdminLastName, Email = admin.AdminEmail, RoleName = admin.AdminRoleName };
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get Login Response
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AuthVM GetLoginResponse(string email)
        {
            try
            {
                var admin = _taxiOperatorContext.Admins.FirstOrDefault(admin => admin.AdminEmail == email);
                // test permissions

                AdminRole permissions = new AdminRole
                {
                    AdminRoleName = "Admin",
                    AdminRoles = 1,
                    ManageRole = true,
                    ManageUser = true,
                    ViewRoles = true,
                    ViewUser = true
                };

                string token = _jwtAuth.Authentication(admin.AdminEmail, admin.AdminRoleName);
                AuthVM response = new AuthVM { AdminId = admin.Id, Email = admin.AdminEmail, LoginToken = admin.AdminLoginToken, Token = token, Permissions = permissions };
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region Setter

        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string AdminLogin(AdminLoginVM vm)
        {
            try
            {
                var email = _taxiOperatorContext.Admins.FirstOrDefault(i => i.AdminEmail == vm.Email);
                if (email != null)
                {

                    if (Utilities.Utilities.ValidateHash(vm.Password, email.AdminPassword))
                    {
                        if (email.AdminIsLocked)
                        {
                            return "Your Account Is Locked, Please Contact Your administrator";
                        }
                        else
                        {
                            email.AdminLoginToken = Guid.NewGuid();
                            email.AdminLoginTokenExpiry = DateTime.Now.AddHours(1);
                            _taxiOperatorContext.SaveChanges();
                            return "Success";
                        }
                    }
                    else
                        return "Invalid Credientials";

                }
                return "Invalid Credientials";
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Check Login Token
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string CheckLoginToken(CheckLoginTokenVM vm)
        {
            var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.AdminEmail == vm.Email);
            if (admin != null)
            {
                if (admin.AdminLoginToken == vm.LoginToken && admin.AdminLoginTokenExpiry > DateTime.Now)
                {
                    return "Success";
                }
                return "UnAuthorized";
            }
            return "UnAuthorized";
        }

        /// <summary>
        /// Add New Admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string AddNewAdmin(ManageAdminVM vm)
        {

            // check if email exist
            var email = _taxiOperatorContext.Admins.FirstOrDefault(i => i.AdminEmail == vm.Email);
            if (email == null)
            {
                var password = "TempTestPassword";
                _taxiOperatorContext.Admins.Add(new Admin
                {
                    AdminEmail = vm.Email,
                    AdminFirstName = vm.FirstName,
                    AdminLastName = vm.LastName,
                    AdminLoginToken = Guid.NewGuid(),
                    AdminLoginTokenExpiry = DateTime.Now,
                    AdminRoleName = "Admin",
                    AdminPassword = Utilities.Utilities.HashText(password),
                    AdminIsLocked = false

                });
                _taxiOperatorContext.SaveChanges();

                // TODO send Email

                return "Success";
            }
            return "Email Already Exist";
        }

        /// <summary>
        /// Update Admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string UpdateAdmin(ManageAdminVM vm)
        {
            var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.Id == vm.AdminId);
            if (admin != null)
            {
                admin.AdminFirstName = vm.FirstName;
                admin.AdminLastName = vm.LastName;
                _taxiOperatorContext.SaveChanges();

                return "Success";
            }
            return $"Admin Id : ${vm.AdminId} not exist";
        }



        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string ResetPassword(ResetPasswordVM vm)
        {
            var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.AdminEmail == vm.Email);
            if (admin != null)
            {
                if (Utilities.Utilities.ValidateHash(vm.OldPassword, admin.AdminPassword))
                {
                    admin.AdminPassword = Utilities.Utilities.HashText(vm.NewPassword);
                    _taxiOperatorContext.SaveChanges();
                    return "Success";
                }
                return "Invalid Credientials";

            }
            return "Invalid Credientials";
        }



        /// <summary>
        /// Lock Unclock
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public string LockUnclock(long adminId)
        {
            var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.Id == adminId);
            if (admin != null)
            {
                admin.AdminIsLocked = !admin.AdminIsLocked;
                _taxiOperatorContext.SaveChanges();
                return "Success";
            }
            return $"Admin Id {adminId} not exist";
        }
        #endregion
    }
}
