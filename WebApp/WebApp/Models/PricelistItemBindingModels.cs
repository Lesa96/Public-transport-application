using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class GetPricelistItemsBindingModel
    {
        [Required]
        [Display(Name = "PricelistItemId")]
        public int PricelistItemId { get; set; }

        [Required]
        [Display(Name = "TicketType")]
        public TicketType TicketType { get; set; }
        [Required]
        [Display(Name = "PassengerType")]
        public PassengerType PassengerType { get; set; }
        [Required]
        [Display(Name = "Price")]
        public float Price { get; set; }
    }
}