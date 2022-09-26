using EcomzExercise.Data.Models;
using EcomzExercise.Data.Services.Interfaces;

namespace EcomzExercise.Data.Services
{
    public class RideService : IRideService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbContext;

        public RideService(TaxiOperatorDbContext taxiOperatorDbContext)
        {
            _taxiOperatorDbContext = taxiOperatorDbContext;
        }
    }
}
