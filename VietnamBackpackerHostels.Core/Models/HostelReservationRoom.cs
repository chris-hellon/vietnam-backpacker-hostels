using System;
namespace VietnamBackpackerHostels.Core.Models
{
    public class HostelReservationRoom
    {
        public int? Id { get; set; }
        public int? HostelReservationId { get; set; }
        public string RoomName { get; set; }
        public decimal Amount { get; set; }
        public int Nights { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string CloudbedsGuestId { get; set; }

        public HostelReservationRoom()
        {
        }
    }
}

