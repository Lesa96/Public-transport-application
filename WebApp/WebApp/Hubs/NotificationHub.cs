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

        private static Dictionary<string, List<Geolocation>> routes;
        private static Dictionary<string, int> lineIndexes;




        public NotificationHub()
        {
            
            routes = new Dictionary<string, List<Geolocation>>();
            lineIndexes = new Dictionary<string, int>();
            unitOfWork = new DemoUnitOfWork(ApplicationDbContext.Create());
        }

        public NotificationHub(IUnitOfWork unitOfWork)
        {
            Trace.WriteLine("Konstruktor");
            this.unitOfWork = unitOfWork;
            
        }

        public void GetTime()
        {
            routes = BussLocationHelper.Instance.GetRoutes(); // pokupi sve rute
            

            Dictionary<string,string> response =new Dictionary<string, string>();
            if (routes.Count != 0)
            {
                foreach (var rt in routes)
                {
                    if(!lineIndexes.Keys.Contains(rt.Key)) // ako ne postoji brojac za tu liniju
                    {
                        lineIndexes.Add(rt.Key, 0);
                    }

                    if (routes[rt.Key].Count - lineIndexes[rt.Key] == 0) //ako je stigao do pocetne lokacije vrati brojac na 0
                        lineIndexes[rt.Key] = 0;

                    response[rt.Key] = routes[rt.Key][lineIndexes[rt.Key]].lat.ToString() + "," + routes[rt.Key][lineIndexes[rt.Key]].lng.ToString() + ";";
                    lineIndexes[rt.Key]++;
                    

                }

            }

            if (response.Count != 0)
            {
                foreach (var item in response)
                {
                    hubContext.Clients.Group(item.Key).setRealTime(item.Value);
                }

            }
        }

        public void TimeServerUpdates()
        {
            

            timer.Interval = 3000;
            timer.Start();
            timer.Elapsed += OnTimedEvent;
        }
        

        private void OnTimedEvent(object source, ElapsedEventArgs e) //stalno ulazi ovde
        {
            
            //TODO GetVehiclePosition()
            GetTime();
        }

        public void AddToGroupe(string groupe , string conId)
        {
            try
            {
                if(routes.Count != 0)
                    foreach (var line in routes.Keys)
                    {
                        hubContext.Groups.Remove(conId, line);
                    }
            }
            catch (Exception)
            {

                
            }
            
            
           // Groups.Add(conId, groupe);
            hubContext.Groups.Add(conId, groupe);
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