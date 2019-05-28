using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }         //ovo promeniti

        [ForeignKey("Id")]
        public List<Driveline> Drivelines { get; set; }

        public Station()
        {

        }
    }
}