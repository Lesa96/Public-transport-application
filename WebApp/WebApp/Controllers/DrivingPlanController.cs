using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/DrivingPlan")]
    public class DrivingPlanController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public DrivingPlanController(IUnitOfWork un)
        {
            unitOfWork = un;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var drivingPlans = unitOfWork.DrivingPlans.GetAll();
            return Ok(drivingPlans);
        }

        //GET / DrivingPlan / GetDrivingPlanDepartures? DriveLineNumber=1 & DrivePlanType=2 & DriveLineType=1
        [HttpGet]
        [Route("GetDrivingPlanDepartures")]
        public IHttpActionResult GetDrivingPlanDepartures(int lineNumber , DriveType driveType , WeekDays drivePlanDay)
        {
            DrivingPlan drivingPlan = unitOfWork.DrivingPlans.GetSpecificDrivingPlan(driveType, drivePlanDay, lineNumber);
            if(drivingPlan == null)
            {
                return NotFound();
            }

            var departures = drivingPlan.Departures.Split(';');

            return Ok(departures);



        }

        [HttpPost]
        [Route("AddDrivingPlan")]
        public IHttpActionResult AddDrivingPlan(AddDrivingPlanBindingModel bindingModel)
        {
            var lineId = unitOfWork.Drivelines.GetLineByNumber(bindingModel.Number).Id;
            string departures = "";

            foreach (var departure in bindingModel.Departures.OrderBy(d => d, StringComparer.Ordinal))
            {
                departures += departure + ";";
            }

            DrivingPlan drivingPlan = new DrivingPlan()
            {
                Type = bindingModel.Type,
                Day = bindingModel.Day,
                DrivelineId = lineId,
                Departures = departures
            };

            unitOfWork.DrivingPlans.Add(drivingPlan);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Route("DeleteDrivingPlan")]
        public IHttpActionResult DeleteDrivingPlan(DeleteDrivingPlanBindingModel bindingModel)
        {
            var drivingPlan = unitOfWork.DrivingPlans.Get(bindingModel.Id);
            if (drivingPlan == null)
                return NotFound();

            unitOfWork.DrivingPlans.Remove(drivingPlan);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
