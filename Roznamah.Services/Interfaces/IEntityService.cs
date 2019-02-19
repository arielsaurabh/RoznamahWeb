using Roznamah.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Services.Interfaces
{
    public interface IEntitiesService
    {
        IEnumerable<GovernmentEntities> GetAllGovernmentEntities();
        IEnumerable<EventOrgnizer> GetAllEventOrganizers();
        IEnumerable<VenueOwners> GetAllVenueOwners();
        GovernmentEntities GetGovtEntity(int entityId);
        EventOrgnizer GetEventOrganizer(int organizerId);
        VenueOwners GetVenueOwner(int ownerId);
    }
}
