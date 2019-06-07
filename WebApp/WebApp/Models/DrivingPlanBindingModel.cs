using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class GetDrivingPlanBindingModel
    {
        [Required]
        [Display(Name = "Drive line number")]
        public int DriveLineNumber { get; set; }

        [Required]
        [Display(Name = "Drive plan type")]
        public DriveType DrivePlanType { get; set; }

        [Required]
        [Display(Name = "Drive plan day")]
        public WeekDays DrivePlanDay { get; set; }
    }

    public class AddDrivingPlanBindingModel
    {
        [Required]
        [Display(Name = "Type")]
        public DriveType Type { get; set; }

        [Required]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Day")]
        public WeekDays Day { get; set; }

        [Display(Name = "Departures")]
        public ICollection<string> Departures { get; set; }

        public AddDrivingPlanBindingModel()
        {
            Departures = new HashSet<string>();
        }
    }

    public class DeleteDrivingPlanBindingModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
    }

    public class DisplayDrivingPlanBindingModel
    {
        public int Id { get; set; }
        public int Line { get; set; }
        public WeekDays Day { get; set; }
        public DriveType Type { get; set; }
        public string Departures { get; set; }
        public int DrivelineId { get; set; }
    }
}