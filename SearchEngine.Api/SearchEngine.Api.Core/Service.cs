using SearchEngine.Api.Core.Dto;
using SearchEngine.Api.Core.Manager;
using SearchEngine.Api.Infrastructure;
using SearchEngine.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Core
{
    public class Service
    {
        CsvContext _context;
        AirportManager _airportManager;
        RouteManager _routeManager;

        public Service()
        {
            _context = new CsvContext();
            _airportManager = new AirportManager(_context);
            _routeManager = new RouteManager(_context);
        }

        public ResponseDto<List<string>> FindShortestRoute(SearchRequestDto searchRequest)
        {
            ResponseDto<List<string>> response = new ResponseDto<List<string>>();

            try
            {
                if (string.IsNullOrEmpty(searchRequest.Origin) || string.IsNullOrEmpty(searchRequest.Destination))
                {
                    response.Success = false;
                    response.Message = "Both origin and destination must be provided";
                    return response;
                }

                var sourceAirport = _airportManager.FindAirport(searchRequest.Origin);
                if (sourceAirport == null)
                {
                    response.Success = false;
                    response.Message = "Origin cannot be found";
                    return response;
                }

                var destAirport = _airportManager.FindAirport(searchRequest.Destination);
                if (destAirport == null)
                {
                    response.Success = false;
                    response.Message = "Destination cannot be found";
                    return response;
                }

                var shortestRoute = _routeManager.FindShortestRoute(sourceAirport.IATA3, destAirport.IATA3, new List<string>());
                if (shortestRoute.Count == 0)
                {
                    response.Success = true;
                    response.Message = $"No flight found between '{sourceAirport.Name},{sourceAirport.Country},{sourceAirport.City},{sourceAirport.IATA3}' and '{destAirport.Name},{destAirport.Country},{destAirport.City},{destAirport.IATA3}'";
                    return response;
                }

                response.Result = shortestRoute;
                response.Success = true;
                response.Message = $"Records fetched successfully between '{sourceAirport.Name},{sourceAirport.Country},{sourceAirport.City},{sourceAirport.IATA3}' and '{destAirport.Name},{destAirport.Country},{destAirport.City},{destAirport.IATA3}'";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Some error occurred while searching shortest route '{ex.Message}'";
            }

            return response;
        }
    }
}
