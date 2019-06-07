using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class DrivelineRepository : Repository<Driveline, int>, IDrivelineRepository
    {

        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public DrivelineRepository(DbContext context) : base(context)
        {

        }

        public Driveline GetLineByNumber(int number)
        {
            return AppDbContext.DriveLines.Where(l => l.Number == number).FirstOrDefault();
        }

        public bool AddStationInLine(int drivelineId, int stationID)
        {
            var station = AppDbContext.Stations.Where(x => x.Id == stationID).FirstOrDefault();           
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();

            if (station == null || driveline == null)
            {
                return false;
            }


            driveline.Stations.Add(station);
            AppDbContext.SaveChanges();

            return true;

        }

        public bool DeleteStationInLine(int drivelineId, int stationID)
        {
            var station = AppDbContext.Stations.Where(x => x.Id == stationID).FirstOrDefault();
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();

            if (station == null || driveline == null)
            {
                return false;
            }

            driveline.Stations.Remove(station);
            AppDbContext.SaveChanges();

            return true;
        }

        public bool UpdateNumber(int drivelineId, int drivelineNumber)
        {
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();
            if ( driveline == null)
            {
                return false;
            }

            driveline.Number = drivelineNumber;

            AppDbContext.SaveChanges();

            return true;
        }

        public List<Driveline> GetAllDriveLines()
        {
            List<Driveline> drivelines = new List<Driveline>();
            foreach (Driveline dl in AppDbContext.DriveLines)
            {
                drivelines.Add(dl);
            }

            return drivelines;
        }

        public bool AddDriveline(int number, List<string> stationNames)
        {
            if(!AppDbContext.DriveLines.Any(d => d.Number == number))
            {
                Driveline dr = new Driveline() { Number = number };
                if (stationNames != null)
                {
                    foreach (string name in stationNames)
                    {
                        string[] n = name.Split(';');
                        string st = n[0];

                        dr.Stations.Add(AppDbContext.Stations.Where(s => s.Name == st).FirstOrDefault()); //dodaje stanice u liniju
                    }
                }

                AppDbContext.DriveLines.Add(dr);
                AppDbContext.SaveChanges();

                return true;

            }

            return false;
        }

        public bool DeleteDriveline(int number)
        {
            Driveline dr = AppDbContext.DriveLines.Where(x => x.Number == number).FirstOrDefault();
            if(dr != null)
            {
                AppDbContext.DriveLines.Remove(dr);
                AppDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}