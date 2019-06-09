using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface IStationRepository : IRepository<Station, int>
    {
        string[] GetStationNames();
        bool AddStation(string name, string addr, float x, float y);
        bool DeleteStationByName(string name);
    }
}
