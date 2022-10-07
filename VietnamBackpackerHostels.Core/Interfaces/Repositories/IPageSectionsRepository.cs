using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface IPageSectionsRepository
    {
        IEnumerable<HostelLocation> HostelLocations { get; set; }
        IEnumerable<Hostel> Hostels { get; set; }
        IEnumerable<Hostel> LocationHostels { get; set; }
        IEnumerable<TripLocation> TripLocations { get; set; }
        IEnumerable<Trip> Trips { get; set; }
        IEnumerable<Trip> LocationTrips { get; set; }
        IEnumerable<VietnamBackpackerHostels.Core.Models.Page> Pages { get; set; }

        Task<IEnumerable<Page>> GetPages();

        Task<Page> GetPage(int pageId);

        Task<IEnumerable<PageSection>> GetPageSections(int pageId);

        Task<int> CreatePageSectionContent(int pageSectionId, string title, string body, string imageUrl, string navigatePage = null, string navigatePageCategory = null, string category = null, string tripName = null, string iconClass = null);

        Task<int> CreatePageSection(int pageId, string title = null);

        Task<PageSection> GetTripLocationsSectionsPageSection(int pageId);

        Task<PageSection> GetHostelLocationsPageSection(int pageId);

        Task<PageSection> GetTripsPageSection(int pageId);
    }
}

