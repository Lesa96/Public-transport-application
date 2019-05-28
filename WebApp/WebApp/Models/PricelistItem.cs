using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class PricelistItem
    {
        public int PricelistItemId { get; set; }
        public TicketType TicketType { get; set; }
        public PassengerType PassengerType { get; set; }
        public int Price { get; set; }
    }
}