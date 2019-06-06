using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class StationRepository : Repository<Station, int>, IStationRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public StationRepository(DbContext context) : base(context)
        {
        }

        public List<string> GetStationNames()
        {
            List<string> stationNames = new List<string>();

            foreach (Station s in AppDbContext.Stations)
            {
                stationNames.Add(s.Name + ';');
            }
            return stationNames;
        }
    }
}