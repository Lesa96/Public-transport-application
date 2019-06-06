using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class BuyTicketBindingModel
    {
        [Required]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "TicketType")]
        public TicketType TicketType { get; set; }

        [Required]
        [Display(Name = "PassengerType")]
        public PassengerType PassengerType { get; set; }
    }

    public class BuyUnregisteredBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}