using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int PricelistId { get; set; }

        [ForeignKey("PricelistId")]
        public Pricelist Pricelist { get; set; }
        //[ForeignKey("PassengerTypeCoefficientId")]
        //public PassengerTypeCoefficient PassengerTypeCoefficient { get; set; }
    }
}