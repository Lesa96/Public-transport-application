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
}