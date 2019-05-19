using SearchEngine.Api.Infrastructure;
using SearchEngine.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Core.Manager
{
    public class RouteManager
    {
        private CsvContext _context;
        private AirlineManager _airlineManager;

        public RouteManager(CsvContext context)
        {
            _context = context;
            _airlineManager = new AirlineManager(context);
        }

        public List<string> FindShortestRoute(string sourceIATA, string destIATA, List<string> path)
        {
            List<string> flights = new List<string>();

            if (path.Count() == 0)//for 1st time
                path.Add(sourceIATA);

            if (path.Count() > 5)//Assumption: max number of paths between two nodes would be 5, this is to prevent this algo to run idefinitely
                return flights;

            if (path.Count() > 0)//Check for cyclic path and return
            {
                for (int i = 0; i < path.Count() - 1; i++)
                    if (path[i] == sourceIATA)
                        return flights;
            }

            if (sourceIATA != destIATA)//this check if source and dest are same then return 0 number of routes in between
            {
                var routes = _context.Routes.Where(x => x.Origin.Equals(sourceIATA));

                var shortestRoute = routes.Where(x => x.Destination.Equals(destIATA));
                if (shortestRoute.Count() > 0)
                    flights.Add($"{_airlineManager.GetFlightName(shortestRoute.First().AirlineId)} ({sourceIATA} - {destIATA})");
                else
                {
                    foreach (var route in routes)
                    {
                        var routePath = new List<string>(path);
                        routePath.Add(route.Destination);
                        List<string> fl = new List<string> { $"{_airlineManager.GetFlightName(route.AirlineId)} ({sourceIATA} - {route.Destination})" };
                        var fl1 = FindShortestRoute(route.Destination, destIATA, routePath);
                        if (fl1.Count() > 0)
                        {
                            fl.AddRange(fl1);
                            if(fl.Count() == 2)
                            {
                                flights = fl;
                                break;
                            }
                            else if ((flights.Count() == 0) || (flights.Count() > 0 && fl.Count() < flights.Count()))
                                flights = fl;
                        }
                    }
                }

            }
            return flights;
        }
    }
}
