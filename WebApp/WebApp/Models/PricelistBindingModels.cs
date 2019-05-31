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
}