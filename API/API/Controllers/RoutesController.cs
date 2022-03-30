using API.Controllers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application;

namespace API
{
    public class RoutesController : BaseApiController
    {
        private readonly IRouteFinder _routeFinder;
        public RoutesController(IRouteFinder routeFinder)
        {
            _routeFinder = routeFinder;
        }


        [HttpGet]
        public ActionResult<RouteResult> GetRoute(AirportCodes source, AirportCodes destination)
        {
            if (source == null || destination == null ||
                string.IsNullOrWhiteSpace(source.Iata) && string.IsNullOrWhiteSpace(source.Icao) ||
                string.IsNullOrWhiteSpace(destination.Iata) &&
                string.IsNullOrWhiteSpace(destination.Icao))
            {
                return BadRequest();
            }
            
            var result = _routeFinder.Find((source.Iata, source.Icao),(destination.Iata, destination.Icao));
            
            return Ok(result);
        }

    }
}
