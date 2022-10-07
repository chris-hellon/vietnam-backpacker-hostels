using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Core.Interfaces.Repositories
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Image>> GetBannerImages(int pageId);

        Task<IEnumerable<Image>> GetTripDaysImages(int tripId);

        Task<Image> CreateImage(string fileName, string imageUrl, string createId, string title = null, string subTitle = null, string url1 = null, string url2 = null, int? hostelId = null, int? tripId = null, int? tripDayId = null, int? pageId = null);
    }
}

