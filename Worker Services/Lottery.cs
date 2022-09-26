using EcomzExercise.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EcomzExercise.Worker_Services
{
    /// <summary>
    /// Lottery Background Service
    /// Checks every 24 for users with points > 200 then deducts the points and creates a Cupon code
    /// </summary>
    public class Lottery : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;


        public Lottery(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        private const int generalDelay = 1000 * 60 * 60 * 24; // 24 hours

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, stoppingToken);
                await CheckForCupons();
            }
        }
        private Task CheckForCupons()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<TaxiOperatorDbContext>();
                try
                {
                    var customers = _db.Customers.ToList();
                    List<Cupon> cupons = new List<Cupon>();
                    foreach (var customer in customers)
                    {
                        if (customer.CustomerPoints >= 200)
                        {
                            customer.CustomerPoints -= 200;
                            Cupon cupon = new()
                            {
                                CuponCustomerId = customer.Id,
                                CuponDateIssued = DateTime.Now,
                                CuponDateExpiry = DateTime.Now.AddMonths(1),
                                CuponDiscount = GenerateRandomDiscount(),
                                CuponCode = GenerateCuponCode(),
                            };
                            cupons.Add(cupon);
                        }
                        foreach (var x in cupons)
                        {
                            _db.Cupons.Add(x);
                        }
                        _db.SaveChanges();

                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return Task.FromResult("Done");
        }

        private string GenerateCuponCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        private int GenerateRandomDiscount()
        {
            Random rand = new Random();
            return rand.Next(0, 101);
        }
    }
}
