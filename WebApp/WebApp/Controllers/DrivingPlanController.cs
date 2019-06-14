using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/DrivingPlan")]
    public class DrivingPlanController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private static NotificationHub notificationHub;


        public DrivingPlanController(IUnitOfWork un)
        {
            unitOfWork = un;
            notificationHub = new NotificationHub(unitOfWork);
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var drivingPlans = unitOfWork.DrivingPlans.GetAll();
            if(drivingPlans == null)
            {
                return NotFound();
            }
            List<DisplayDrivingPlanBindingModel> displayDrivingPlans =
                new List<DisplayDrivingPlanBindingModel>();
            foreach (var drivingPlan in drivingPlans)
            {
                displayDrivingPlans.Add(new DisplayDrivingPlanBindingModel()
                {
                    Id = drivingPlan.Id,
                    Day = drivingPlan.Day,
                    Type = drivingPlan.Type,
                    Departures = drivingPlan.Departures,
                    Line = unitOfWork.Drivelines.Get(drivingPlan.DrivelineId).Number,
                    DrivelineId = drivingPlan.DrivelineId
                });
            }
            return Ok(displayDrivingPlans);
        }

        [HttpGet]
        [Route("GetPlan")]
        public IHttpActionResult GetPlan(int id)
        {
            var drivingPlan = unitOfWork.DrivingPlans.Get(id);
            if (drivingPlan == null)
            {
                return NotFound();
            }
            DisplayDrivingPlanBindingModel displayDPBM =
                new DisplayDrivingPlanBindingModel()
                {
                    Id = drivingPlan.Id,
                    Day = drivingPlan.Day,
                    Type = drivingPlan.Type,
                    Departures = drivingPlan.Departures,
                    Line = unitOfWork.Drivelines.Get(drivingPlan.DrivelineId).Number,
                    DrivelineId = drivingPlan.DrivelineId
                };
            return Ok(displayDPBM);
        }

        //GET / DrivingPlan / GetDrivingPlanDepartures? DriveLineNumber=1 & DrivePlanType=2 & DriveLineType=1
        [HttpGet]
        [Route("GetDrivingPlanDepartures")]
        public IHttpActionResult GetDrivingPlanDepartures(int lineNumber , DriveType driveType , WeekDays drivePlanDay)
        {
            //notificationHub.TimeServerUpdates();
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
        [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Route("DeleteDrivingPlan")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteDrivingPlan(int id)
        {
            if(unitOfWork.DrivingPlans.DeleteDrivingPlan(id) == HttpStatusCode.OK)
                return Ok();
            if (unitOfWork.DrivingPlans.DeleteDrivingPlan(id) == HttpStatusCode.Conflict)
                return Conflict();

            return NotFound();
        }

        [HttpPut]
        [Route("UpdateDrivingPlan")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateDrivingPlan(UpdateDrivingPlanBindingModel bindingModel)
        {
            if(unitOfWork.DrivingPlans.UpdateDrivingPlan(bindingModel.Id, bindingModel.Number , bindingModel.Type , bindingModel.Day , bindingModel.Departures) == HttpStatusCode.OK)
                return Ok();
            if (unitOfWork.DrivingPlans.UpdateDrivingPlan(bindingModel.Id, bindingModel.Number, bindingModel.Type, bindingModel.Day, bindingModel.Departures) == HttpStatusCode.Conflict)
                return Conflict();

            return NotFound();
        }
    }
}
