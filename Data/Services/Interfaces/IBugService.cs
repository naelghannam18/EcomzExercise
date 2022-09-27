using EcomzExercise.Data.Models.View_Models;
using System;
using System.Collections.Generic;

namespace EcomzExercise.Data.Services.Interfaces
{
    public interface IBugService
    {
        /// <summary>
        /// List All Bugs
        /// </summary>
        /// <returns></returns>
        public List<BugListVM> ListBugs();


        /// <summary>
        /// Adds a bug on catch clause
        /// </summary>
        /// <param name="bug"></param>
        public void AddBug(BugListVM bug);

        /// <summary>
        /// Converts Exception to Bug Model
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public BugListVM ExceptionToBug(Exception ex);

    }
}
