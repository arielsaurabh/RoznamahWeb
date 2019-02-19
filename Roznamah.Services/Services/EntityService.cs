using Roznamah.Model.Entities;
using Roznamah.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace Roznamah.Services
{
    public class EntitiesService : IEntitiesService
    {
        private readonly IContentService _contentService;

        public EntitiesService() {
            _contentService = ApplicationContext.Current.Services.ContentService; ;
        }

        public IEnumerable<GovernmentEntities> GetAllGovernmentEntities()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var gvtEntity = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "GovernmentEntity");
            
            List<GovernmentEntities> GvtEntityList = new List<GovernmentEntities>();
            foreach (var item in gvtEntity)
            {
                GvtEntityList.Add(new GovernmentEntities
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEn = Convert.ToString(item.GetValue("EntityName")),
                    NameAr = Convert.ToString(item.GetValue("EntityNameArabic")),
                    DescriptionEn = Convert.ToString(item.GetValue("Description")),
                    DescriptionAr = Convert.ToString(item.GetValue("DescriptionArabic")),
                    Website = Convert.ToString(item.GetValue("EntityWebsite")),
                    RelatedVenues = Convert.ToString(item.GetValue("RelatedVenues"))
                });

            }
            return GvtEntityList;
        }

        public GovernmentEntities GetGovtEntity(int entityId)
        {
            var gvtEntityData = _contentService.GetById(entityId);
            GovernmentEntities GvtEntityItem = new GovernmentEntities
            {
                Id = gvtEntityData.Id,
                Name = gvtEntityData.Name,
                NameEn = Convert.ToString(gvtEntityData.GetValue("EntityName")),
                NameAr = Convert.ToString(gvtEntityData.GetValue("EntityNameArabic")),
                DescriptionEn = Convert.ToString(gvtEntityData.GetValue("Description")),
                DescriptionAr = Convert.ToString(gvtEntityData.GetValue("DescriptionArabic")),
                Website = Convert.ToString(gvtEntityData.GetValue("EntityWebsite")),
                RelatedVenues = Convert.ToString(gvtEntityData.GetValue("RelatedVenues"))
            };
            return GvtEntityItem;
        }

        public IEnumerable<EventOrgnizer> GetAllEventOrganizers()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var eventOrganizers = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "Organizer");

            List<EventOrgnizer> EventOrganizerList = new List<EventOrgnizer>();
            foreach (var item in eventOrganizers)
            {
                EventOrganizerList.Add(new EventOrgnizer
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEn = Convert.ToString(item.GetValue("OrganizerName")),
                    NameAr = Convert.ToString(item.GetValue("OrganizerNameArabic")),
                    DescriptionEn = Convert.ToString(item.GetValue("Description")),
                    DescriptionAr = Convert.ToString(item.GetValue("DescriptionArabic")),
                    Website = Convert.ToString(item.GetValue("EntityWebsite")),
                    ContactInformation = Convert.ToString(item.GetValue("ContactInfo"))
                });

            }

            return EventOrganizerList;
        }

        public EventOrgnizer GetEventOrganizer(int organizerId)
        {
            var eventOrgnizer = _contentService.GetById(organizerId);
            EventOrgnizer GvtEntityItem = new EventOrgnizer
            {
                Id = eventOrgnizer.Id,
                Name = eventOrgnizer.Name,
                NameEn = Convert.ToString(eventOrgnizer.GetValue("OrganizerName")),
                NameAr = Convert.ToString(eventOrgnizer.GetValue("OrganizerNameArabic")),
                DescriptionEn = Convert.ToString(eventOrgnizer.GetValue("Description")),
                DescriptionAr = Convert.ToString(eventOrgnizer.GetValue("DescriptionArabic")),
                Website = Convert.ToString(eventOrgnizer.GetValue("EntityWebsite")),
                ContactInformation = Convert.ToString(eventOrgnizer.GetValue("ContactInfo"))
            };
            return GvtEntityItem;
        }

        public IEnumerable<VenueOwners> GetAllVenueOwners()
        {
            var root = _contentService.GetRootContent().FirstOrDefault();
            var venueOwners = _contentService.GetDescendants(root.Id).Where(x => x.ContentType.Alias == "VenuesOwner");

            List<VenueOwners> VenueOwnerList = new List<VenueOwners>();
            foreach (var item in venueOwners)
            {
                VenueOwnerList.Add(new VenueOwners
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameEn = Convert.ToString(item.GetValue("OwnerName")),
                    NameAr = Convert.ToString(item.GetValue("nameArabic")),
                    DescriptionEn = Convert.ToString(item.GetValue("Description")),
                    DescriptionAr = Convert.ToString(item.GetValue("DescriptionArabic")),
                    Website = Convert.ToString(item.GetValue("EntityWebsite")),
                    ContactInformation = Convert.ToString(item.GetValue("ContactInfo"))
                });

            }

            return VenueOwnerList;
        }

        public VenueOwners GetVenueOwner(int ownerId)
        {
            var eventOrgnizer = _contentService.GetById(ownerId);
            VenueOwners EventOrgnizerItem = new VenueOwners
            {
                Id = eventOrgnizer.Id,
                Name = eventOrgnizer.Name,
                NameEn = Convert.ToString(eventOrgnizer.GetValue("OwnerName")),
                NameAr = Convert.ToString(eventOrgnizer.GetValue("nameArabic")),
                DescriptionEn = Convert.ToString(eventOrgnizer.GetValue("Description")),
                DescriptionAr = Convert.ToString(eventOrgnizer.GetValue("DescriptionArabic")),
                Website = Convert.ToString(eventOrgnizer.GetValue("EntityWebsite")),
                ContactInformation = Convert.ToString(eventOrgnizer.GetValue("ContactInfo"))
            };
            return EventOrgnizerItem;
        }

    }
}
