using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class CoordinatesRepository : Repository<Coordinates, int>, ICoordinatesRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public CoordinatesRepository(DbContext context) : base(context)
        {
        }
    }
}