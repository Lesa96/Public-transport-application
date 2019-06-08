using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface IDrivelineRepository : IRepository<Driveline,int>
    {
        Driveline GetLineByNumber(int number);
        Driveline GetLineById(int id);
        bool AddDriveline(int number, List<string> stationNames);
        bool UpdateDriveline(int id, int number, List<string> stationNames);
        bool DeleteDriveline(int number);
        bool AddStationInLine(int drivelineId, int stationID);
        bool DeleteStationInLine(int drivelineId, int stationID);
        bool UpdateNumber(int drivelineId, int drivelineNumber);
        List<string> GetDrivelineNumbersAndIds();
        string[] GetDrivelineStationsNames(int id);



        List<Driveline> GetAllDriveLines();
    }
}
