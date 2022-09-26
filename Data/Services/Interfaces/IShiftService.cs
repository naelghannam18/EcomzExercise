using EcomzExercise.Models.View_Models;
using System.Collections.Generic;

namespace EcomzExercise.Data.Services.Interfaces
{
    /// <summary>
    /// Shift Service Interface
    /// </summary>
    public interface IShiftService
    {
        /// <summary>
        /// Adding Shifts
        /// </summary>
        /// <param name="addShiftVM"></param>
        /// <returns></returns>
        public string AddShift(AddShiftVM addShiftVM);

        /// <summary>
        /// Updating Shifts
        /// </summary>
        /// <param name="updateShiftVM"></param>
        /// <returns></returns>
        public string UpdateShift(AddShiftVM updateShiftVM);

        /// <summary>
        /// Starting Shifts
        /// </summary>
        /// <param name="driverEmail"></param>
        /// <returns></returns>
        public string StartShift(ToggleShiftVM driverEmail);

        /// <summary>
        /// Ending Shifts
        /// </summary>
        /// <param name="driverEmail"></param>
        /// <returns></returns>
        public string EndShift(ToggleShiftVM driverEmail);

        /// <summary>
        /// Listing All Shifts
        /// </summary>
        /// <returns></returns>
        public List<ListShiftsVM> ListShifts();

        /// <summary>
        /// List Available Shifts
        /// </summary>
        /// <returns></returns>
        public List<ListShiftsVM> ListAvailableShifts();
    }
}
