using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Transaction
    {
        [Key]
        public string OrderId { get; set; }

        public string UserId { get; set; }

        public string CreateTime { get; set; }

        public string Status { get; set; }

        public string PayerEmail { get; set; }
    }
}