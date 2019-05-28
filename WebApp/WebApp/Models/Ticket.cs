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

        [ForeignKey("PricelistItemId")]
        public virtual PricelistItem TicketInfo { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser Passenger { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser Controller { get; set; }
    }
}