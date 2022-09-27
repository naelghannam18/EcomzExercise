using EcomzExercise.Data.Models;
using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcomzExercise.Data.Services
{
    public class AddressService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbContext;
        private readonly IBugService _bugService;

        public AddressService(TaxiOperatorDbContext taxiOperatorDbContext, IBugService bugService)
        {
            _taxiOperatorDbContext = taxiOperatorDbContext;
            _bugService = bugService;
        }

        public List<ListCountryVM> ListCountries()
        {
            try
            {
                var c = _taxiOperatorDbContext.Countries.ToList();
                List<ListCountryVM> countries = new List<ListCountryVM>();

                foreach (var country in c)
                {
                    ListCountryVM listCountryVM = new()
                    {
                        CountryInitials = country.CountryInitials,
                        CountryName = country.CountryName,
                    };
                    countries.Add(listCountryVM);
                }
                return countries;
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<ListCityVM> ListCities()
        {
            try
            {
                var c = _taxiOperatorDbContext.Cities.ToList();
                List<ListCityVM> listCityVMs = new List<ListCityVM>();
                foreach (var city in c)
                {
                    ListCityVM listCityVM = new()
                    {
                        CityName = city.CityName,
                        CountryId = city.CountryId,
                    };
                    listCityVMs.Add(listCityVM);
                }
                return listCityVMs;
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<AddAddressVM> ListAllAddresses()
        {
            try
            {
                var a = _taxiOperatorDbContext.Addresses.ToList();
                List<AddAddressVM> addAddressVMs = new List<AddAddressVM>();

                foreach (var address in a)
                {
                    AddAddressVM addAddressVM = new()
                    {
                        AddressStreetName = address.AddressStreetName,
                        AddressStreetNumber = address.AddressStreetNumber,
                        AddressTypeId = address.AddressTypeId,
                        AddressZipPostal = address.AddressZipPostal,
                        CityId = address.CityId,
                        CustomerId = address.CustomerId,

                    };
                    addAddressVMs.Add(addAddressVM);
                }
                return addAddressVMs;
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<ListAddressTypeVM> ListAddressTypes()

        {
            try
            {
                var at =  _taxiOperatorDbContext.AddressTypes.ToList();
                List<ListAddressTypeVM> listAddressTypeVMs = new List<ListAddressTypeVM>();

                foreach(var addresstype in at)
                {
                    listAddressTypeVMs.Add(new ListAddressTypeVM()
                    {
                         AddressTypeName = addresstype.AddressTypeDescription
                    });

                }
                return listAddressTypeVMs;
            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<AddAddressVM> ListUserAddresses(string customerEmail)
        {
            try
            {

                var customer = _taxiOperatorDbContext.Customers.FirstOrDefault(c => c.CustomerEmail == customerEmail);
                if (customer != null)
                {
                    var addresses = (from a in _taxiOperatorDbContext.Addresses
                                     where a.CustomerId == customer.Id
                                     select a
                                     ).ToList();
                    if (addresses != null)
                    {
                        List<AddAddressVM> list = new List<AddAddressVM>();
                        foreach (var address in addresses)
                        {
                            list.Add(new AddAddressVM()
                            {
                                AddressStreetName = address.AddressStreetName,
                                AddressStreetNumber = address.AddressStreetNumber,
                                AddressTypeId = address.AddressTypeId,
                                AddressZipPostal = address.AddressZipPostal,
                                CityId = address.CityId,
                                CustomerId = address.CustomerId,
                            });
                        }
                        return list;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    Console.WriteLine("User Does Not Exist");
                    return null;
                }

            }
            catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string AddAddress(AddAddressVM address)
        {
            try
            {
                Address add = new()
                {
                    AddressStreetName = address.AddressStreetName,
                    AddressStreetNumber = address.AddressStreetNumber,
                    AddressTypeId = address.AddressTypeId,
                    AddressZipPostal = address.AddressZipPostal,
                    CityId = address.CityId,
                    CustomerId = address.CustomerId,
                };
                _taxiOperatorDbContext.Add(add);
                _taxiOperatorDbContext.SaveChanges();
                return "Success";

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
