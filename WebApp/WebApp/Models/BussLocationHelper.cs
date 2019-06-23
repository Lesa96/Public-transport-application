using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApp.Models
{
    public class BussLocationHelper
    {
        private static BussLocationHelper instance = null;
        private static readonly object lockObject = new object();

        private static Dictionary<string, List<Geolocation>> routes;

        public BussLocationHelper()
        {
            routes = new Dictionary<string, List<Geolocation>>();
        }

        public static BussLocationHelper Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BussLocationHelper();
                    }

                    return instance;
                }
            }
        }

        public void AddRoutes(RoutesBindingModel routesBindingModel)
        {
            if(!routes.Keys.Contains(routesBindingModel.LineNumber.ToString()))
            {
                routes.Add(routesBindingModel.LineNumber.ToString(), new List<Geolocation>());
                for (int i = 0; i < routesBindingModel.RouteCoordinates.Count(); i++)
                {
                    routes[routesBindingModel.LineNumber.ToString()].Add(routesBindingModel.RouteCoordinates[i]);
                }

                //salji ka hubu

            }
            
            
        }

        public Dictionary<string, List<Geolocation>> GetRoutes()
        {
            return routes;
        }
    }
}