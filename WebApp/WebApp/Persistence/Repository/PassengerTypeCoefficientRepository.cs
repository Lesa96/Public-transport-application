using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PassengerTypeCoefficientRepository : Repository<PassengerTypeCoefficient, int>, IPassengerTypeCoefficientRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public PassengerTypeCoefficientRepository(DbContext context) : base(context)
        {
        }
    }
}