using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Models
{
    public class Hostel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string LocalTripUrl { get; set; } = string.Empty;
        public string ShortIntroduction { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string GettingThere { get; set; } = string.Empty;
        public string CloudBedsKey { get; set; } = string.Empty;
        public string GoogleMapsKey { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool Active { get; set; }
        public string LogoImageUrl { get; set; } = string.Empty;
        public int? LocationId { get; set; } = null;
        public HostelLocation Location { get; set; } = null;
        public IEnumerable<HostelRoom> Rooms { get; set; } = Enumerable.Empty<HostelRoom>();
        public IEnumerable<Image> BannerImages { get; set; } = Enumerable.Empty<Image>();
        public IEnumerable<string> SlideshowImages { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<LocalTrip> LocalTrips { get; set; } = Enumerable.Empty<LocalTrip>();
    }
}
