using EcomzExercise.Data.Models;
using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using static EcomzExercise.Models.View_Models.CustomerVM;

namespace EcomzExercise.Data.Services
{
    public class RideService : IRideService
    {
        private readonly TaxiOperatorDbContext _taxiOperatorDbContext;
        private readonly ICustomerService _customerService;
        private readonly IBugService _bugService;

        public RideService(TaxiOperatorDbContext taxiOperatorDbContext, ICustomerService customerService, IBugService bugService)
        {
            _taxiOperatorDbContext = taxiOperatorDbContext;
            _customerService = customerService;
            _bugService = bugService;
        }


        public string RequestRide(RequestRideVM requestRideVM)
        {
            try
            {

                var isShiftAvailableAndActive =
                _taxiOperatorDbContext.Shifts.FirstOrDefault(
                    s => s.Id == requestRideVM.ShiftId &&
                    s.ShiftIsAvailable == true && s.ShiftIsActive == true);
                if (isShiftAvailableAndActive != null)
                {
                    string cuponCode = requestRideVM.CuponCode;
                    Cupon cupon = _taxiOperatorDbContext.Cupons.FirstOrDefault(c =>
                    c.CuponCustomerId == requestRideVM.CustomerId &&
                    c.CuponCode == cuponCode &&
                    c.CuponDateExpiry >= DateTime.Now
                    );
                    decimal discount = cupon == null ? 0 : cupon.CuponDiscount;
                    Pricing pricing = _taxiOperatorDbContext.Pricings.FirstOrDefault(p => p.Id == requestRideVM.PricingId);
                    PaymentType paymentType = _taxiOperatorDbContext.PaymentTypes.FirstOrDefault(pt => pt.Id == requestRideVM.RidePaymentType);
                    decimal pricingPerKm = pricing.PricingPerKm;
                    decimal distance = GetDistanceGoogleMaps(requestRideVM.StartingLatitude, requestRideVM.StartingLongitude, requestRideVM.EndingLatitude, requestRideVM.EndingLongitude);
                    decimal ridePrice = pricingPerKm * distance - (pricingPerKm * distance) * (discount / 100);

                    Ride newRide = new()
                    {
                        RideShiftId = isShiftAvailableAndActive.Id,
                        RideStartTime = DateTime.Now,
                        RideEndTime = null,
                        RideStartingAddress = requestRideVM.RideStartingAddress,
                        RideDestinationAddress = requestRideVM.RideEndingAddress,
                        RideCanceled = false,
                        RideDone = false,
                        RideRewardPoints = (int)(distance * 1), // each km earns a point,
                        RidePrice = ridePrice,
                        RidePaymentType = paymentType.Id,
                        RideCuponId = cupon != null ? cupon.Id : null,
                        RideStartingLatitude = requestRideVM.StartingLatitude,
                        RideStartingLongitude = requestRideVM.StartingLongitude,
                        RideEndingLatitude = requestRideVM.EndingLatitude,
                        RideEndingLongitude = requestRideVM.EndingLongitude,
                        RideDistance = distance,
                        RidePricingId = pricing.Id,
                        RideCustomerId = requestRideVM.CustomerId
                    };
                    isShiftAvailableAndActive.ShiftIsAvailable = false;
                    _taxiOperatorDbContext.Rides.Add(newRide);
                    _taxiOperatorDbContext.SaveChanges();
                    return "Success";

                }
                else
                {
                    return "No Shifts Are Available At this Time.";
                }

            } catch(Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
            
        }

        public decimal GetDistanceGoogleMaps(decimal StartingLat, decimal startingLong, decimal endingLat, decimal endingLong)
        {
            try
            {
                string uri = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + StartingLat.ToString() + "%2C" + startingLong.ToString() + "&destinations="+ endingLat.ToString()+ "%2C"+ endingLong.ToString() + "&key={API_KEY}";
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        RideDistanceAndTimeVM response = JsonConvert.DeserializeObject<RideDistanceAndTimeVM>(result);
                        var rows = response.rows;
                        List<decimal> distances = new List<decimal>();
                        List<long> timesInSeconds = new List<long>();
                        foreach (var row in rows)
                        {
                            foreach (var element in row.elements)
                            {
                                distances.Add(element.distance.value / 1000);
                                timesInSeconds.Add((long)element.duration.value);
                            }
                        }
                        int bestTimeIndex = timesInSeconds.IndexOf(timesInSeconds.Max());
                        decimal bestDistance = distances[bestTimeIndex];
                        return bestDistance;

                    }
                    else
                    {
                        return 0;
                    }
                }
            } catch(Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public string CancelRide(int rideId)
        {
            try
            {

                var ride = _taxiOperatorDbContext.Rides.FirstOrDefault(r => r.Id == rideId);
                if (ride!=null)
                {
                    var shift = _taxiOperatorDbContext.Shifts.FirstOrDefault(s => s.Id == ride.RideShiftId);
                    ride.RideCanceled = true;
                    shift.ShiftIsAvailable = true;
                    return "Ride Cancelled.";
                }
                else
                {
                    return "Ride Does Not Exist";
                }


            }catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }

        public string RideDone(int rideId)
        {
            try
            {
                var ride = _taxiOperatorDbContext.Rides.FirstOrDefault(r => r.Id == rideId);
                if(ride != null)
                {
                    var customer = _taxiOperatorDbContext.Customers.FirstOrDefault(c => c.Id == ride.RideCustomerId);
                    ride.RideDone = true;
                    ride.RideEndTime = DateTime.Now;

                    AddCustomerPointsVM addCustomerPointsVM = new()
                    {
                        Email = customer.CustomerEmail,
                        Points = ride.RideRewardPoints,
                    };
                    _customerService.AddCustomerPoints(addCustomerPointsVM);
                    var shift = _taxiOperatorDbContext.Shifts.FirstOrDefault(s => s.Id == ride.RideShiftId);
                    shift.ShiftIsAvailable = true;
                    _taxiOperatorDbContext.SaveChanges();
                    return "Ride Successful.";
                }
                else
                {
                    return "Invalid Ride.";
                }
            }catch (Exception ex)
            {
                BugListVM bug = _bugService.ExceptionToBug(ex);
                _bugService.AddBug(bug);
                return ex.Message;
            }
        }
    }
}
