using Roznamah.Model.Entities;
using Roznamah.Model.Venue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Services.Interfaces
{
    public interface IVenueService
    {
        IEnumerable<Venue> GetAllVenues();
        Venue GetVenue(int venueId);
        VenueDetails GetVenueDetails(int venueId);
        VenueLocation GetVenueLocation(int venueId);
        IEnumerable<FloorMap> FloorMap();
    }
}
