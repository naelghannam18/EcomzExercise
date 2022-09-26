using EcomzExercise.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcomzExercise.Data.Services
{
    public class CabService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbontext;

        public CabService(TaxiOperatorDbContext taxiOperatorDbontext)
        {
            _taxiOperatorDbontext = taxiOperatorDbontext;
        }

        public List<Cab> ListCabs()
        {
            return _taxiOperatorDbontext.Cabs.ToList();
        }

        public List<CarModel> ListCabModels()
        {
            return _taxiOperatorDbontext.CarModels.ToList();
        }
    }
}
