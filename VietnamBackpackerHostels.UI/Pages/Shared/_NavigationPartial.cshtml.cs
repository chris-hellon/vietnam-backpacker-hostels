using System;
using System.Collections.Generic;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class NavigationPartialModel
    {
        public IEnumerable<HostelLocation> HostelLocations { get; set; } = Array.Empty<HostelLocation>();
        public IEnumerable<TripLocation> TripLocations { get; set; } = Array.Empty<TripLocation>();
        public string CssClass { get; set; }

        public NavigationPartialModel()
        {

        }

        public NavigationPartialModel(IEnumerable<HostelLocation> hostelLocations, IEnumerable<TripLocation> tripLocations, bool hasShadow)
        {
            HostelLocations = hostelLocations;
            TripLocations = tripLocations;
            CssClass = hasShadow ? "shadow always-shadow": "";
        }
    }
}
