using System;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface IBookingsRepository
    {
        Task<int> CreateHostelReservation(DateTime checkInDate, DateTime checkOutDate, int roomQuantity, decimal amount, string currencyCode, int hostelId, string userId = null, string cloudbedsReservationId = null, int? cloudbedsPropertyId = null);
        Task CreateHostelReservationRoom(int hostelReservationId, string roomName, decimal amount, int nights, DateTime checkInDate, DateTime checkOutDate, string guestFirstName, string guestLastName, string cloudbedsGuestId);
    }
}

