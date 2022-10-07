using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface IHostelsRepository
    {
        Task<IEnumerable<Hostel>> GetHostels();

        Task<Hostel> GetHostel(int id);

        Task<IEnumerable<HostelLocation>> GetHostelLocations();

        Task<IEnumerable<HostelRoom>> GetHostelRooms(int hostelId);

        Task<IEnumerable<LocalTrip>> GetLocalTrips(int hostelId);
    }
}

