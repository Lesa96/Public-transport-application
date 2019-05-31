using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public DateTime TimeOfPurchase { get; set; }

        public int TicketInfoId { get; set; }
        public string PassengerId { get; set; }
        public string ControllerId { get; set; }
        public bool IsCanceled { get; set; }

        [ForeignKey("TicketInfoId")]
        public  PricelistItem TicketInfo { get; set; }
        [ForeignKey("PassengerId")]
        public  ApplicationUser Passenger { get; set; }
        [ForeignKey("ControllerId")]
        public  ApplicationUser Controller { get; set; }
    }
}