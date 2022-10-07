using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Models
{
    public class TripLocation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public IEnumerable<Trip> Trips { get; set; } = Enumerable.Empty<Trip>();
    }
}

