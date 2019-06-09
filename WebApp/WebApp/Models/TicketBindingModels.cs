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
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "TicketType")]
        public TicketType TicketType { get; set; }
    }

    public class BuyUnregisteredBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }
    }

    public class TicketValidationBindingModel
    {
        [Required]
        [Display(Name = "TicketId")]
        public int TicketId { get; set; }
    }

    public class DisplayTicketBindingModel
    {
        [Required]
        [Display(Name = "TicketId")]
        public int TicketId { get; set; }

        [Required]
        [Display(Name = "TimeOfPurchase")]
        public DateTime TimeOfPurchase { get; set; }

        [Required]
        [Display(Name = "IsValid")]
        public bool IsValid { get; set; }
    }
}