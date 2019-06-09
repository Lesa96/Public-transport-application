﻿using System;
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
        [HttpPost]
        [Route("AddStation")]
        public IHttpActionResult AddStation(AddStationFullBindingModel bindingModel)
        {
            if (unitOfWork.Stations.AddStation(bindingModel.StationName, bindingModel.StationAddress, bindingModel.X, bindingModel.Y))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteStation")]
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

        [Route("UpdateStationInfo"), HttpPatch]
        public IHttpActionResult UpdateStationInfo(UpdateStationInfoBindingModel bindingModel)
        {
            var station = unitOfWork.Stations.Get(bindingModel.Id);
            if(station == null)
            {
                return NotFound();
            }
            station.Name = bindingModel.Name;
            station.Address = bindingModel.Address;
            station.Coordinates.CoordX = bindingModel.Coordinates.CoordX;
            station.Coordinates.CoordY = bindingModel.Coordinates.CoordY;

            unitOfWork.Stations.Update(station);
            unitOfWork.Complete();
            return Ok();
        }

        [Route("AddLine"), HttpPatch]
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
