using EcomzExercise.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using EcomzExercise.Data.Models;
using System.Collections.Generic;
using System.Linq;
using EcomzExercise.Data.Models.View_Models;
using EcomzExercise.Data.Services;

namespace EcomzExercise.Worker_Services
{
    // This Background Service is used to Change Shift Coordinates
    // To simulate a Working driver 
    public class GenerateCoordinates : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;


        public GenerateCoordinates(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        private const int generalDelay = 5000; // millisseconds

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, stoppingToken);
               await GenerateRandomCoordinates();
            }
        }
        private Task GenerateRandomCoordinates()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<TaxiOperatorDbContext>();
                var _bugService = scope.ServiceProvider.GetRequiredService<IBugService>();
                try
                {
                    var shifts = _db.Shifts.ToList();
                    foreach (var shift in shifts)
                    {
                        Dictionary<string, decimal> coordinates = GenerateRandomCoordinates(33.888630, 35.49580, 1500); // Coordinates of beirut and a 50km Radius
                        shift.ShiftLatitude = coordinates["latitude"];
                        shift.ShiftLongitude = coordinates["longitude"];
                    }
                    _db.SaveChanges();
                }
                catch (WebException ex)
                {
                    BugListVM bug = _bugService.ExceptionToBug(ex);
                    _bugService.AddBug(bug);
                    Console.WriteLine(ex.Message);
                }
                
            }
            return Task.FromResult("Done");
        }

        // Generating Random Coordinates Within a Certain Radius
        private Dictionary<string, decimal> GenerateRandomCoordinates(double CenterLat, double CenterLmg, double radiusInMeters)
        {
            var y0 = CenterLat;
            var x0 = CenterLmg;
            var rd = radiusInMeters / 111300; 
            Random rand = new Random();
            var u = rand.NextDouble();
            var v = rand.NextDouble();

            var w = rd * Math.Sqrt(u);
            var t = 2 * Math.PI * v;
            var x = w * Math.Cos(t);
            var y = w * Math.Sin(t);

            var xp = x / Math.Cos(y0);

            Dictionary<string, decimal> coordinates = new Dictionary<string, decimal>();
            coordinates["latitude"] = (decimal)(y + y0);
            coordinates["longitude"] = (decimal)(xp + x0);
            return coordinates;
        }
    }
}
