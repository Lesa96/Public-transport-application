namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }


            InitialDBAdding(context); //dodaje neke pocetne vrednosti u bazi 
        }

        private void InitialDBAdding(WebApp.Persistence.ApplicationDbContext context)
        {
            Coordinates coordinates = new Coordinates() { CoordinatesId = 1,CoordX = 1, CoordY = 1 };

            Station station = new Station() {Id = 1, Address = "Prva Adresa", CoordinatesId = coordinates.CoordinatesId, Name = "Prva stanica" };
            Driveline driveline = new Driveline() {Id = 1, Number = 1 };
            driveline.Stations.Add(station);
            station.Drivelines.Add(driveline);

            DrivingPlan drivingPlan = new DrivingPlan() {Id = 1, Day = Models.Enums.WeekDays.Monday, Type = Models.Enums.DriveType.City };
            drivingPlan.Line.Add(driveline);

            Pricelist pricelist = new Pricelist() {PricelistId = 1, ValidFrom = DateTime.Now, ValidUntil = DateTime.Now.AddDays(2) };
            PricelistItem pricelistItem = new PricelistItem() {PricelistItemId = 1, TicketType = Models.Enums.TicketType.Daily, Price = 200, PricelistId = pricelist.PricelistId };
            PassengerTypeCoefficient passengerTypeCoefficient = new PassengerTypeCoefficient() {PassengerTypeCoefficientId = 1, Coefficient = 0.9F, PassengerType = Models.Enums.PassengerType.Regular };
            pricelistItem.PassengerTypeCoefficientId = passengerTypeCoefficient.PassengerTypeCoefficientId;
           // pricelist.PricelistItems.Add(pricelistItem);  //ovde je neki problem, treba ovo ispitati

            Ticket ticket = new Ticket() {TicketId = 1, IsCanceled = false, TicketInfoId = pricelistItem.PricelistItemId, TimeOfPurchase = DateTime.Now };

            context.Coordinates.AddOrUpdate(coordinates);
            context.Stations.AddOrUpdate(station);
            context.DriveLines.AddOrUpdate(driveline);
            context.DrivingPlans.AddOrUpdate(drivingPlan);
            
            context.PricelistItems.AddOrUpdate(pricelistItem);
            
            context.PassengerTypeCoefficients.AddOrUpdate(passengerTypeCoefficient);
            context.Pricelists.AddOrUpdate(pricelist);
            context.Tickets.AddOrUpdate(ticket);

            context.SaveChanges();
            
            
        }
    }
}
