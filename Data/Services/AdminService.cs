using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Data.Models.View_Models;

namespace EcomzExercise.Services
{
    public class AdminService : IAdminService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorContext;
        private readonly IJwtAuth _jwtAuth;
        private readonly IBugService _bugService;

        public AdminService(TaxiOperatorDbContext taxiOperatorContext, IJwtAuth jwtAuth, IBugService bugService)
        {
            _taxiOperatorContext = taxiOperatorContext;
            _jwtAuth = jwtAuth;
            _bugService = bugService;
        }

       
        public ManageAdminVM GetAdminDetails(long adminId)
        {
            try
            {
                var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.Id == adminId);
                if (admin != null)
                {
                    ManageAdminVM result = new ManageAdminVM { AdminId = admin.Id, FirstName = admin.AdminFirstName, LastName = admin.AdminLastName, Email = admin.AdminEmail, RoleName = admin.AdminRoleName };
                    return result;
                }
                return null;
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return null;
            }
        }

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
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return null;
            }
        }


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
                        return "Invalid Credentials";

                }
                return "Invalid Credeentials";
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

    
        public string CheckLoginToken(CheckLoginTokenVM vm)
        {
            try
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
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        
        public string AddNewAdmin(ManageAdminVM vm)
        {

            try
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
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        
        public string UpdateAdmin(ManageAdminVM vm)
        {
            try
            {
                var admin = _taxiOperatorContext.Admins.FirstOrDefault(i => i.Id == vm.AdminId);
                if (admin != null)
                {
                    admin.AdminFirstName = vm.FirstName;
                    admin.AdminLastName = vm.LastName;
                    _taxiOperatorContext.SaveChanges();

                    return "Success";
                }
                return $"Admin Id : ${vm.AdminId} does not exist";
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }


        public string ResetPassword(ResetPasswordVM vm)
        {
            try
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
            } catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }


        public string LockUnclock(long adminId)
        {
            try
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
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }
        
    }
}
