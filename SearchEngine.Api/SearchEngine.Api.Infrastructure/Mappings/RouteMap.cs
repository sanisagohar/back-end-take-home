using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Data.Entities
{
    public class RouteMap : ClassMap<Route>
    {
        public RouteMap()
        {
            Map(m => m.AirlineId).Name("Airline Id");
            Map(m => m.Origin).Name("Origin");
            Map(m => m.Destination).Name("Destination");
        }
    }
}