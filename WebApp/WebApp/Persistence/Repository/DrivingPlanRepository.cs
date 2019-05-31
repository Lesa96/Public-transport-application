using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

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
    }
}