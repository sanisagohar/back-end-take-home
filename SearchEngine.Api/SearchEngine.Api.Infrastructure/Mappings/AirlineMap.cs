using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Data.Entities
{
    public class AirlineMap: ClassMap<Airline>
    {
        public AirlineMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.TwoDigitCode).Name("2 Digit Code");
            Map(m => m.ThreeDigitCode).Name("3 Digit Code");
            Map(m => m.Country).Name("Country");
        }
    }
}
