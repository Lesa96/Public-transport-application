﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Transactions;
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

        public HttpStatusCode DeleteStationByName(string name)
        {          
                try
                {
                    Station st = AppDbContext.Stations.Where(x => x.Name == name).FirstOrDefault();
                    if (st != null)
                    {
                        AppDbContext.Stations.Remove(st);

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

        public HttpStatusCode AddStation(string name, string addr, float x, float y)
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

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
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
                res.RowVersion = station.RowVersion;

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

        public HttpStatusCode UpdateStationInfo(UpdateStationInfoBindingModel bindingModel)
        {
            try
            {
                Station station = AppDbContext.Stations.Where(x => x.Id == bindingModel.Id).FirstOrDefault();
                if (station != null)
                {
                    Station s = AppDbContext.Stations.Where(x => x.Name == bindingModel.Name && x.Id != bindingModel.Id).FirstOrDefault();
                    if (s != null) //ako postoji stanica sa takvim imenom, vrati gresku
                    {
                        return HttpStatusCode.BadRequest;
                    }
                    
                    for (int i = 0; i < bindingModel.RowVersion.Count(); i++)
                    {
                        if (bindingModel.RowVersion[i] != station.RowVersion[i])
                            return HttpStatusCode.Conflict;
                    }

                    station.Name = bindingModel.Name;
                    station.Address = bindingModel.Address;

                    Coordinates co = new Coordinates() { CoordX = bindingModel.X, CoordY = bindingModel.Y };
                    AppDbContext.Coordinates.Add(co);
                    AppDbContext.SaveChanges();

                    int corId = AppDbContext.Coordinates.Where(x => x.CoordX == co.CoordX && x.CoordY == co.CoordY).FirstOrDefault().CoordinatesId;
                    station.CoordinatesId = corId;

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