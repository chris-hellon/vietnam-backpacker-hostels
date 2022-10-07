using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class HeaderBannerPartialModel : PageModel
    {
        public IEnumerable<HostelLocation> HostelLocations { get; set; } = Enumerable.Empty<HostelLocation>();
        public IEnumerable<TripLocation> TripLocations { get; set; } = Enumerable.Empty<TripLocation>();
        public IEnumerable<Image> BannerImages { get; set; } = Enumerable.Empty<Image>();
        public IEnumerable<VietnamBackpackerHostels.Core.Models.Hostel> Hostels { get; set; } = Enumerable.Empty<VietnamBackpackerHostels.Core.Models.Hostel>();

        public HeaderBannerPartialModel()
        {

        }

        public HeaderBannerPartialModel(IEnumerable<HostelLocation> hostelLocations, IEnumerable<TripLocation> tripLocations, IEnumerable<Image> bannerImages, IEnumerable<VietnamBackpackerHostels.Core.Models.Hostel> hostels)
        {
            HostelLocations = hostelLocations;
            TripLocations = tripLocations;
            BannerImages = bannerImages;
            Hostels = hostels;
        }
    }
}
