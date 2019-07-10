using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Driveline")]
    public class DrivelineController : ApiController
    {
        private static BussLocationHelper bussLocation = new BussLocationHelper();
        private readonly IUnitOfWork unitOfWork;
        private static NotificationHub notificationHub;


        public DrivelineController(IUnitOfWork unitOfWork , NotificationHub hub)
        {
            this.unitOfWork = unitOfWork;
            notificationHub = hub;
        }

        [HttpPost, Route("AddBussRoutes")]
        public IHttpActionResult AddBussRoutes(RoutesBindingModel routesBindingModel)
        {
            bussLocation.AddRoutes(routesBindingModel);

            notificationHub.AddToGroupe(routesBindingModel.LineNumber.ToString(), routesBindingModel.ConId);
            return Ok();
        }


        [HttpGet, Route("GetDrivelineNumbers")]
        public IHttpActionResult GetDrivelineNumbers()
        {
            List<Driveline> drivelines = unitOfWork.Drivelines.GetAllDriveLines();

            if(drivelines.Count == 0)
            {
                return NotFound();
            }

            int[] numbers = new int[drivelines.Count];
            for (int i = 0; i < drivelines.Count; i++)
            {
                numbers[i] = drivelines[i].Number;
            }
            

            return Ok(numbers);
        }

        [HttpGet, Route("GetStationsByDrivelineNumber")]
        public IHttpActionResult GetStationsByDrivelineNumber(int number)
        {
            Driveline driveline = unitOfWork.Drivelines.GetLineByNumber(number);
            if(driveline == null || driveline.Stations.Count == 0)
            {
               return NotFound();
            }
            List<UpdateStationInfoBindingModel> stations = new List<UpdateStationInfoBindingModel>();
            foreach (Station s in driveline.Stations)
            {
                UpdateStationInfoBindingModel station = new UpdateStationInfoBindingModel();
                station.Id = s.Id;
                station.Name = s.Name;
                station.Address = s.Address;
                Coordinates cor = unitOfWork.CoordinatesRepository.Get(s.CoordinatesId);
                station.X = cor.CoordX;
                station.Y = cor.CoordY;

                stations.Add(station);
            }

            return Ok(stations);
        }

        [HttpGet, Route("GetDrivelineNumberById")]
        public IHttpActionResult GetDrivelineNumberById(int id)
        {
            Driveline dr = unitOfWork.Drivelines.GetLineById(id);
            ChangeDrivelineBindingModel bindingModel = new ChangeDrivelineBindingModel();
           
  
            if (dr == null)
                return NotFound();
            bindingModel.DriveLineNumber = dr.Number;
            bindingModel.RowVersion = dr.RowVersion;

            return Ok(bindingModel);
        }

        [HttpGet, Route("GetDrivelineNumbersAndIds")]
        public IHttpActionResult GetDrivelineNumbersAndIds()
        {
            List<string> drLines = unitOfWork.Drivelines.GetDrivelineNumbersAndIds();

            if(drLines.Count == 0)
            {
                return NotFound();
            }

            return Ok(drLines);
        }

        [HttpGet, Route("GetDrivelineStationsNames")]
        public IHttpActionResult GetDrivelineStationsNames(int id)
        {
            string[] stations = unitOfWork.Drivelines.GetDrivelineStationsNames(id);
            if(stations.Count() == 0)
            {
                return NotFound();
            }

            return Ok(stations);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch, Route("UpdateDriveline")]
        public IHttpActionResult UpdateDriveline(ChangeDrivelineBindingModel bindingModel)
        {
            HttpStatusCode response = unitOfWork.Drivelines.UpdateDriveline(bindingModel.DriveLineId, bindingModel.DriveLineNumber, bindingModel.StationNames,bindingModel.RowVersion);
            if (response == HttpStatusCode.OK)
                return Ok();
            if (response == HttpStatusCode.Conflict)
                return Conflict();

            return NotFound();


            //Driveline dr = unitOfWork.Drivelines.Find(x => x.Id == bindingModel.DriveLineId).FirstOrDefault();

            //if (dr != null)
            //{
            //    dr.Number = bindingModel.DriveLineNumber;
            //    dr.Stations.Clear();

            //    if (bindingModel.StationNames != null)
            //    {
            //        foreach (string name in bindingModel.StationNames)
            //        {
            //            dr.Stations.Add(unitOfWork.Stations.Find(s => s.Name == name).FirstOrDefault()); //dodaje stanice u liniju
            //        }
            //    }
            //    unitOfWork.Drivelines.Update(dr);

            //    try
            //    {
            //        unitOfWork.Complete();

            //        return Ok();
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        Trace.WriteLine("DbUpdateConcurrencyException Message: {0}", ex.Message);
            //        return Conflict();
            //    }
            //    catch (Exception ex)
            //    {
            //        Trace.WriteLine("NormalException Message: {0}", ex.Message);
            //        return Conflict();
            //    }

            //}
            //return NotFound();



        }


        [Authorize(Roles ="Admin")]
        [HttpPost, Route("AddDriveline")]
        public IHttpActionResult AddDriveline(AddDrivelineBindingModel bindingModel)
        {

            if (unitOfWork.Drivelines.AddDriveline(bindingModel.Number, bindingModel.StationNames))
            {
                return Ok();
            }
            
            return BadRequest("There is already a station with that number...Station numbers must be uniqe");

            
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete, Route("DeleteDriveline")]
        public IHttpActionResult DeleteDriveline(int number)
        {
            HttpStatusCode response = unitOfWork.Drivelines.DeleteDriveline(number);
            if (response == HttpStatusCode.OK)
            {
                return Ok();
            }
            if (response == HttpStatusCode.Conflict)
            {
                return Conflict();
            }

            return NotFound();


        }


        [Authorize(Roles ="Admin")]
        [HttpPatch, Route("AddStation")]
        public IHttpActionResult AddStation(AddStationBindingModel bindingModel)
        {
            if(unitOfWork.Drivelines.AddStationInLine(bindingModel.DriveLineId, bindingModel.StationId))
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles ="Admin")]
        [HttpPatch, Route("DeleteStation")]
        public IHttpActionResult DeleteStation(AddStationBindingModel bindingModel)
        {
            if(unitOfWork.Drivelines.DeleteStationInLine(bindingModel.DriveLineId, bindingModel.StationId))
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles ="Admin")]
        [HttpPatch, Route("ChangeNumber")]
        public IHttpActionResult ChangeNumber(ChangeNumberBindingModel bindingModel)
        {
            if(unitOfWork.Drivelines.UpdateNumber(bindingModel.DriveLineId, bindingModel.DriveLineNumber))
            {
                return NotFound();
            }

            return Ok();
        }

        
    }
}
