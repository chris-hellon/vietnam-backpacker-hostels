using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;
using Microsoft.CodeAnalysis;
using Rollbar.DTOs;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class PageSectionsRepository : BaseRepository, IPageSectionsRepository
    {
        public IEnumerable<HostelLocation> HostelLocations { get; set; }
        public IEnumerable<Hostel> Hostels { get; set; }
        public IEnumerable<Hostel> LocationHostels { get; set; }
        public IEnumerable<TripLocation> TripLocations { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<Trip> LocationTrips { get; set; }
        public IEnumerable<VietnamBackpackerHostels.Core.Models.Page> Pages { get; set; } = Enumerable.Empty<VietnamBackpackerHostels.Core.Models.Page>();

        public PageSectionsRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async Task<IEnumerable<Page>> GetPages()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Page>("GetPages");
        }

        public async Task<Page> GetPage(int pageId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Page>("GetPages", new
            {
                PageId = pageId
            });
        }

        public async Task<int> CreatePageSection(int pageId, string title = null)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreatePageSection", new
            {
                PageId = pageId,
                Title = title
            });
        }

        public async Task<int> CreatePageSectionContent(int pageSectionId, string title, string body, string imageUrl, string navigatePage = null, string navigatePageCategory = null, string category = null, string tripName = null, string iconClass = null)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreatePageSectionContent", new
            {
                PageSectionId = pageSectionId,
                Title = title,
                Body = body,
                ImageUrl = imageUrl,
                NavigatePage = navigatePage,
                NavigatePageCategory = navigatePageCategory,
                Category = category,
                TripName = tripName,
                IconClass = iconClass
            });
        }

        public async Task<IEnumerable<PageSection>> GetPageSections(int pageId)
        {
            var pageSectionContents = await DapperConnection.ExecuteGetStoredProcedureList<PageSectionContent>("GetPageSections", new
            {
                PageId = pageId
            });

            if (pageSectionContents.Any())
            {
                var pageSections = pageSectionContents.GroupBy(gb => gb.PageSectionId).ToDictionary(psc => psc.Key, psc => psc.ToList());
                return pageSections.Select(ps => new PageSection(ps.Key, pageId, ps.Value, ps.Value.First().PageSectionTitle));
            }

            return Enumerable.Empty<PageSection>();
        }

        private async Task<IEnumerable<PageSection>> GetSections(int pageId)
        {
            List<PageSection> pageSections = new List<PageSection>();

            var pageSectionContents = new List<PageSectionContent>();

            switch (pageId)
            {
                case 4:
                case 8:
                case 9:
                case 10:
                    var section = await CreatePageSection(pageId);
                    pageSectionContents = new List<PageSectionContent>()
                    {
                        new PageSectionContent(1, 1, "", "Airport Transfer", "<p>No matter which of our hostels you are staying at, if you are arriving by plane, VBH has got you covered. Let us help you, by arranging a hassle-free transfer.</p>", "/Services/Index", "airport-transfer", "Services", "fa-plane"),
                        new PageSectionContent(2, 1, "", "Visa Letters", "<p>Most nationalites need a visa to enter Vietnam. Just let us know your information and we can help organize an invitation letter.</p>", "/Services/Index", "visa-letters", "Services", "fa-envelope-open-text"),
                        new PageSectionContent(3, 1, "", "Travel Insurance", "<p>It is important to make sure you have travel insurance sorted for your trip. At VBH we work closely with World Nomads to help provide you with the cover you need.</p>", "/Services/Index", "travel-insurance", "Services", "fa-passport")
                    };

                    foreach (var content in pageSectionContents)
                    {
                        await CreatePageSectionContent(section, content.Title, content.Body, content.ImageUrl, content.NavigatePage, content.NavigatePageCategory, content.Category, content.TripName, content.IconClass);
                    }

                    break;
            }

            #region Trips

            #endregion

            return pageSections;
        }

        public async Task<PageSection> GetTripLocationsSectionsPageSection(int pageId)
        {
            return await Task.Run(() =>
            {
                return new PageSection(0, pageId, TripLocations.Select(tl => new PageSectionContent(0, 0, tl.ImageUrl, tl.Name, tl.Description, "/TripCategory/Index", tl.Name.UrlFriendly(), "Explore", "fa-car", null, string.Join(", ", tl.Trips.Select(t => t.Title)))));
            });
        }

        public async Task<PageSection> GetHostelLocationsPageSection(int pageId)
        {
            return await Task.Run(() =>
            {
                return new PageSection(0, pageId, HostelLocations.Select(hl => new PageSectionContent(0, 0, hl.ImageUrl, hl.Name, hl.Description, "/Hostel/Index", hl.Name.UrlFriendly(), "Sleep & Eat", "fa-bed", null)));
            });
        }

        public async Task<PageSection> GetTripsPageSection(int pageId)
        {
            return await Task.Run(() =>
            {
                return new PageSection(0, pageId, Trips.Select(t => new PageSectionContent(0, 0, t.BackgroundImageUrl, t.Title, t.Description.Split("</p>")[0] + "</p>", "/Trip/Index", t.Location.Name.UrlFriendly(), "Explore", "fa-car", t.Title.UrlFriendly())));
            });
        }
    }
}

