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
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") , BirthDate = DateTime.Now };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!"), BirthDate = DateTime.Now };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            if(!context.DriveLines.Any(d => d.Number == 4))
            {
                var drLine = new Driveline() { Number = 4 };
                context.DriveLines.Add(drLine);
                context.SaveChanges();
            }

            if(!context.DrivingPlans.Any(p => p.Departures.Equals("4: 50 ; 10:30")))
            {
                DrivingPlan drivingPlan = new DrivingPlan() { Day = Models.Enums.WeekDays.Monday, Type = Models.Enums.DriveType.City, Departures = "4: 50 ; 10:30", DrivelineId = context.DriveLines.Where( l => l.Number == 4).FirstOrDefault().Id };
                context.DrivingPlans.Add(drivingPlan);
                context.SaveChanges();
            }

           // InitialDBAdding(context); //dodaje neke pocetne vrednosti u bazi 
        }

        private void InitialDBAdding(WebApp.Persistence.ApplicationDbContext context)
        {
            Coordinates coordinates = new Coordinates() { CoordinatesId = 1,CoordX = 1, CoordY = 1 };

            DrivingPlan drivingPlan = new DrivingPlan() {Id = 1, Day = Models.Enums.WeekDays.Monday, Type = Models.Enums.DriveType.City , Departures ="4: 50 ; 10:30" };
            
            
            Station station = new Station() {Id = 1, Address = "TestAddress", CoordinatesId = 1, Name = "TestName" };
            Driveline driveline = new Driveline() {Id = 1, Number = 1 };
            driveline.DrivingPlans.Add(drivingPlan);
            driveline.Stations.Add(station);
            station.Drivelines.Add(driveline);


            Pricelist pricelist = new Pricelist() {PricelistId = 1, ValidFrom = DateTime.Now, ValidUntil = DateTime.Now.AddDays(2) };
            PricelistItem pricelistItem = new PricelistItem() {PricelistItemId = 1, TicketType = Models.Enums.TicketType.Daily, Price = 200, PricelistId = 1, PassengerType = Models.Enums.PassengerType.Regular };
            PassengerTypeCoefficient passengerTypeCoefficient = new PassengerTypeCoefficient() {PassengerTypeCoefficientId = 1, Coefficient = 0.9F, PassengerType = Models.Enums.PassengerType.Regular };
           // pricelist.PricelistItems.Add(pricelistItem);  //ovde je neki problem, treba ovo ispitati

            Ticket ticket = new Ticket() {TicketId = 1, IsCanceled = false, TicketInfoId = 1, TimeOfPurchase = DateTime.Now };

            context.Coordinates.AddOrUpdate(coordinates);
            context.DrivingPlans.AddOrUpdate(drivingPlan);
            context.Stations.AddOrUpdate(station);
            context.DriveLines.AddOrUpdate(driveline);
            
            context.Pricelists.AddOrUpdate(pricelist);
            context.PricelistItems.AddOrUpdate(pricelistItem);
            
            context.PassengerTypeCoefficients.AddOrUpdate(passengerTypeCoefficient);
            context.Tickets.AddOrUpdate(ticket);

            context.SaveChanges();
            
            
        }
    }
}
