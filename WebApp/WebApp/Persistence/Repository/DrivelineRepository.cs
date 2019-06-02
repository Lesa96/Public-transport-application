using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class DrivelineRepository : Repository<Driveline, int>, IDrivelineRepository
    {

        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public DrivelineRepository(DbContext context) : base(context)
        {

        }

        public Driveline GetLineByNumber(int number)
        {
            return AppDbContext.DriveLines.Where(l => l.Number == number).FirstOrDefault();
        }
    }
}