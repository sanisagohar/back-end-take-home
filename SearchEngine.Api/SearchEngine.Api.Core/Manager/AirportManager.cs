using SearchEngine.Api.Infrastructure;
using SearchEngine.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Core.Manager
{
    public class AirportManager
    {
        CsvContext _context;

        public AirportManager(CsvContext context)
        {
            _context = context;
        }

        public Airport FindAirport(string searchString)
        {
            Airport airport = null;

            airport = _context.Airports.FirstOrDefault(x => x.Name.Equals(searchString)
                                                || x.City.Equals(searchString)
                                                || x.Country.Equals(searchString)
                                                || x.IATA3.Equals(searchString));

            if (airport == null)
                airport = _context.Airports.FirstOrDefault(x => x.Name.Contains(searchString)
                                                    || x.City.Contains(searchString)
                                                    || x.Country.Contains(searchString)
                                                    || x.IATA3.Contains(searchString));

            return airport;
        }

    }
}
