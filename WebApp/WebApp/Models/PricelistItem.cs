using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public float Price { get; set; }
        public int PricelistId { get; set; }

        [ForeignKey("PricelistId")]
        public Pricelist Pricelist { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        //[ForeignKey("PassengerTypeCoefficientId")]
        //public PassengerTypeCoefficient PassengerTypeCoefficient { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


        public PricelistItem()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}