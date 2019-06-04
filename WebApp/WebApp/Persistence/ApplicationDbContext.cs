using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<Driveline> DriveLines { get; set; }
        public DbSet<DrivingPlan> DrivingPlans { get; set; }
        public DbSet<PassengerTypeCoefficient> PassengerTypeCoefficients { get; set; }
        public DbSet<Pricelist> Pricelists { get; set; }
        public DbSet<PricelistItem> PricelistItems { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Departure> Departures { get; set; }

        public ApplicationDbContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}