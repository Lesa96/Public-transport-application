using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface IStationRepository : IRepository<Station, int>
    {
        string[] GetStationNames();
        HttpStatusCode AddStation(string name, string addr, float x, float y);
        HttpStatusCode DeleteStationByName(string name);
        UpdateStationInfoBindingModel GetStationsById(int id);
        List<string> GetStationsIdsAndNames();
        List<UpdateStationInfoBindingModel> GetAllStations();
        HttpStatusCode UpdateStationInfo(UpdateStationInfoBindingModel bindingModel);
    }
}
