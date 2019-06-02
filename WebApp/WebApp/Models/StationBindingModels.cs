using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UpdateStationInfoBindingModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Coodinates")]
        public Coordinates Coordinates { get; set; }
    }

    public class ManageLinesBindingModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "LineNumber")]
        public int LineNumber { get; set; }
    }


}