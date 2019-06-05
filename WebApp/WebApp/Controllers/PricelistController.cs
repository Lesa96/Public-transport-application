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
    }
}
