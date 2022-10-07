using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCore.SEOHelper.Sitemap;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.UI.Pages.Sitemap
{
    public class IndexModel : PageModel
    {
        protected readonly ITripsRepository _tripsRepository;
        protected readonly IHostelsRepository _hostelsRepository;

        public IndexModel(ITripsRepository tripsRepository, IHostelsRepository hostelsRepository)
        {
            _tripsRepository = tripsRepository;
            _hostelsRepository = hostelsRepository;
        }

        public async Task<ContentResult> OnGet()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(await GenerateSitemap());

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };
        }

        private async Task<string> GenerateSitemap()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var sitemapNodes = new List<SitemapNode>()
            {
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = baseUrl, Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.3, Url = baseUrl + "/sleep-and-eat", Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.3, Url = baseUrl + "/explore", Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.3, Url = baseUrl + "/trips-and-travel", Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/services", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/services/airport-transfer", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/services/visa-letters", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/services/travel-insurance", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/contact", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/about-us", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/about-us/city-guides", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/about-us/community", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.3, Url = baseUrl + "/about-us/join-our-crew", Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/terms-and-conditions", Frequency = SitemapFrequency.Yearly }
            };

            var tripLocations = _tripsRepository.GetTripLocations();
            var trips = _tripsRepository.GetTrips();
            var hostelLocations = _hostelsRepository.GetHostelLocations();

            await Task.WhenAll(tripLocations, trips, hostelLocations);

            var tripLocationsResult = tripLocations.Result.Select(tl => { tl.Trips = trips.Result.Where(t => t.LocationId == tl.Id); return tl; });
            var tripsResult = trips.Result.Select(t => { t.Location = tripLocationsResult.Where(tl => tl.Id == t.LocationId).FirstOrDefault(); return t; });

            if (tripLocationsResult.Any())
            {
                foreach (var tripLocation in tripLocationsResult)
                {
                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{baseUrl}/explore/{tripLocation.Name.UrlFriendly()}", Frequency = SitemapFrequency.Monthly });
                }
            }

            if (tripsResult.Any())
            {
                foreach (var trip in tripsResult)
                {
                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{baseUrl}/explore/{trip.Location.Name.UrlFriendly()}/{trip.Title.UrlFriendly()}", Frequency = SitemapFrequency.Monthly });
                }
            }

            if (hostelLocations.Result.Any())
            {
                foreach (var hostelLocation in hostelLocations.Result)
                {
                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{baseUrl}/sleep-and-eat/{hostelLocation.Name.UrlFriendly()}", Frequency = SitemapFrequency.Monthly });
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<SitemapNode>));

            var stringwriter = new StringWriter();
            serializer.Serialize(stringwriter, sitemapNodes);

            return stringwriter.ToString();
        }
    }
}
