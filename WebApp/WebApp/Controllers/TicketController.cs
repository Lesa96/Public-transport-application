using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
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
            var user = unitOfWork.Users.Find(u => u.Email.Equals(bindingModel.Email)).FirstOrDefault();

            DateTime currentTime = DateTime.Now;
            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(bindingModel.TicketType, user.PassengerType, currentTime);
            if (pricelistItem == null)
            {
                return NotFound();
            }

            Ticket ticket = new Ticket() {
                TimeOfPurchase = currentTime,
                TicketInfo = pricelistItem,
                IsCanceled = false,
                PassengerId = user.Id
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

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var tickets = unitOfWork.Tickets.GetAll();
            var resultList = new List<DisplayTicketBindingModel>();
            foreach (var ticket in tickets)
            {
                resultList.Add(new DisplayTicketBindingModel()
                {
                    TicketId = ticket.TicketId,
                    TimeOfPurchase = ticket.TimeOfPurchase,
                    IsValid = !ticket.IsCanceled
                });
            }
            return Ok(resultList);
        }

        [HttpPatch]
        [Route("ValidateTicket")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult ValidateTicket(TicketValidationBindingModel bindingModel)
        {
            var ticket = unitOfWork.Tickets.Get(bindingModel.TicketId);
            var resultTicket = new DisplayTicketBindingModel()
            {
                TicketId = ticket.TicketId,
                TimeOfPurchase = ticket.TimeOfPurchase,
                IsValid = !ticket.IsCanceled
            };

            if (!ticket.IsCanceled)
            {
                var ticketInfo = unitOfWork.PricelistItems.Get(ticket.TicketInfoId);

                if (ticketInfo.TicketType == TicketType.OneHourTicket)
                {
                    if (ticket.TimeOfPurchase.AddHours(1) < DateTime.Now)
                    {
                        ticket.IsCanceled = true;
                        resultTicket.IsValid = false;
                        unitOfWork.Tickets.Update(ticket);
                        unitOfWork.Complete();
                    }
                }
                else if (ticketInfo.TicketType == TicketType.Daily)
                {
                    if (!(ticket.TimeOfPurchase.Day == DateTime.Now.Day &&
                        ticket.TimeOfPurchase.Month == DateTime.Now.Month &&
                        ticket.TimeOfPurchase.Year == DateTime.Now.Year))
                    {
                        ticket.IsCanceled = true;
                        resultTicket.IsValid = false;
                        unitOfWork.Tickets.Update(ticket);
                        unitOfWork.Complete();
                    }
                }
                else if (ticketInfo.TicketType == TicketType.Monthly)
                {
                    if (!(ticket.TimeOfPurchase.Month == DateTime.Now.Month &&
                        ticket.TimeOfPurchase.Year == DateTime.Now.Year))
                    {
                        ticket.IsCanceled = true;
                        resultTicket.IsValid = false;
                        unitOfWork.Tickets.Update(ticket);
                        unitOfWork.Complete();
                    }
                }
                else if (ticketInfo.TicketType == TicketType.Annual)
                {
                    if (ticket.TimeOfPurchase.Year != DateTime.Now.Year)
                    {
                        ticket.IsCanceled = true;
                        resultTicket.IsValid = false;
                        unitOfWork.Tickets.Update(ticket);
                        unitOfWork.Complete();
                    }
                }
            }
            return Ok(resultTicket);
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
