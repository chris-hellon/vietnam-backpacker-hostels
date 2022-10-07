using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface ITripsRepository
    {
        Task<IEnumerable<TripLocation>> GetTripLocations();

        Task<Trip> GetTrip(int id);

        Task<IEnumerable<Trip>> GetTrips();

        Task<IEnumerable<Trip>> GetLocationTrips(int locationId);

        Task<IEnumerable<TripDay>> GetTripDays(int tripId);

        Task<IEnumerable<TripImportantInformation>> GetTripImportantInformation(int tripId);
    }
}

