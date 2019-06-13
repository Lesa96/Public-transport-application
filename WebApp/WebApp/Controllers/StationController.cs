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
    [RoutePrefix("api/Station")]
    public class StationController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public StationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetAllStationNames")]
        public IHttpActionResult GetAllStationNames()
        {
            string[] stationNames = unitOfWork.Stations.GetStationNames();
            if(stationNames.Equals(string.Empty))
            {
                return NotFound();
            }
            return Ok(stationNames);

        }

        [HttpGet]
        [Route("GetStationsById")]
        public IHttpActionResult GetStationsById(int id)
        {
            UpdateStationInfoBindingModel station = unitOfWork.Stations.GetStationsById(id);
            if(station == null)
            {
                return NotFound();
            }
            
            return Ok(station);

        }

        [HttpGet]
        [Route("GetAllStations")]
        public IHttpActionResult GetAllStations()
        {
            List<UpdateStationInfoBindingModel> stations = unitOfWork.Stations.GetAllStations();
            if (stations.Count == 0)
            {
                return NotFound();
            }

            return Ok(stations);

        }


        [HttpPost]
        [Route("AddStation")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddStation(AddStationFullBindingModel bindingModel)
        {
            if (unitOfWork.Stations.AddStation(bindingModel.StationName, bindingModel.StationAddress, bindingModel.X, bindingModel.Y))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteStation")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteStation(string StationName)
        {
            if (unitOfWork.Stations.DeleteStationByName(StationName))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetIdsAndStationNames")]
        public IHttpActionResult GetIdsAndStationNames()
        {
            List<string> res = unitOfWork.Stations.GetStationsIdsAndNames();
            if(res.Count == 0)
            {
               return NotFound();
            }

            return Ok(res);

        }



        [Route("UpdateStationInfo"), HttpPatch]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateStationInfo(UpdateStationInfoBindingModel bindingModel)
        {
            if (unitOfWork.Stations.UpdateStationInfo(bindingModel))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("AddLine"), HttpPatch]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddLine(ManageLinesBindingModel bindingModel)
        {
            var station = unitOfWork.Stations.Get(bindingModel.Id);
            if (station == null)
            {
                return NotFound();
            }

            var line = unitOfWork.Drivelines.GetLineByNumber(bindingModel.LineNumber);
            if (line == null)
            {
                return NotFound();
            }

            station.Drivelines.Add(line);

            unitOfWork.Stations.Update(station);
            unitOfWork.Drivelines.Update(line);
            unitOfWork.Complete();
            return Ok();
        }

        [Route("RemoveLine"), HttpPatch]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult RemoveLine(ManageLinesBindingModel bindingModel)
        {
            var station = unitOfWork.Stations.Get(bindingModel.Id);
            if (station == null)
            {
                return NotFound();
            }

            var line = unitOfWork.Drivelines.GetLineByNumber(bindingModel.LineNumber);
            if (line == null)
            {
                return NotFound();
            }

            station.Drivelines.Remove(line);

            unitOfWork.Stations.Update(station);
            unitOfWork.Drivelines.Update(line);
            unitOfWork.Complete();
            return Ok();
        }
    }
}
