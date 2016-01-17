using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly IWorldRepository _worldRepository;

        public TripController(IWorldRepository worldRepository)
        {
            _worldRepository = worldRepository;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            var results = _worldRepository.GetUserTripsWithStops(User.Identity.Name);
            var rt = Mapper.Map<IEnumerable<TripViewModel>>(results);
            return Json(rt);
        }
        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel trip)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var newTrip = Mapper.Map<Trip>(trip);
                    newTrip.UserName = User.Identity.Name;
                    //Save to the database
                    _worldRepository.AddTrip(newTrip);
                    if (_worldRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { e.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }



    }
}
