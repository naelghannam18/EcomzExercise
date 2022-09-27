using EcomzExercise.Data.Models;
using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcomzExercise.Data.Services
{
    public class BugService : IBugService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbContext;

        public BugService(TaxiOperatorDbContext taxiOperatorDbContext)
        {
            _taxiOperatorDbContext = taxiOperatorDbContext;
        }

        public void AddBug(BugListVM bug)
        {
            var bugs = (from b in _taxiOperatorDbContext.Bugs
                        where b.ErrorMessage == bug.ErrorMessage &&
                        b.ErrorStacktrace == bug.ErrorStacktrace
                        select b).FirstOrDefault();
            if (bug == null)
            {
                _taxiOperatorDbContext.Bugs.Add(new()
                {
                    ErrorSource = bug.ErrorSource,
                    ErrorMessage = bug.ErrorMessage,
                    ErrorStacktrace = bug.ErrorStacktrace,
                    ErrorInnerException = bug.ErrorInnerException,
                    ErrorTargetSite = bug.ErrorTargetSite
                });
                _taxiOperatorDbContext.SaveChanges();
            }
        }

        public BugListVM ExceptionToBug(Exception ex)
        {
            return new()
            {
                ErrorSource = ex.Source,
                ErrorInnerException = ex.InnerException != null ? ex.InnerException.ToString() : null,
                ErrorStacktrace = ex.StackTrace,
                ErrorMessage = ex.Message,
                ErrorTargetSite = ex.TargetSite.ToString()

            };
        }

        public List<BugListVM> ListBugs()
        {
            try
            {
                var bugs = _taxiOperatorDbContext.Bugs.ToList();
                List<BugListVM> bugList = new List<BugListVM>();
                foreach (var bug in bugs)
                {

                    bugList.Add(new()
                    {
                        ErrorSource = bug.ErrorSource,
                        ErrorMessage = bug.ErrorMessage,
                        ErrorStacktrace = bug.ErrorStacktrace,
                        ErrorInnerException = bug.ErrorInnerException,
                        ErrorTargetSite = bug.ErrorTargetSite
                    });

                }
                return bugList;
            }
            catch (Exception ex)
            {
                BugListVM bug = ExceptionToBug(ex);
                AddBug(bug);
                return null;
            }
        }


    }
}
