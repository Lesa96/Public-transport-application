using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;
using WebApp.Persistence;
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
        protected ApplicationDbContext AppDbContext = new ApplicationDbContext();

        private static Timer timer = new Timer();
        private IUnitOfWork unitOfWork;
        private Dictionary<int, List<Station>> lineStations = new Dictionary<int, List<Station>>();

        public NotificationHub()
        {
            unitOfWork = new DemoUnitOfWork(ApplicationDbContext.Create());
        }

        public NotificationHub(IUnitOfWork unitOfWork)
        {
            Trace.WriteLine("Konstruktor");
            this.unitOfWork = unitOfWork;
            
        }

        public void GetTime()
        {
            string response = "";
            //int index = 0;
            //foreach (var item in lineStations) // za svaku liniju
            //{
            //    response += item.Key.ToString() + ":"; //broj :

            //    Coordinates cor = unitOfWork.CoordinatesRepository.Find(
            //        x => x.CoordinatesId == lineStations[item.Key][index].CoordinatesId).FirstOrDefault(); //koordinate trenutne stanice

            //    response += cor.CoordX.ToString() + "," + cor.CoordX.ToString() + ";"; // trenutnaX,trenutnaY;
            //    index++;

            //}
            ////krajnji response:
            ////  4:45.12,19.154; 7:46.12,19.7754;
            ////  

            //Svim klijentima se salje setRealTime poruka
            Clients.All.setRealTime(response);
            //Clients.All.setNewPosition();
        }

        public void TimeServerUpdates()
        {
           // ApplicationDbContext dbContext = ApplicationDbContext.Create();
            //List<Driveline> lines = unitOfWork.Drivelines.GetAllDriveLines();
            //foreach (var line in lines)
            //{
            //    lineStations.Add(line.Number, new List<Station>());
            //    foreach (var station in line.Stations)
            //    {
            //        lineStations[line.Number].Add(station);
            //    }
            //}

            timer.Interval = 3000;
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
            Trace.WriteLine("OnConnect");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}