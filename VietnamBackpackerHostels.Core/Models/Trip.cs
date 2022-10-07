using System;

namespace VietnamBackpackerHostels.Core.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string WhatsIncluded { get; set; } = string.Empty;
        public string WhatsNotIncluded { get; set; } = string.Empty;
        public string BackgroundImageUrl { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string PaxQuantity { get; set; } = string.Empty;
        public TripLocation Location { get; set; } = null;
    }
}

