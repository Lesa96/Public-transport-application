using Microsoft.AspNet.Identity;
using PayPal;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;
using System.Threading.Tasks;

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

        [HttpGet, Route("GetTicketPrices")]
        public IHttpActionResult GetTicketPrices(string email)
        {
            DateTime currentTime = DateTime.Now;
            if (email == "" || email == null) // za neregistrovane
            {
               
                var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.OneHourTicket, PassengerType.Regular, currentTime);
                if (pricelistItem == null)
                {
                    return NotFound();
                }

                return Ok(pricelistItem.Price);

            }

            var user = unitOfWork.Users.Find(u => u.Email.Equals(email)).FirstOrDefault();
            var oneHourPrice = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.OneHourTicket, user.PassengerType, currentTime).Price;
            var dailyPrice = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.Daily, user.PassengerType, currentTime).Price;
            var monthPrice = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.Monthly, user.PassengerType, currentTime).Price;
            var annualPrice = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.Annual, user.PassengerType, currentTime).Price;

            GetPricesBindingModel prices = new GetPricesBindingModel() { OneHour = oneHourPrice, Daily = dailyPrice, Monthly = monthPrice, Annual = annualPrice };

            return Ok(prices);
        }

        [HttpPost, Route("BuyTicket")]
        public async Task<IHttpActionResult> BuyTicket(BuyTicketBindingModel bindingModel)
        {
            var user = unitOfWork.Users.Find(u => u.Email.Equals(bindingModel.Email)).FirstOrDefault();
            if(user == null)
            {
                return BadRequest("User with that email doesn't exist");
            }

            DateTime currentTime = DateTime.Now;
            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(bindingModel.TicketType, user.PassengerType, currentTime);
            if (pricelistItem == null)
            {
                return NotFound();
            }

            OrdersGetRequest request = new OrdersGetRequest(bindingModel.OrderId);
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();
            Transaction transaction;

            if (result.Status == "COMPLETED")
            {
                 transaction = new Transaction()
                {
                    OrderId = result.Id,
                    CreateTime = result.CreateTime,
                    PayerEmail = result.Payer.Email,
                    Status = result.Status,
                    UserId = user.Id

                };

                unitOfWork.Transactions.Add(transaction);
                unitOfWork.Complete();

            }
            else
            {
                return BadRequest("Transaction was not validated.");
            }

            Ticket ticket = new Ticket() {
                TimeOfPurchase = currentTime,
                TicketInfo = pricelistItem,
                IsCanceled = false,
                PassengerId = user.Id
            };
            SendTicketToEmail(ticket, transaction.PayerEmail);

            unitOfWork.Tickets.Add(ticket);
            unitOfWork.Complete();

            return Ok();
        }

        //BuyUnregistered
        [HttpPost, Route("BuyUnregistered")]
        public async Task<IHttpActionResult> BuyUnregistered(BuyUnregisteredBindingModel bindingModel)
        {
            DateTime currentTime = DateTime.Now;
            var pricelistItem = unitOfWork.Pricelists.GetPricelistItemForSelectedTypes(TicketType.OneHourTicket, PassengerType.Regular, currentTime);
            if (pricelistItem == null)
            {
                return NotFound();
            }

            OrdersGetRequest request = new OrdersGetRequest(bindingModel.OrderId);
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();
            Transaction transaction;

            if (result.Status == "COMPLETED")
            {
                transaction = new Transaction()
                {
                    OrderId = result.Id,
                    CreateTime = result.CreateTime,
                    PayerEmail = result.Payer.Email,
                    Status = result.Status,
                   // UserId = unitOfWork.Users.Find(x => x.Email == bindingModel.email).FirstOrDefault().Id
                };

                unitOfWork.Transactions.Add(transaction);
                unitOfWork.Complete();

            }
            else
            {
                return BadRequest("Transaction was not validated.");
            }

            Ticket ticket = new Ticket()
            {
                TimeOfPurchase = currentTime,
                TicketInfo = pricelistItem,
                IsCanceled = false
            };

            SendTicketToEmail(ticket, transaction.PayerEmail);

            unitOfWork.Tickets.Add(ticket);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var tickets = unitOfWork.Tickets.GetAll();
            if(tickets == null)
            {
                return NotFound();
            }
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
            if(ticket == null)
            {
                return BadRequest("There is no ticket with that id");
            }
            var resultTicket = new DisplayTicketBindingModel()
            {
                TicketId = ticket.TicketId,
                TimeOfPurchase = ticket.TimeOfPurchase,
                IsValid = !ticket.IsCanceled
            };

            if (!ticket.IsCanceled)
            {
                var ticketInfo = unitOfWork.PricelistItems.Get(ticket.TicketInfoId);
                if(ticket == null)
                {
                    return BadRequest("There is no pricelist item for that ticket");
                }

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
            

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("pusgs.testing@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = "Ticket: ";
                mail.Body = "Your ticket: \n Price: " +ticket.TicketInfo.Price + ",\n Type: " + ticket.TicketInfo.PassengerType.ToString() + ",\n Time of purchase: " + ticket.TimeOfPurchase;

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("pusgs.testing@gmail.com ", "Pusgs123!t3sting.");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
