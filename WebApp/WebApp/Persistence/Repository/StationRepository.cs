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

        public bool AddStation(string name, string addr, float x, float y)
        {
            Station st = AppDbContext.Stations.Where(s => s.Name == name).FirstOrDefault();
            if (st == null)
            {
                Coordinates co = new Coordinates() { CoordX = x, CoordY = y };
                AppDbContext.Coordinates.Add(co);
                AppDbContext.SaveChanges();

                Coordinates coFromBase = AppDbContext.Coordinates.Where(p => p.CoordX == x && p.CoordY == y).FirstOrDefault();
                st = new Station() { Name = name, Address = addr, CoordinatesId = coFromBase.CoordinatesId };
                AppDbContext.Stations.Add(st);
                AppDbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public List<string> GetStationsIdsAndNames()
        {
            List<string> res = new List<string>();

            foreach (Station s in AppDbContext.Stations)
            {
                res.Add("Id: " + s.Id + " Name: " + s.Name);
            }
            return res;
        }

        public UpdateStationInfoBindingModel GetStationsById(int id)
        {
            Station station = AppDbContext.Stations.Where(x => x.Id == id).FirstOrDefault();
            UpdateStationInfoBindingModel res;

            if (station != null)
            {
                res = new UpdateStationInfoBindingModel() { Id = id };
                res.Name = station.Name;
                res.Address = station.Address;
                Coordinates co = AppDbContext.Coordinates.Where(c => c.CoordinatesId == station.CoordinatesId).FirstOrDefault();
                res.X = co.CoordX;
                res.Y = co.CoordY;

                return res;
            }
            


            return null;
        }

        public List<UpdateStationInfoBindingModel> GetAllStations()
        {
            List<UpdateStationInfoBindingModel> stations = new List<UpdateStationInfoBindingModel>();
            List<Station> dbStations = AppDbContext.Stations.ToList();

            foreach (Station s in dbStations)
            {
                UpdateStationInfoBindingModel station = new UpdateStationInfoBindingModel()
                {
                    Id = s.Id, Name = s.Name , Address = s.Address
                };
                Coordinates cor = AppDbContext.Coordinates.Where(x => x.CoordinatesId == s.CoordinatesId).FirstOrDefault();

                station.X = cor.CoordX;
                station.Y = cor.CoordY;

                stations.Add(station);
            }

            return stations;
        }
    }
}