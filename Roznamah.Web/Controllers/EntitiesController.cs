using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
using System.Threading.Tasks;
using Roznamah.Model.Entities;
using Roznamah.Services.Interfaces;
using Roznamah.Services;

namespace Roznamah.Web.Controllers
{
    //[Route("api/[controller]")]
    public class EntitiesController : UmbracoApiController
    {
        private readonly IEntitiesService _entityService;

        public EntitiesController(EntitiesService entityService)
        {
            _entityService = entityService;
        }

        #region Government Entity

        [HttpGet]
        public async Task<IEnumerable<GovernmentEntities>> GetAllGovernmentEntities()
        {
            return _entityService.GetAllGovernmentEntities();
        }

        [HttpGet]
        public async Task<GovernmentEntities> GetGovtEntity(int entityId)
        {
            return _entityService.GetGovtEntity(entityId);
        }

        #endregion

        #region Event Organizers

        [HttpGet]
        public async Task<IEnumerable<EventOrgnizer>> GetAllEventOrganizers()
        {
            return _entityService.GetAllEventOrganizers();
        }

        [HttpGet]
        public async Task<EventOrgnizer> GetEventOrganizer(int organizerId)
        {
            return _entityService.GetEventOrganizer(organizerId);
        }

        #endregion

        #region Venue Owner

        [HttpGet]
        public async Task<IEnumerable<VenueOwners>> GetAllVenueOwners()
        {
            return _entityService.GetAllVenueOwners();
        }

        [HttpGet]
        public async Task<VenueOwners> GetVenueOwner(int ownerId)
        {
            return _entityService.GetVenueOwner(ownerId);
        }

        #endregion
    }
}