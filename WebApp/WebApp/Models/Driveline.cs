using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Driveline
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public virtual List<Station> Stations { get; set; }

        public Driveline()
        {
            Stations = new List<Station>();
        }
    }
}