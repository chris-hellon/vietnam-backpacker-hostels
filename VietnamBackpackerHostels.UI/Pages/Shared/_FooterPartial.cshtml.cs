using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class FooterPartialModel
    {
        public IEnumerable<HostelLocation> HostelLocations { get; set; } = Array.Empty<HostelLocation>();
        public IEnumerable<TripLocation> TripLocations { get; set; } = Array.Empty<TripLocation>();

        public FooterPartialModel()
        {

        }

        public FooterPartialModel(IEnumerable<HostelLocation> hostelLocations, IEnumerable<TripLocation> tripLocations)
        {
            HostelLocations = hostelLocations;
            TripLocations = tripLocations;
        }
    }
}
