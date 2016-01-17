using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(or => or.Name).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError("Could not get trips from database", exception);
                return null;
            }
        }
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips.Include(inc => inc.Stops)
                .OrderBy(or => or.Name)
                .ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError("Could not get trip with stops from database", exception);
                return null;
            }

        }

        public void AddTrip(Trip newTrip)
        {
            _context.Trips.Add(newTrip);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .FirstOrDefault(t => t.Name == tripName);
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName);

            newStop.Order = theTrip.Stops.Any() ? theTrip.Stops.Max(s => s.Order) + 1 : 1;
            //theTrip.Stops.Add(newStop);
            _context.Trips.Single(s => s.Name == tripName).Stops.Add(newStop);
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
                 .Include(inc => inc.Stops)
                .OrderBy(or => or.Name)
                .Where(w => w.UserName == name)
                .ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError("Could not get trip with stops from database", exception);
                return null;
            }
        }
    }
}
