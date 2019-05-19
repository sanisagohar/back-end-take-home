using SearchEngine.Api.Infrastructure;
using SearchEngine.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Core.Manager
{
    public class AirlineManager
    {
        CsvContext _context;

        public AirlineManager(CsvContext context)
        {
            _context = context;
        }

        public string GetFlightName(string airlineId)
        {
            var airline = _context.Airlines.FirstOrDefault(x => x.TwoDigitCode.Equals(airlineId));
            if (airline == null)
                return airlineId;
            else
                return airline.Name;
        }
    }
}
