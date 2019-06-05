using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Driveline
    {
        public int Id { get; set; }
        public int Number { get; set; }
        
        public virtual ICollection<DrivingPlan> DrivingPlans { get; set; }

        public virtual ICollection<Station> Stations { get; set; }
        

        public Driveline()
        {
            Stations = new HashSet<Station>();
            DrivingPlans = new HashSet<DrivingPlan>();


        }
    }
}