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
            if (!context.Users.Any(u => u.UserName == "sale@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "sale", UserName = "sale@yahoo.com", Email = "sale@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Sale123!"), BirthDate = DateTime.Now };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!"), BirthDate = DateTime.Now };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            

             InitialDBAdding(context); //dodaje neke pocetne vrednosti u bazi 
        }

        private void InitialDBAdding(WebApp.Persistence.ApplicationDbContext context)
        {
            if(!context.Coordinates.Any(c => c.CoordinatesId == 1)) // kada budemo napravili server ovo ce biti nepotrebno
            {
                Coordinates c = new Coordinates() { CoordX = 1, CoordY = 1 };
                context.Coordinates.Add(c);
                context.SaveChanges();
            }
            if (!context.Coordinates.Any(c => c.CoordinatesId == 2)) // kada budemo napravili server ovo ce biti nepotrebno
            {
                Coordinates c = new Coordinates() { CoordX = 10, CoordY = 10 };
                context.Coordinates.Add(c);
                context.SaveChanges();
            }

            if (!context.DriveLines.Any(d => d.Number == 4))
            {
                var drLine = new Driveline() { Number = 4 };
                context.DriveLines.Add(drLine);
                context.SaveChanges();
            }

            if (!context.DriveLines.Any(d => d.Number == 7))
            {
                var drLine = new Driveline() { Number = 7 };
                context.DriveLines.Add(drLine);
                context.SaveChanges();
            }

            if (!context.DrivingPlans.Any(p => p.Departures.Equals("4: 50 ; 10:30")))
            {
                DrivingPlan drPlan = new DrivingPlan() { Day = Models.Enums.WeekDays.Monday, Type = Models.Enums.DriveType.City, Departures = "4: 50 ; 10:30", DrivelineId = context.DriveLines.Where(l => l.Number == 4).FirstOrDefault().Id };
                context.DrivingPlans.Add(drPlan);
                context.SaveChanges();
            }

            if (!context.DrivingPlans.Any(p => p.Departures.Equals("10:00 ; 11:00 ; 12:00")))
            {
                DrivingPlan drPlan = new DrivingPlan() { Day = Models.Enums.WeekDays.Monday, Type = Models.Enums.DriveType.City, Departures = "10:00 ; 11:00 ; 12:00", DrivelineId = context.DriveLines.Where(l => l.Number == 7).FirstOrDefault().Id };
                context.DrivingPlans.Add(drPlan);
                context.SaveChanges();
            }
            if(!context.Stations.Any(s => s.Name == "FirstStation"))
            {
                Station s = new Station() { Name = "FirstStation", Address = "Bulevar Oslobodjenja 1" };
                s.CoordinatesId = context.Coordinates.Where(c => c.CoordinatesId == 1).FirstOrDefault().CoordinatesId;
                context.Stations.Add(s);
                context.SaveChanges();
            }
            if (!context.Stations.Any(s => s.Name == "SecondStation"))
            {
                Station s = new Station() { Name = "SecondStation", Address = "Bulevar Oslobodjenja 10" };
                s.CoordinatesId = context.Coordinates.Where(c => c.CoordinatesId == 2).FirstOrDefault().CoordinatesId;
                context.Stations.Add(s);
                context.SaveChanges();
            }
            if (!context.Pricelists.Any(p => p.PricelistId == 1)) //necemo po ID-u , ali posto je samo test onda je ok
            {
                Pricelist pr = new Pricelist() { ValidFrom = DateTime.Now, ValidUntil = DateTime.Now.AddDays(2) };
                context.Pricelists.Add(pr);
                context.SaveChanges();
            }
            if(!context.PricelistItems.Any(p => p.TicketType == Models.Enums.TicketType.Daily && p.PassengerType == Models.Enums.PassengerType.Regular))
            {
                PricelistItem prI = new PricelistItem() { TicketType = Models.Enums.TicketType.Daily, Price = 200, PassengerType = Models.Enums.PassengerType.Regular };
                Pricelist pr = context.Pricelists.Where(p => p.PricelistId == 1).FirstOrDefault();
                prI.PricelistId = pr.PricelistId;
                context.PricelistItems.Add(prI);
                context.SaveChanges();

                pr.PricelistItems.Add(prI);
                context.PricelistItems.AddOrUpdate(prI);
                context.SaveChanges();
            }
            if(!context.PassengerTypeCoefficients.Any(p=> p.PassengerType == Models.Enums.PassengerType.Regular))
            {
                PassengerTypeCoefficient pas = new PassengerTypeCoefficient() {  Coefficient = 1F, PassengerType = Models.Enums.PassengerType.Regular };
                context.PassengerTypeCoefficients.Add(pas);
                context.SaveChanges();
            }
            if (!context.PassengerTypeCoefficients.Any(p => p.PassengerType == Models.Enums.PassengerType.Pensioner))
            {
                PassengerTypeCoefficient pas = new PassengerTypeCoefficient() { Coefficient = 0.9F, PassengerType = Models.Enums.PassengerType.Pensioner };
                context.PassengerTypeCoefficients.Add(pas);
                context.SaveChanges();
            }
            if (!context.PassengerTypeCoefficients.Any(p => p.PassengerType == Models.Enums.PassengerType.Student))
            {
                PassengerTypeCoefficient pas = new PassengerTypeCoefficient() { Coefficient = 0.8F, PassengerType = Models.Enums.PassengerType.Student };
                context.PassengerTypeCoefficients.Add(pas);
                context.SaveChanges();
            }
            
        }
    }
}
