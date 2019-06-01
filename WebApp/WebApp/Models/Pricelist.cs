using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        public int PricelistId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public virtual List<PricelistItem> PricelistItems {get; set;}

        public Pricelist()
        {
            PricelistItems = new List<PricelistItem>();
        }
    }
}