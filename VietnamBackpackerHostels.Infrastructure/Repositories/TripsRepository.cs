using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class TripsRepository : BaseRepository, ITripsRepository
    {
        public TripsRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<TripLocation>> GetTripLocations()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<TripLocation>("GetTripLocations");
        }

        public async Task<Trip> GetTrip(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Trip>("GetTrips", new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Trip>> GetTrips()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Trip>("GetTrips");
        }

        public async Task<IEnumerable<Trip>> GetLocationTrips(int locationId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Trip>("GetTrips", new
            {
                LocationId = locationId
            });
        }

        public async Task<IEnumerable<TripDay>> GetTripDays(int tripId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<TripDay>("GetTripDays", new
            {
                Id = tripId
            });
        }

        public async Task<IEnumerable<TripImportantInformation>> GetTripImportantInformation(int tripId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<TripImportantInformation>("GetTripImportantInformation", new
            {
                Id = tripId
            });
        }
    }
}

