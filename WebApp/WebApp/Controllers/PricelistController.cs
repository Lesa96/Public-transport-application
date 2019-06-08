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
            //Debug this... List as field problem?
            var result = new List<DisplayPricelistBindingModel>();
            var pricelists = unitOfWork.Pricelists.GetAll();
            foreach (var pricelist in pricelists)
            {
                result.Add(new DisplayPricelistBindingModel()
                {
                    PricelistId = pricelist.PricelistId,
                    ValidFrom = pricelist.ValidFrom,
                    ValidUntil = pricelist.ValidUntil
                });
            }
            return Ok(result);
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
                ValidUntil = bindingModel.ValidUntil
            };

            unitOfWork.Pricelists.Add(pricelist);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Route("DeletePricelist")]
        public IHttpActionResult DeletePricelist(int id)
        {
            var pricelist = unitOfWork.Pricelists.Get(id);
            if (pricelist == null)
                return NotFound();

            unitOfWork.Pricelists.Remove(pricelist);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [Route("UpdatePricelist")]
        public IHttpActionResult UpdatePricelist(UpdatePricelistBindingModel bindingModel)
        {
            Pricelist pricelist = unitOfWork.Pricelists.Get(bindingModel.Id);

            pricelist.ValidFrom = bindingModel.ValidFrom;
            pricelist.ValidUntil = bindingModel.ValidUntil;
            pricelist.PricelistItems = bindingModel.PricelistItems;

            unitOfWork.Pricelists.Update(pricelist);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
