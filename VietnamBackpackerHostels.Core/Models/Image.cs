using System;

namespace VietnamBackpackerHostels.Core.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = null;
        public string Subtitle { get; set; } = null;
        public string Url1 { get; set; } = null;
        public string Url2 { get; set; } = null;
        public int Index { get; set; } = 0;

        public int? PageId { get; set; }
        public int? HostelId { get; set; }
        public int? TripId { get; set; }
        public int? TripDayId { get; set; }
    }
}

