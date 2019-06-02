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

        public bool AddStationInLine(int drivelineId , int stationID)
        {
            var station = AppDbContext.Stations.Where(x => x.Id == stationID).FirstOrDefault();
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();

            driveline.Stations.Add(station);
            AppDbContext.SaveChanges();

            return true;

        }

        public bool DeleteStationInLine(int drivelineId, int stationID)
        {
            var station = AppDbContext.Stations.Where(x => x.Id == stationID).FirstOrDefault();
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();

            driveline.Stations.Remove(station);
            AppDbContext.SaveChanges();

            return true;
        }

        public bool UpdateNumber(int drivelineId, int drivelineNumber)
        {
            var driveline = AppDbContext.DriveLines.Where(x => x.Id == drivelineId).FirstOrDefault();
            driveline.Number = drivelineNumber;

            AppDbContext.SaveChanges();

            return true;
        }
    }
}