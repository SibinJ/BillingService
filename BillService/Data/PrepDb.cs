using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BillService.Models;

namespace BillService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            
            if(!context.Bills.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Bills.AddRange(
                    new Bill() {Name="Phil", BillDate="26-Oct-2023", BillAmount="Rs 2315"},
                    new Bill() {Name="Cameron", BillDate="30-Oct-2023",  BillAmount="Rs 4200"},
                    new Bill() {Name="Jay", BillDate="02-Nov-2023",  BillAmount="Rs 1560"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}