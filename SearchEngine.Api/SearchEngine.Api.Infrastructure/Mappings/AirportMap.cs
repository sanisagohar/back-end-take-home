using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Data.Entities
{
    public class AirportMap : ClassMap<Airport>
    {
        public AirportMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.City).Name("City");
            Map(m => m.Country).Name("Country");
            Map(m => m.IATA3).Name("IATA 3");
            Map(m => m.Latitude).Name("Latitute");
            Map(m => m.Longtitude).Name("Longitude");
        }
    }
}
