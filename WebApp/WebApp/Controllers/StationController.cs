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

        [Route("UpdateStationInfo"), HttpPatch]
        public IHttpActionResult UpdateStationInfo(UpdateStationInfoBindingModel bindingModel)
        {
            var station = unitOfWork.Stations.Get(bindingModel.Id);
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
            var line = unitOfWork.Drivelines.GetLineByNumber(bindingModel.LineNumber);

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
            var line = unitOfWork.Drivelines.GetLineByNumber(bindingModel.LineNumber);

            station.Drivelines.Remove(line);

            unitOfWork.Stations.Update(station);
            unitOfWork.Drivelines.Update(line);
            unitOfWork.Complete();
            return Ok();
        }
    }
}
