using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    public class AddDrivelineBindingModel
    {
        [Required]
        [Display(Name = "Drive line number")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Station names")]
        public List<string> StationNames { get; set; }
    }
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

    public class ChangeDrivelineBindingModel
    {
        [Required]
        [Display(Name = "Drive line Id")]
        public int DriveLineId { get; set; }

        [Required]
        [Display(Name = "Driveline number")]
        public int DriveLineNumber { get; set; }

        [Required]
        [Display(Name = "Station names")]
        public List<string> StationNames { get; set; }
    }

    public class RoutesBindingModel
    {
        [Required]
        [Display(Name = "Driveline number")]
        public int LineNumber { get; set; }

        [Required]
        [Display(Name = "Route coordinates")]
        public Geolocation[] RouteCoordinates { get; set; }
    }

    public class Geolocation
    {
        [Required]
        public float lat { get; set; }
        [Required]
        public float lng { get; set; }
    }
}