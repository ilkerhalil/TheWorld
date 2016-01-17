using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace TheWorld.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }

    }
}