using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Departure
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int DrivingPlanId { get; set; }
        public int DrivelineId { get; set; }

        [ForeignKey("DrivingPlanId")]
        public DrivingPlan DrivingPlan { get; set; }
        [ForeignKey("DrivelineId")]
        public Driveline Driveline { get; set; }

        public Departure()
        {

        }
    }
}