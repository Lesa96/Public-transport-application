using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class PassengerTypeCoefficient
    {
        public int PassengerTypeCoefficientId { get; set; }
        public PassengerType PassengerType { get; set; }
        public float Coefficient { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}