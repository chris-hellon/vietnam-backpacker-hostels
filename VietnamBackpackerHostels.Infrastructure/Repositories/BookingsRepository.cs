using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;
using Microsoft.CodeAnalysis;
using Rollbar.DTOs;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class BookingsRepository : BaseRepository, IBookingsRepository
    {
        public BookingsRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async Task<int> CreateHostelReservation(DateTime checkInDate, DateTime checkOutDate, int roomQuantity, decimal amount, string currencyCode, int hostelId, string userId = null, string cloudbedsReservationId = null, int? cloudbedsPropertyId = null)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateHostelReservation", new
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                RoomQuantity = roomQuantity,
                Amount = amount,
                CurrencyCode = currencyCode,
                HostelId = hostelId,
                UserId = userId,
                CloudbedsReservationId = cloudbedsReservationId,
                CloudbedsPropertyId = cloudbedsPropertyId
            });
        }

        public async Task CreateHostelReservationRoom(int hostelReservationId, string roomName, decimal amount, int nights, DateTime checkInDate, DateTime checkOutDate, string guestFirstName, string guestLastName, string cloudbedsGuestId)
        {
            await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateHostelReservationRoom", new
            {
                HostelReservationId = hostelReservationId,
                RoomName = roomName,
                Amount = amount,
                Nights = nights,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                GuestFirstName = guestFirstName,
                GuestLastName = guestLastName,
                CloudbedsGuestId = cloudbedsGuestId
            });
        }
    }
}

