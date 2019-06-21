using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.Repository;
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
                return BadRequest("There is no price for this ticket");
            }

            return Ok(regularPrice * coefficient);
        }

        [Route("UpdateTicketPrice")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateTicketPrice(UpdateTicketPriceBindingModel bindingModel)
        {
            var pricelist = unitOfWork.Pricelists.Get(bindingModel.PricelistId);
            if (pricelist == null)
            {
                return BadRequest("There is no such pricelist");
            }

            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemByIds(bindingModel.PricelistId, bindingModel.PricelistItemId);
            if (pricelistItem == null)
            {
                return BadRequest("There is no such priceliset item");
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
            if(pricelists.Count() == 0)
            {
                return NotFound();
            }
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
            if (pricelist == null)
            {
                return NotFound();
            }
            var resultPricelist = new DisplayPricelistBindingModel()
            {
                PricelistId = pricelist.PricelistId,
                ValidFrom = pricelist.ValidFrom,
                ValidUntil = pricelist.ValidUntil
            };

            return Ok(resultPricelist);
        }

        [HttpPost]
        [Route("AddPricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddPricelist(AddPricelistBindingModel bindingModel)
        {
            Pricelist pricelist = new Pricelist()
            {
                ValidFrom = bindingModel.ValidFrom,
                ValidUntil = bindingModel.ValidUntil,
            };

            unitOfWork.Pricelists.Add(pricelist);
            unitOfWork.Complete();

            var pr = unitOfWork.Pricelists.Find(pl => pl.ValidFrom == bindingModel.ValidFrom && pl.ValidUntil == bindingModel.ValidUntil).LastOrDefault();
            if(pr == null)
            {
                return BadRequest("Pricelist Items where not added corectly");
            }
            foreach (var item in bindingModel.PricelistItems)
            {
                item.PricelistId = pr.PricelistId;
                unitOfWork.PricelistItems.Add(item);
            }

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Route("DeletePricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeletePricelist(int id)
        {
            HttpStatusCode response = unitOfWork.Pricelists.DeletePricelist(id);
            if (response == HttpStatusCode.OK)
                return Ok();
            if (response == HttpStatusCode.Conflict)
                return Conflict();

            return NotFound();
        }

        [HttpPut]
        [Route("UpdatePricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdatePricelist(UpdatePricelistBindingModel bindingModel)
        {
            HttpStatusCode response = unitOfWork.Pricelists.UpdatePricelist(bindingModel);
            if (response == HttpStatusCode.OK)
                return Ok();
            if (response == HttpStatusCode.Conflict)
                return Conflict();

            return NotFound();
        }

        [HttpGet]
        [Route("GetPricelistItems")]
        public IHttpActionResult GetPricelistItems(int id)
        {
            var items = unitOfWork.PricelistItems.Find(item => item.PricelistId == id);
            if(items == null)
            {
                return BadRequest("There are no pricelist items");
            }
            var resultList = new List<GetPricelistItemsBindingModel>();
            foreach (var item in items)
            {
                resultList.Add(new GetPricelistItemsBindingModel()
                {
                    PricelistItemId = item.PricelistItemId,
                    TicketType = item.TicketType,
                    PassengerType = item.PassengerType,
                    Price = item.Price
                });
            }
            return Ok(resultList);
        }
    }
}
