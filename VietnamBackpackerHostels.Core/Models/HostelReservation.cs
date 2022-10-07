using System;
using System.Collections.Generic;
namespace VietnamBackpackerHostels.Core.Models
{
    public class HostelReservation
    {
        public int? Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string CloudbedsReservationId { get; set; }
        public int? CloudbedsPropertyId { get; set; }
        public int RoomQuantity { get; set; }
        public IEnumerable<HostelReservationRoom> Rooms { get; set; } = Array.Empty<HostelReservationRoom>();
        public int HostelId { get; set; }
        public string UserId { get; set; }

        public HostelReservation()
        {
        }
    }
}