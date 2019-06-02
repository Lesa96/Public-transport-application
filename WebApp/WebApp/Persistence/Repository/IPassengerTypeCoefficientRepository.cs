using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface IPassengerTypeCoefficientRepository : IRepository<PassengerTypeCoefficient, int>
    {
        float GetCoefficientForType(PassengerType passengerType);
    }
}
