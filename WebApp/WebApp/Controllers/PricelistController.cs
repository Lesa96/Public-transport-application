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
    [RoutePrefix("api/Pricelist")]
    public class PricelistController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        
        public PricelistController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("GetTicketPrice")]
        public IHttpActionResult GetTicketPrice(TicketType ticketType, PassengerType passengerType)
        {
            DateTime currentTime = DateTime.Now;
            float regularPrice = unitOfWork.Pricelists.GetTicketPrice(ticketType, passengerType, currentTime);
            float coefficient = unitOfWork.PassengerTypeCoefficients.GetCoefficientForType(passengerType);

            if (regularPrice == 0 || coefficient == 0)
            {
                return BadRequest();
            }

            return Ok(regularPrice * coefficient);
        }

        [Route("UpdateTicketPrice")]
        public IHttpActionResult UpdateTicketPrice(UpdateTicketPriceBindingModel bindingModel)
        {
            var pricelist = unitOfWork.Pricelists.Get(bindingModel.PricelistId);
            if (pricelist == null)
            {
                return NotFound();
            }

            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemByIds(bindingModel.PricelistId, bindingModel.PricelistItemId);
            if (pricelistItem == null)
            {
                return NotFound();
            }

            pricelistItem.Price = bindingModel.Price;

            unitOfWork.PricelistItems.Update(pricelistItem);
            unitOfWork.Pricelists.Update(pricelist);
            unitOfWork.Complete();

            return Ok();
        }

        /////////////////////////////////////

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var pricelists = unitOfWork.Pricelists.GetAll();
            return Ok(pricelists);
        }

        [HttpGet]
        [Route("GetPricelist")]
        public IHttpActionResult GetPricelist(int id)
        {
            var pricelist = unitOfWork.Pricelists.Get(id);

            return Ok(pricelist);
        }

        [HttpPost]
        [Route("AddPricelist")]
        public IHttpActionResult AddPricelist(AddPricelistBindingModel bindingModel)
        {
            Pricelist pricelist = new Pricelist()
            {
                ValidFrom = bindingModel.ValidFrom,
                ValidUntil = bindingModel.ValidUntil,
                PricelistItems = bindingModel.PricelistItems
            };

            unitOfWork.Pricelists.Add(pricelist);
            unitOfWork.Complete();

            return Ok();
        }

        // TODO PROMENITI DRIVINGPLAN U PRICELIST
        [HttpDelete]
        [Route("DeletePricelist")]
        public IHttpActionResult DeletePricelist(int id)
        {
            var drivingPlan = unitOfWork.DrivingPlans.Get(id);
            if (drivingPlan == null)
                return NotFound();

            unitOfWork.DrivingPlans.Remove(drivingPlan);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateDrivingPlan")]
        public IHttpActionResult UpdateDrivingPlan(UpdateDrivingPlanBindingModel bindingModel)
        {
            var lineId = unitOfWork.Drivelines.GetLineByNumber(bindingModel.Number).Id;
            string departures = "";

            foreach (var departure in bindingModel.Departures.OrderBy(d => d, StringComparer.Ordinal))
            {
                departures += departure + ";";
            }

            DrivingPlan drivingPlan = unitOfWork.DrivingPlans.Get(bindingModel.Id);

            drivingPlan.Type = bindingModel.Type;
            drivingPlan.Day = bindingModel.Day;
            drivingPlan.DrivelineId = lineId;
            drivingPlan.Departures = departures;

            unitOfWork.DrivingPlans.Update(drivingPlan);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
