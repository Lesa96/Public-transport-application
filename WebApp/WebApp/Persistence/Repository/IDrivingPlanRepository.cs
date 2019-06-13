using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface IDrivingPlanRepository : IRepository<DrivingPlan,int>
    {
        DrivingPlan GetSpecificDrivingPlan(DriveType driveType, WeekDays day, int lineNumber);
        HttpStatusCode DeleteDrivingPlan(int id);
        HttpStatusCode UpdateDrivingPlan(int id,int number, DriveType Type, WeekDays Day , ICollection<string> Departures);
    }
}
