using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class DepartureRepository : Repository<Departure, int>, IDepartureRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public DepartureRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Departure> GetDepartures(int lineNumber, DriveType driveType, WeekDays drivePlanDay)
        {
            Driveline driveline = AppDbContext.DriveLines.Where(x => x.Number == lineNumber).FirstOrDefault();

            IEnumerable<Departure> departures =  driveline.DrivingPlan.Departures.Where(dep => dep.DrivelineId == driveline.Id);

            return departures;
            
        }

        
    }
}