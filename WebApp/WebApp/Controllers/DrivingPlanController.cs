﻿using System;
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

        public DrivingPlanController(IUnitOfWork un)
        {
            unitOfWork = un;
        }

        //GET / DrivingPlan / GetDrivingPlan? ...
        [HttpGet]
        [Route("GetDrivingPlanLine")]
        public IHttpActionResult GetDrivingPlanLine (GetDrivingPlanBindingModel model)
        {

            var lines = unitOfWork.DrivingPlans.GetSpecificDrivingPlan(model.DrivePlanType, model.DrivePlanDay, model.DriveLineNumber).Line;

            var drivingPlanLine = lines.Find(x => x.Number == model.DriveLineNumber); //tacno trazena linija

            return Ok(drivingPlanLine);
            


        }
    }
}