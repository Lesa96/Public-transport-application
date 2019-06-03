using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddStationBindingModel
    {
        [Required]
        [Display(Name = "Drive line number")]
        public int DriveLineId { get; set; }

        [Required]
        [Display(Name = "Station number")]
        public int StationId { get; set; }
    }

    public class ChangeNumberBindingModel
    {
        [Required]
        [Display(Name = "Drive line Id")]
        public int DriveLineId { get; set; }

        [Required]
        [Display(Name = "Driveline number")]
        public int DriveLineNumber { get; set; }
    }
}