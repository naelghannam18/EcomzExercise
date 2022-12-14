using EcomzExercise.Data.Models;
using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models;
using EcomzExercise.Models.Auth;
using EcomzExercise.Models.View_Models;
using EcomzExercise.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace EcomzExercise.Services
{
    public class DriverService : IDriverService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorContext;
        private readonly IJwtAuth _JwtAuth;
        private readonly IBugService _bugService;


        public DriverService(TaxiOperatorDbContext taxiOperatorContext, IJwtAuth jwtAuth, IBugService bugService)
        {
            _taxiOperatorContext = taxiOperatorContext;
            _JwtAuth = jwtAuth;
            _bugService = bugService;
        }

        public string AddDriver(AddDriverVM tempDriver)
        {
            try
            {
                Driver driverRes = _taxiOperatorContext.Drivers.FirstOrDefault(
                    driver => driver.DriverEmail == tempDriver.Email ||
                    driver.DrivingLicenseNumber == tempDriver.DriverLicense);
                if (driverRes == null)
                {
                    Driver newDriver = new Driver
                    {
                        DriverFirstName = tempDriver.FirstName,
                        DriverLastName = tempDriver.LastName,
                        DriverDob = DateTime.Now,
                        DrivingLicenseNumber = tempDriver.DriverLicense,
                        DrivingLicenseExpiry = DateTime.Now.AddMonths(12), // For Testing Purposes
                        DriverUsername = tempDriver.Username,
                        DriverPassword = Utilities.Utilities.HashText(tempDriver.Password),
                        PhoneNumber = tempDriver.PhoneNumber,
                        DriverEmail = tempDriver.Email

                    };
                    _taxiOperatorContext.Add(newDriver);
                    _taxiOperatorContext.SaveChanges();
                    return "Created";
                }
                else if (driverRes.DrivingLicenseNumber.Equals(tempDriver.DriverLicense))
                {
                    return "License Plate Already Exists";
                }
                else
                {
                    return "Email Already Exists.";
                }

            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        public string LoginDriver(AdminLoginVM adminLoginVM)
        {
            try
            {
                var email = _taxiOperatorContext.Drivers.FirstOrDefault(i => i.DriverEmail == adminLoginVM.Email);
                if (email != null)
                {

                    if (Utilities.Utilities.ValidateHash(adminLoginVM.Password, email.DriverPassword))
                    {
                        if (email.DriverAccountDisabled)
                        {
                            return "Your Account Is Locked, Please Contact Your administrator";
                        }
                        else
                        {
                            
                            email.DriverLastLogin = DateTime.Now;
                            email.DriverIsActive = true;
                            email.DriverLoginToken = Guid.NewGuid();
                            email.DriverLoginTokenExpiry = DateTime.Now.AddHours(1);
                            _taxiOperatorContext.SaveChanges();
                            return "Success";
                        }
                    }
                    else
                        return "Invalid Credientials";

                }
                return "Invalid Credientials";
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

     
        public string UpdateDriver(UpdateDriverVM updateDriver)
        {
            try
            {
                var driver = _taxiOperatorContext.Drivers.FirstOrDefault(d => d.DriverEmail == updateDriver.Email);
                if (driver != null)
                {
                    driver.DriverEmail = updateDriver.Email;
                    driver.DriverUsername = updateDriver.Username;
                    driver.DriverPassword = Utilities.Utilities.HashText(updateDriver.Password);
                    driver.DrivingLicenseNumber = updateDriver.DriverLicense;
                    driver.DrivingLicenseExpiry = updateDriver.DrivingLicenseExpiry;
                    driver.DriverFirstName = updateDriver.FirstName;
                    driver.DriverLastName = updateDriver.LastName;
                    _taxiOperatorContext.SaveChanges();
                    return "Success";
                }else
                {
                    return "Invalid User";
                }
            }catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        public string CheckLoginToken(CheckLoginTokenVM checkLoginTokenVM)
        {
            try
            {
                var driver = _taxiOperatorContext.Drivers.FirstOrDefault(d => d.DriverEmail == checkLoginTokenVM.Email);
                if (driver != null)
                {
                    if(driver.DriverLoginToken == checkLoginTokenVM.LoginToken
                        && driver.DriverLoginTokenExpiry > DateTime.Now)
                    {
                        return "Success";
                    }else
                    {
                        return "Unauthorized";
                    }
                }
                else
                {
                    return "UnAuthorized";
                }
            }catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        public AuthVM GetDriverLoginResponse(string email)
        {
            try
            {
                var driver = _taxiOperatorContext.Drivers.FirstOrDefault(d => d.DriverEmail == email);
                // test permissions

                AdminRole permissions = new AdminRole
                {
                    AdminRoleName = "Driver",
                    AdminRoles = 1,
                    ManageRole = true,
                    ManageUser = true,
                    ViewRoles = true,
                    ViewUser = true
                };

                string token = _JwtAuth.Authentication(driver.DriverEmail, "Driver");
                AuthVM response = new AuthVM { AdminId = driver.DriverId, Email = driver.DriverEmail, LoginToken = driver.DriverLoginToken, Token = token, Permissions = permissions };
                return response;
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return null;
            }
        }

        public List<ListDriversVm> ListDrivers()
        {
            try
            {
                List<Driver> Drivers = _taxiOperatorContext.Drivers.ToList();
                List<ListDriversVm> DriverList = new List<ListDriversVm>();
                foreach (var driver in Drivers)
                {
                    ListDriversVm addDriverVM = new ListDriversVm
                    {
                        DateOfBirth = driver.DriverDob,
                        DriverLicense = driver.DrivingLicenseNumber,
                        DrivingLicenseExpiry = driver.DrivingLicenseExpiry,
                        Email = driver.DriverEmail,
                        FirstName = driver.DriverFirstName,
                        LastName = driver.DriverLastName,
                        PhoneNumber = Utilities.Utilities.HashText(driver.DriverPassword),
                        Username = driver.DriverUsername,
                    };
                    DriverList.Add(addDriverVM);
                }
                return DriverList;

            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return null;
            }
        }

        public List<ListUserShiftsVM> ListDriverShifts(string driverEmail)
        {
            try
            {
                var shifts = (from s in _taxiOperatorContext.Shifts
                              join d in _taxiOperatorContext.Drivers
                              on s.DriverId equals d.DriverId
                              into sd
                              from sdJoin in sd.DefaultIfEmpty()
                              where sdJoin.DriverEmail == driverEmail
                              select new
                              {
                                  s.ShiftStart,
                                  s.ShiftEnd,
                                  sdJoin.DriverFirstName,
                                  sdJoin.DriverLastName
                              }).ToList();
                if (shifts != null)
                {
                    List<ListUserShiftsVM> listUserShifts = new List<ListUserShiftsVM>();
                    foreach (var shift in shifts)
                    {
                        ListUserShiftsVM listUserShiftsVM = new ListUserShiftsVM
                        {
                            DriverFirstName = shift.DriverFirstName,
                            DriverLastName = shift.DriverLastName,
                            ShiftStart = shift.ShiftStart,
                            ShiftEnd = shift.ShiftEnd,
                        };
                        listUserShifts.Add(listUserShiftsVM);
                    }
                    return listUserShifts;

                }else
                {
                    return null;
                }
            }catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
