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
        IApplicationUserRepository ApplicationUsers { get; set; }
        ICoordinatesRepository CoordinatesRepository { get; set; }
        IDrivelineRepository Drivelines { get; set; }
        IDrivingPlanRepository DrivingPlans { get; set; }
        IPassengerTypeCoefficientRepository PassengerTypeCoefficients { get; set; }
        IPricelistItemRepository PricelistItems { get; set; }
        IPricelistRepository Pricelists { get; set; }
        IStationRepository Stations { get; set; }
        ITicketRepository Tickets { get; set; }

        int Complete();
    }
}
