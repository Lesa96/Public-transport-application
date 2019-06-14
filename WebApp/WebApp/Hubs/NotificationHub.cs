using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Hubs
{
    /**
     * Prilikom pristizanja svake poruke instancira se novi Hub koji
     * je obradjuje.
     * **/
    [HubName("notifications")]
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        
        private static Timer timer = new Timer();
        private IUnitOfWork unitOfWork;
        private Dictionary<int, List<Station>> lineStations = new Dictionary<int, List<Station>>();
        private Dictionary<int, int> nextStationPerLine = new Dictionary<int, int>();

        public NotificationHub(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void GetTime()
        {
            List<string> response = new List<string>();
            foreach (var item in lineStations)
            {
                string newCoords = item.Key + ";" + lineStations[item.Key][nextStationPerLine[item.Key]].Coordinates.CoordX + ";" + lineStations[item.Key][nextStationPerLine[item.Key]].Coordinates.CoordY;
                nextStationPerLine[item.Key] = (nextStationPerLine[item.Key] + 1) % lineStations[item.Key].Count;
                response.Add(newCoords);
            }
            //Svim klijentima se salje setRealTime poruka
            Clients.All.setRealTime(response);
            //Clients.All.setNewPosition();
        }

        public void TimeServerUpdates()
        {
            var lines = unitOfWork.Drivelines.GetAll();
            foreach (var line in lines)
            {
                lineStations.Add(line.Number, new List<Station>());
                foreach (var station in line.Stations)
                {
                    lineStations[line.Number].Add(station);
                }
                nextStationPerLine.Add(line.Number, 0);
            }

            timer.Interval = 1000;
            timer.Start();
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //TODO GetVehiclePosition()
            GetTime();
        }

        public void StopTimeServerUpdates()
        {
            timer.Stop();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}