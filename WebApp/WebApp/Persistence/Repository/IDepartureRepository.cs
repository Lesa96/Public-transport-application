using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface IDepartureRepository : IRepository<Departure, int>
    {
        IEnumerable<Departure> GetDepartures(int lineNumber, DriveType driveType, WeekDays drivePlanDay);
    }
}
