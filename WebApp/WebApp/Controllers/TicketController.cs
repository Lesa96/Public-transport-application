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
    public class TicketController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public TicketController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost, Route("BuyTicket")]
        public IHttpActionResult BuyTicket(BuyTicketBindingModel bindingModel)
        {
            DateTime currentTime = DateTime.Now;
            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(bindingModel.TicketType, bindingModel.PassengerType, currentTime);
            var passenger = unitOfWork.Users.Find(u => u.Id.Equals(bindingModel.UserId)).FirstOrDefault();

            Ticket ticket = new Ticket() {
                TimeOfPurchase = currentTime,
                TicketInfo = pricelistItem,
                IsCanceled = false,
                Passenger = passenger
            };

            unitOfWork.Tickets.Add(ticket);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
