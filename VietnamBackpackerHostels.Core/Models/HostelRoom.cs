using System;
using System.Collections;

namespace VietnamBackpackerHostels.Core.Models
{
    public class HostelRoom
    {
        public int Id { get; set; }
        public int HostelId { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public string RoomDescription { get; set; } = string.Empty;
        public int Order { get; set; }
        public string RoomImageUrl { get; set; } = string.Empty;
        public string RoomImageTypeUrl { get; set; } = string.Empty;
    }
}