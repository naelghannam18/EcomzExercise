﻿using EcomzExercise.Data.Models;
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
                    ShiftEnd = addShiftVM.ShiftEnd,
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

                                   select s).ToList();

                if (takenShifts.Count == 0)
                {
                    _taxiOperatorContext.Add(newShift);
                    _taxiOperatorContext.SaveChanges();
                    return "Success";
                }

                else
                {
                    List<ShiftTimesVM> shiftTimesListVM = new List<ShiftTimesVM>();
                    foreach (var shift in takenShifts)
                    {
                        shiftTimesListVM.Add(new()
                        {
                            ShiftId = shift.Id,
                            StartingTime = shift.ShiftStart,
                            EndingTime = shift.ShiftEnd
                        });
                    }

                    Dictionary<int, bool> ShiftConflictDisct = new Dictionary<int, bool>();
                    foreach (var shift in shiftTimesListVM)
                    {
                        bool notconflict = NotConflict(shift.StartingTime, shift.EndingTime, addShiftVM.ShiftStart, addShiftVM.ShiftEnd);
                        if (!notconflict)
                        {
                            ShiftConflictDisct[shift.ShiftId] = notconflict;
                        }
                    }

                    if (ShiftConflictDisct.Count == 0)
                    {
                        _taxiOperatorContext.Add(newShift);
                        _taxiOperatorContext.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        string responseString = "Conflict with Shifts:\n";

                        foreach (var shift in ShiftConflictDisct)
                        {
                            responseString += shift.Key + "/n";
                        }
                        return responseString;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.ToString();
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
        /// List Shifts that are available
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ListShiftsVM> ListAvailableShifts()
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
                              where s.ShiftIsAvailable == true
                              && s.ShiftIsActive == true
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

        /// <summary>
        /// Check if a Shift Starting and Ending dates conflict with another shift Starting and ending dates;
        /// </summary>
        /// <param name="StartingDate"></param>
        /// <param name="EndingDate"></param>
        /// <param name="DateStartToCheck"></param>
        /// <param name="DateEndToCheck"></param>
        /// <returns></returns>
        private bool NotConflict(DateTime StartingDate, DateTime EndingDate, DateTime DateStartToCheck, DateTime DateEndToCheck)
        {
            // Todo
            return false;
        }

        private static class Range
        {
            public static IEnumerable<long> Int64(long from, long to, long step)
            {
                if (step <= 0L) step = (step == 0L) ? 1L : -step;

                if (from <= to)
                {
                    for (long l = from; l <= to; l += step) yield return l;
                }
                else
                {
                    for (long l = from; l >= to; l -= step) yield return l;
                }
            }

            public static IEnumerable<long> Int64(long from, long to)
            {
                return Range.Int64(from, to, 1L);
            }
        }

    }
}
