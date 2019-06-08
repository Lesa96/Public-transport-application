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

        public string[] GetStationNames()
        {
            int numberOfStations = 0;
            foreach (Station s in AppDbContext.Stations)
            {
                numberOfStations++;
            }

            string[] stationNames = new string[numberOfStations];

            int i = 0;
            foreach (Station s in AppDbContext.Stations)
            {
                stationNames[i] = (s.Name);
                i++;
            }
            return stationNames;
        }

        public bool DeleteStationByName(string name)
        {
            Station st = AppDbContext.Stations.Where(x => x.Name == name).FirstOrDefault();
            if (st != null)
            {
                AppDbContext.Stations.Remove(st);
                AppDbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}