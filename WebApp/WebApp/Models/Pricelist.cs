using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        public int PricelistId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public virtual ICollection<PricelistItem> PricelistItems {get; set;}

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Pricelist()
        {
            PricelistItems = new HashSet<PricelistItem>();
        }
    }
}