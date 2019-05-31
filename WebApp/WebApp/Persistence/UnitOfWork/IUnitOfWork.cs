using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPricelistRepository Pricelists { get; set; }
        IPricelistItemRepository PricelistItems { get; set; }
        ITicketRepository Tickets { get; set; }
        IPassengerTypeCoefficientRepository PassengerTypeCoefficients { get; set; }
        IApplicationUserRepository ApplicationUsers { get; set; }

        IStationRepository Stations { get; set; }
        IDrivelineRepository Drivelines { get; set; }
        IDrivingPlanRepository DrivingPlans { get; set; }

        int Complete();
    }
}
