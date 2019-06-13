using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class DrivingPlan
    {
        public int Id { get; set; }
        public int DrivelineId { get; set; }
        public WeekDays Day { get; set; }
        public DriveType Type { get; set; }

        [ForeignKey("DrivelineId")]
        public Driveline Driveline { get; set; }
        public string Departures { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DrivingPlan()
        {
           


        }
    }
}