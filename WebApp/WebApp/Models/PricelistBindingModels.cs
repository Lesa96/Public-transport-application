using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class GetTicketPriceBindingModel
    {
        [Required]
        [Display(Name = "TicketType")]
        public TicketType TicketType { get; set; }

        [Required]
        [Display(Name = "PassengerType")]
        public PassengerType PassengerType { get; set; }
    }

    public class UpdateTicketPriceBindingModel
    {
        [Required]
        [Display(Name = "PricelistId")]
        public int PricelistId { get; set; }

        [Required]
        [Display(Name = "PricelistItemId")]
        public int PricelistItemId { get; set; }

        [Required]
        [Display(Name = "Price")]
        public float Price { get; set; }
    }

    public class AddPricelistBindingModel
    { 
        [Required]
        [Display(Name = "ValidFrom")]
        public DateTime ValidFrom { get; set; }
        [Required]
        [Display(Name = "ValidUntil")]
        public DateTime ValidUntil { get; set; }
        [Display(Name = "PricelistItems")]
        public virtual ICollection<PricelistItem> PricelistItems { get; set; }
    }
}