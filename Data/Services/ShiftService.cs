using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;
using EcomzExercise.Models;
using EcomzExercise.Models.View_Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcomzExercise.Services
{
    /// <summary>
    /// Handles Shift Manipulations
    /// </summary>
    public class ShiftService : IShiftService
    {

        private readonly TaxiOperatorDbContext _taxiOperatorContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taxiOperatorContext"></param>
        public ShiftService(TaxiOperatorDbContext taxiOperatorContext)
        {
            _taxiOperatorContext = taxiOperatorContext;
        }

        /// <summary>
        /// Add Shift
        /// </summary>
        /// <param name="addShiftVM"></param>
        /// <returns></returns>
        public string AddShift(AddShiftVM addShiftVM)
        {
            try
            {
                Shift newShift = new()
                {
                    ShiftStart = addShiftVM.ShiftStart,
                    ShiftEnd = addShiftVM.ShiftStart.AddHours(12),
                    DriverId = addShiftVM.DriverId,
                    ShiftCabId = addShiftVM.ShiftCabId,
                    ShiftIsActive = false,
                    ShiftIsAvailable = false,
                    ShiftLatitude = 0,
                    ShiftLongitude = 0,
                    ShiftLoginTime = DateTime.MinValue,
                    ShiftLogoutTime = DateTime.MinValue,
                };

                var takenShifts = (from s in _taxiOperatorContext.Shifts
                                   where s.ShiftStart.Day == addShiftVM.ShiftStart.Day
                                   && s.DriverId == addShiftVM.DriverId && s.ShiftCabId == addShiftVM.ShiftCabId
                                   && (addShiftVM.ShiftStart <= s.ShiftEnd || addShiftVM.ShiftStart >= s.ShiftStart.AddHours(-12))
                                   select s).ToList();

                if (takenShifts.Count == 0)
                {
                    _taxiOperatorContext.Add(newShift);
                    _taxiOperatorContext.SaveChanges();
                    return "Success";
                }

                return "Conflicting Shifts";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// End Shift When Driver Logs Out
        /// </summary>
        /// <param name="driverEmail"></param>
        /// <returns></returns>
        public string EndShift(ToggleShiftVM driverEmail)
        {
            try
            {

                var driver = _taxiOperatorContext.Drivers.FirstOrDefault(d => d.DriverEmail == driverEmail.Email);
                if (driver != null)
                {
                    var shift = _taxiOperatorContext.Shifts.FirstOrDefault(s => s.DriverId == driver.DriverId);
                    if (shift != null)
                    {
                        shift.ShiftIsAvailable = false;
                        shift.ShiftIsActive = false;
                        _taxiOperatorContext.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Shift Does not Exist";
                    }
                }
                else
                {
                    return "Invalid Driver.";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Listing All Shifts
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ListShiftsVM> ListShifts()
        {
            try
            {
                var shifts = (from s in _taxiOperatorContext.Shifts
                              join d in _taxiOperatorContext.Drivers
                              on s.DriverId equals d.DriverId
                              join c in _taxiOperatorContext.Cabs
                              on s.ShiftCabId equals c.Id
                              join m in _taxiOperatorContext.CarModels
                              on c.CarModelId equals m.Id
                              select new
                              {
                                  m.ModelName,
                                  d.DriverFirstName,
                                  d.DriverLastName,
                                  s.ShiftStart,
                                  s.ShiftEnd,
                                  s.ShiftLatitude,
                                  s.ShiftLongitude,
                                  s.ShiftIsActive,
                                  s.ShiftIsAvailable

                              }).ToList();
                List<ListShiftsVM> listShifts = new List<ListShiftsVM>();
                foreach (var shift in shifts)
                {
                    ListShiftsVM listShiftsVM = new()
                    {
                        DriverFirstName = shift.DriverFirstName,
                        DriverLastName = shift.DriverLastName,
                        ModelName = shift.ModelName,
                        ShiftEnds = shift.ShiftEnd,
                        shiftIsActive = shift.ShiftIsActive,
                        ShiftIsAvailable = shift.ShiftIsAvailable,
                        ShiftLatitude = shift.ShiftLatitude,
                        ShiftLongitude = shift.ShiftLongitude,
                        ShiftStarts = shift.ShiftStart
                    };
                    listShifts.Add(listShiftsVM);
                }
                return listShifts;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        /// <summary>
        /// Start Shift When Driver Logs In
        /// </summary>
        /// <param name="driverEmail"></param>
        /// <returns></returns>
        public string StartShift(ToggleShiftVM driverEmail)
        {
            try
            {

                var driver = _taxiOperatorContext.Drivers.FirstOrDefault(d => d.DriverEmail == driverEmail.Email);
                if (driver != null)
                {
                    var shift = _taxiOperatorContext.Shifts.FirstOrDefault(s => s.DriverId == driver.DriverId);
                    if (shift != null)
                    {
                        shift.ShiftIsAvailable = true;
                        shift.ShiftIsActive = true;
                        _taxiOperatorContext.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Shift Does not Exist";
                    }
                }
                else
                {
                    return "Invalid Driver.";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Update Shift
        /// </summary>
        /// <param name="updateShiftVM"></param>
        /// <returns></returns>
        public string UpdateShift(AddShiftVM updateShiftVM)
        {
            try
            {

                var s = _taxiOperatorContext.Shifts.FirstOrDefault(shift => shift.DriverId == updateShiftVM.DriverId && shift.ShiftCabId == updateShiftVM.ShiftCabId);
                if (s != null)
                {
                    s.ShiftLongitude = updateShiftVM.ShiftLongitude;
                    s.ShiftLatitude = updateShiftVM.ShiftLatitude;
                    s.ShiftStart = updateShiftVM.ShiftStart;
                    s.ShiftEnd = updateShiftVM.ShiftEnd;
                    s.ShiftCabId = updateShiftVM.ShiftCabId;
                    s.DriverId = updateShiftVM.DriverId;
                    _taxiOperatorContext.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Invalid Shift";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        // Add shifts (Authorized Only)
        // View All Shifts

    }
}
