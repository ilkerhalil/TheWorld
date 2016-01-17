using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private readonly WorldContext _context;
        private readonly UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task EnsureDataAsync()
        {

            if (await _userManager.FindByEmailAsync("ilkerhalil@gmail.com") == null)
            {
                var newUser = new WorldUser()
                {
                    UserName = "iturer",
                    Email = "ilkerhalil@gmail.com"
                };
                await _userManager.CreateAsync(newUser, "P@ssw0rd!");
            }

            if (_context.Trips.Any()) return;
            var usTrip = new Trip
            {
                Name = "TR Trip",
                Created = DateTime.UtcNow,
                UserName = "",
                Stops = new List<Stop>()
                {
                    new Stop
                    {
                        //41°00'N	29°00'E
                        Name = "Ýstanbul",
                        Arrival = new DateTime(2015, 1, 15),
                        Order = 1,
                        Latitude = 41,
                        Longitude = 29
                    },
                    new Stop
                    {
                        //	38°25'N	27°08'E
                        Name = "Ýzmir",
                        Arrival = new DateTime(2015, 3, 6),
                        Order = 2,
                        Latitude = 38.25,
                        Longitude = 27.08
                    },
                    new Stop
                    {
                        //39°57'N	32°54'E
                        Name = "Ankara",
                        Arrival = new DateTime(2015, 5, 8),
                        Order = 3,
                        Latitude = 39.57,
                        Longitude = 32.54
                    },
                    new Stop
                    {
                        //	39°50'N	30°30'E
                        Name = "Eskiþehir",
                        Arrival = new DateTime(2015, 8, 24),
                        Order = 4,
                        Latitude = 39.50,
                        Longitude = 30.30
                    },
                    new Stop
                    {
                        //	39°39'N	27°53'E
                        Name = "Balýkesir",
                        Arrival = new DateTime(2015, 9, 18),
                        Order = 5,
                        Latitude = 39.39,
                        Longitude = 27.53
                    },
                    new Stop
                    {
                        //38°38'N	27°30'E
                        Name = "Manisa",
                        Arrival = new DateTime(2015, 2, 11),
                        Order = 6,
                        Latitude = 38.38,
                        Longitude = 27.30
                    }
                }
            };
            _context.Trips.Add(usTrip);
            _context.Stops.AddRange(usTrip.Stops);
            _context.SaveChanges();
            // Add new Data
        }
    }
}