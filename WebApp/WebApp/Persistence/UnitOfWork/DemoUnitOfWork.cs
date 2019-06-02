using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IPricelistRepository Pricelists { get; set; }
        [Dependency]
        public IPricelistItemRepository PricelistItems { get; set; }
        [Dependency]
        public ITicketRepository Tickets { get; set; }
        [Dependency]
        public IStationRepository Stations { get; set; }
        [Dependency]
        public IDrivelineRepository Drivelines { get ; set ; }
        [Dependency]
        public IDrivingPlanRepository DrivingPlans { get ; set; }
        [Dependency]
        public IPassengerTypeCoefficientRepository PassengerTypeCoefficients { get; set; }
        [Dependency]
        public IApplicationUserRepository ApplicationUsers { get; set; }
        [Dependency]
        public ICoordinatesRepository CoordinatesRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}