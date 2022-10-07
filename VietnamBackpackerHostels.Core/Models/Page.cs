using System;
namespace VietnamBackpackerHostels.Core.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string PageTitle { get; set; } = string.Empty;
        public int? HostelLocationId { get; set; }
        public int? HostelId { get; set; }
        public int? TripLocationId { get; set; }
        public int? TripId { get; set; }
        public int? ParentId { get; set; }
        public bool Active { get; set; }
    }
}

