using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class PassengerTypeCoefficientRepository : Repository<PassengerTypeCoefficient, int>, IPassengerTypeCoefficientRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public PassengerTypeCoefficientRepository(DbContext context) : base(context)
        {
        }

        public float GetCoefficientForType(PassengerType passengerType)
        {
            return AppDbContext.PassengerTypeCoefficients.Where(c => c.PassengerType == passengerType).FirstOrDefault().Coefficient;
        }
    }
}