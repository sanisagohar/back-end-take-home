using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Data.Entities
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA3 { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}
