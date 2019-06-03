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

        //[Authorize(Roles ="Admin")]
        [HttpPatch, Route("AddStation")]
        public IHttpActionResult AddStation(AddStationBindingModel bindingModel)
        {
            unitOfWork.Drivelines.AddStationInLine(bindingModel.DriveLineId, bindingModel.StationId);

            return Ok();
        }

        //[Authorize(Roles ="Admin")]
        [HttpPatch, Route("DeleteStation")]
        public IHttpActionResult DeleteStation(AddStationBindingModel bindingModel)
        {
            unitOfWork.Drivelines.DeleteStationInLine(bindingModel.DriveLineId, bindingModel.StationId);

            return Ok();
        }

        //[Authorize(Roles ="Admin")]
        [HttpPatch, Route("ChangeNumber")]
        public IHttpActionResult ChangeNumber(ChangeNumberBindingModel bindingModel)
        {
            unitOfWork.Drivelines.UpdateNumber(bindingModel.DriveLineId, bindingModel.DriveLineNumber);

            return Ok();
        }
    }
}
