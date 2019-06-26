using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Transactions;
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
            if (driveline == null)
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
            if (!AppDbContext.DriveLines.Any(d => d.Number == number))
            {
                Driveline dr = new Driveline() { Number = number };
                if (stationNames != null)
                {
                    foreach (string name in stationNames)
                    {


                        dr.Stations.Add(AppDbContext.Stations.Where(s => s.Name == name).FirstOrDefault()); //dodaje stanice u liniju
                    }
                }

                AppDbContext.DriveLines.Add(dr);
                AppDbContext.SaveChanges();

                return true;

            }

            return false;
        }

        public HttpStatusCode DeleteDriveline(int number)
        {
                try
                {
                    Driveline dr = AppDbContext.DriveLines.Where(x => x.Number == number).FirstOrDefault();
                    if (dr != null)
                    {
                        AppDbContext.DriveLines.Remove(dr);
                        AppDbContext.SaveChanges();
                        
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        return HttpStatusCode.NotFound;
                    }
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

        public List<string> GetDrivelineNumbersAndIds()
        {
            List<string> drLines = new List<string>();

            foreach (Driveline dr in AppDbContext.DriveLines)
            {
                string num = "Id: " + dr.Id.ToString() + " Number: " + dr.Number.ToString();
                drLines.Add(num);
            }

            return drLines;
        }

        public Driveline GetLineById(int id)
        {
            Driveline dr = AppDbContext.DriveLines.Where(x => x.Id == id).FirstOrDefault();
            return dr;
        }

        public string[] GetDrivelineStationsNames(int id)
        {
            int numberOfStations = 0;
            Driveline dr = AppDbContext.DriveLines.Where(x => x.Id == id).FirstOrDefault();
            numberOfStations = dr.Stations.Count;
            string[] stations = new string[numberOfStations];

            int i = 0;
            foreach (Station s in dr.Stations)
            {
                stations[i] = s.Name;
                i++;
            }

            return stations;
        }

        public HttpStatusCode UpdateDriveline(int id, int number, List<string> stationNames, byte[] rowVersion)
        {
            
            try
            {
                Driveline dr = AppDbContext.DriveLines.Where(x => x.Id == id).FirstOrDefault();
                if (dr != null)
                {
                    for (int i = 0; i < dr.RowVersion.Count(); i++)
                    {
                        if (dr.RowVersion[i] != rowVersion[i])
                            return HttpStatusCode.Conflict;
                    }
                    dr.Number = number;
                    dr.Stations.Clear();

                    if (stationNames != null)
                    {
                        foreach (string name in stationNames)
                        {
                            dr.Stations.Add(AppDbContext.Stations.Where(s => s.Name == name).FirstOrDefault()); //dodaje stanice u liniju
                        }
                    }
                    AppDbContext.SaveChanges();
                        
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.NotFound;
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