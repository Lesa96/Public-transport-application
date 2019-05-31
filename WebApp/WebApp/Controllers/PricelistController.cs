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
    [RoutePrefix("api/Pricelist")]
    public class PricelistController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PricelistController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("GetTicketPrice")]
        public IHttpActionResult GetTicketPrice(GetTicketPriceBindingModel bindingModel)
        {
            DateTime currentTime = DateTime.Now;
            int price = unitOfWork.Pricelists.GetTicketPrice(bindingModel.TicketType, bindingModel.PassengerType, currentTime);

            return Ok(price);
        }
    }
}
