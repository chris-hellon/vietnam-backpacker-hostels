using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class HostelsRepository : BaseRepository, IHostelsRepository
    {
        public HostelsRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Hostel>> GetHostels()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Hostel>("GetHostels");
        }

        public async Task<Hostel> GetHostel(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Hostel>("GetHostels", new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<HostelLocation>> GetHostelLocations()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<HostelLocation>("GetHostelLocations");
        }

        public async Task<IEnumerable<HostelRoom>> GetHostelRooms(int hostelId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<HostelRoom>("GetHostelRooms", new
            {
                HostelId = hostelId
            });
        }

        public async Task<IEnumerable<LocalTrip>> GetLocalTrips(int hostelId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<LocalTrip>("GetLocalTrips", new
            {
                HostelId = hostelId
            });
        }
    }
}

