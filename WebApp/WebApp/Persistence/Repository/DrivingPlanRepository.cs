using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Transactions;
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

        public HttpStatusCode DeleteDrivingPlan(int id)
        {

                try
                {
                    var drivingPlan = AppDbContext.DrivingPlans.Where(x => x.Id == id).FirstOrDefault();
                    if (drivingPlan == null)
                        return HttpStatusCode.NotFound;
                    AppDbContext.DrivingPlans.Remove(drivingPlan);
                    AppDbContext.SaveChanges();
                   

                    return HttpStatusCode.OK;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Trace.WriteLine("DbUpdateConcurrencyException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("NormalException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }





        }

        public HttpStatusCode UpdateDrivingPlan(int id, int number, DriveType Type, WeekDays Day, ICollection<string> Departures)
        {          
                try
                {
                    var line = AppDbContext.DriveLines.Where(x => x.Number == number).FirstOrDefault();
                    if(line == null)
                    {
                        return HttpStatusCode.NotFound;
                    }
                    string departures = "";

                    foreach (var departure in Departures.OrderBy(d => d, StringComparer.Ordinal))
                    {
                        departures += departure + ";";
                    }

                    DrivingPlan drivingPlan = AppDbContext.DrivingPlans.Where(x => x.Id == id).FirstOrDefault();

                    drivingPlan.Type = Type;
                    drivingPlan.Day = Day;
                    drivingPlan.DrivelineId = line.Id;
                    drivingPlan.Departures = departures;

                    AppDbContext.DrivingPlans.Add(drivingPlan);
                    AppDbContext.SaveChanges();

                    return HttpStatusCode.OK;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Trace.WriteLine("DbUpdateConcurrencyException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("NormalException Message: {0}", ex.Message);
                    return HttpStatusCode.Conflict;
                }


        }
    }
}