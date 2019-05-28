using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class DrivingPlan
    {
        public int Id { get; set; }
        public WeekDays Day { get; set; }
        public DriveType Type { get; set; }

        [ForeignKey("Id")]
        public List<Driveline> Line { get; set; }
        

        public DrivingPlan()
        {

        }
    }
}