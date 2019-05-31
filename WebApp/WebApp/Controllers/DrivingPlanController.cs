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
    [RoutePrefix("DrivingPlan")]
    public class DrivingPlanController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        //GET / DrivingPlan / GetDrivingPlan?type=City&day=Monday&drivelineNumber=1
        [HttpGet]
        [Route("GetDrivingPlanLine")]
        public IHttpActionResult GetDrivingPlanLine (GetDrivingPlanBindingModel model)
        {

            var lines = unitOfWork.DrivingPlans.GetAllDrivingPlans().Where
                (
                    x => x.Type.ToString() == model.DrivePlanType && x.Day.ToString() == model.DrivePlanDay
                ).FirstOrDefault().Line; //sve linije za odredjeni dan i tip

            var drivingPlanLine = lines.Find(x => x.Number == int.Parse(model.DriveLineNumber)); //tacno trazena linija

            return Ok(drivingPlanLine);
            


        }
    }
}
