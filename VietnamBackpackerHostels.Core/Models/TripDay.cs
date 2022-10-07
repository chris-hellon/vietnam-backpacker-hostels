using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Models
{
    public class TripDay
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string DayTitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Food { get; set; } = string.Empty;
        public string Accommodation { get; set; } = string.Empty;
        public string WalkingActivity { get; set; } = string.Empty;
        public string BikeActivity { get; set; } = string.Empty;
        public string EveningActivity { get; set; } = string.Empty;
        public string BoatActivity { get; set; } = string.Empty;
        public bool FreeTime { get; set; }
        public string TravelMethod { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();
    }
}