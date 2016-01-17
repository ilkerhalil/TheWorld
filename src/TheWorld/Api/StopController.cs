using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly IWorldRepository _worldRepository;
        private readonly ILogger<StopController> _logger;
        private readonly BingGeoLocationService _geoLocationService;

        public StopController(IWorldRepository worldRepository, ILogger<StopController> logger, BingGeoLocationService geoLocationService)
        {
            _worldRepository = worldRepository;
            _logger = logger;
            _geoLocationService = geoLocationService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var result = _worldRepository.GetTripByName(tripName);
                return Json(result == null ? null : Mapper.Map<IEnumerable<StopViewModel>>(result.Stops.OrderBy(o => o.Order)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding trip name");
            }
        }
        [HttpPost("")]
        public async Task<JsonResult> Post(string tripName, [FromBody] StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stop);


                    var coordResult = await _geoLocationService.Lookup(stop.Name);
                    if (!coordResult.Success)
                    {
                        _logger.LogError($"Failed coor", coordResult.Message);
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }
                    newStop.Latitude = coordResult.Latitude;
                    newStop.Longitude = coordResult.Longitude;

                    _worldRepository.AddStop(tripName, newStop);
                    if (_worldRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to new stop {tripName}", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }



    }
}