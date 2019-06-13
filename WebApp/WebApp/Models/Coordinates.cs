using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Coordinates
    {
        public int CoordinatesId { get; set; }
        public float CoordX { get; set; }
        public float CoordY { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}