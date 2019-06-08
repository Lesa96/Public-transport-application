using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Driveline")]
    public class DrivelineController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public DrivelineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

        [HttpGet, Route("GetDrivelineNumberById")]
        public IHttpActionResult GetDrivelineNumberById(int id)
        {
            Driveline dr = unitOfWork.Drivelines.GetLineById(id);
            if (dr == null)
                return NotFound();

            return Ok(dr.Number);
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

            return Ok(stations);
        }

        [HttpPatch, Route("UpdateDriveline")]
        public IHttpActionResult UpdateDriveline(ChangeDrivelineBindingModel bindingModel)
        {
            Driveline dr = unitOfWork.Drivelines.Get(bindingModel.DriveLineId);
            if (dr != null)
            {
                dr.Number = bindingModel.DriveLineNumber;
                dr.Stations.Clear();

                if (bindingModel.StationNames != null)
                {
                    foreach (string name in bindingModel.StationNames)
                    {
                        dr.Stations.Add(unitOfWork.Stations.Find(s => s.Name == name).FirstOrDefault()); //dodaje stanice u liniju
                    }
                }

                unitOfWork.Drivelines.Update(dr);
                unitOfWork.Complete();
                return Ok();
            }

            return NotFound();

            
        }


        //[Authorize(Roles ="Admin")]
        [HttpPost, Route("AddDriveline")]
        public IHttpActionResult AddDriveline(AddDrivelineBindingModel bindingModel)
        {

            if (unitOfWork.Drivelines.AddDriveline(bindingModel.Number, bindingModel.StationNames))
            {
                return Ok();
            }

            return BadRequest();

            
        }

        //[Authorize(Roles ="Admin")]
        [HttpDelete, Route("DeleteDriveline")]
        public IHttpActionResult DeleteDriveline(int number)
        {

            if (unitOfWork.Drivelines.DeleteDriveline(number))
            {
                return Ok();
            }

            return NotFound();


        }


        //[Authorize(Roles ="Admin")]
        [HttpPatch, Route("AddStation")]
        public IHttpActionResult AddStation(AddStationBindingModel bindingModel)
        {
            if(unitOfWork.Drivelines.AddStationInLine(bindingModel.DriveLineId, bindingModel.StationId))
            {
                return NotFound();
            }

            return Ok();
        }

        //[Authorize(Roles ="Admin")]
        [HttpPatch, Route("DeleteStation")]
        public IHttpActionResult DeleteStation(AddStationBindingModel bindingModel)
        {
            if(unitOfWork.Drivelines.DeleteStationInLine(bindingModel.DriveLineId, bindingModel.StationId))
            {
                return NotFound();
            }

            return Ok();
        }

        //[Authorize(Roles ="Admin")]
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
