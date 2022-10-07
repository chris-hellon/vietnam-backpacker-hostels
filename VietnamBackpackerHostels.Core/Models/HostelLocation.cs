using System;
using System.Collections;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Models
{
    public class HostelLocation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public IEnumerable<Hostel> Hostels { get; set; } = Array.Empty<Hostel>();
    }
}

