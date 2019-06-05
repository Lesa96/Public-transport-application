using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class DrivingPlanRepository : Repository<DrivingPlan, int>, IDrivingPlanRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public DrivingPlanRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<DrivingPlan> GetAllDrivingPlans()
        {
           return AppDbContext.DrivingPlans;
        }

        public DrivingPlan GetSpecificDrivingPlan(DriveType driveType, WeekDays day, int lineNumber)
        {
            return AppDbContext.DrivingPlans.Where(x => x.Type == driveType && x.Day == day && x.Driveline.Number == lineNumber).FirstOrDefault(); 
        }


    }
}