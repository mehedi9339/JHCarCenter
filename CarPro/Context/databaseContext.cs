using CarPro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPro.Context
{
    public class databaseContext:DbContext
    {
        public databaseContext(DbContextOptions<databaseContext> options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<VehicleType> VehicleTypes { get; set; }
        //public DbSet<Year> Years { get; set; }
        //public DbSet<Colour> Colours { get; set; }
        //public DbSet<CarPro.Models.OfferVm> OfferVm { get; set; }
    }
}
