using SearchEngine.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SearchEngine.Api.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/search/5
        public HttpResponseMessage Get(string origin, string destination)
        {
            try
            {
                Service service = new Service();
                var request = new Core.Dto.SearchRequestDto { Origin = origin, Destination = destination };

                var response = service.FindShortestRoute(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Some error occurred while processing the request. Please try again later. Error: '{ex.Message}'");
            }
        }
    }
}
