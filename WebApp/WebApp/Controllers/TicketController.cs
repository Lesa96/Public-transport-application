﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Ticket")]
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
            if (pricelistItem == null)
            {
                return NotFound();
            }

            var passenger = unitOfWork.Users.Find(u => u.Id.Equals(bindingModel.UserId)).FirstOrDefault();
            if (passenger == null)
            {
                return NotFound();
            }

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

        //BuyUnregistered
        [HttpPost, Route("BuyUnregistered")]
        public IHttpActionResult BuyUnregistered(BuyUnregisteredBindingModel bindingModel)
        {
            DateTime currentTime = DateTime.Now;
            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.OneHourTicket, PassengerType.Regular, currentTime);
            if (pricelistItem == null)
            {
                return NotFound();
            }

            Ticket ticket = new Ticket()
            {
                TimeOfPurchase = currentTime,
                TicketInfo = pricelistItem,
                IsCanceled = false
            };

            SendTicketToEmail(ticket, bindingModel.email);

            unitOfWork.Tickets.Add(ticket);
            unitOfWork.Complete();

            return Ok();
        }

        public void SendTicketToEmail(Ticket ticket, string toEmail)
        {
            MailMessage mail = new MailMessage("you@yourcompany.com", toEmail);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Ticket";
            mail.Body = ticket.ToString();
            client.Send(mail);
        }
    }
}
