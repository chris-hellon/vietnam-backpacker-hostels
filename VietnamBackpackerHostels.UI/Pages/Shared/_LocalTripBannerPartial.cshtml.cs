using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class LocalTripBannerPartialModel
    {
        public bool JustifyContent { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }

        public LocalTripBannerPartialModel(string title, string body, string imageUrl, bool justifyContent = true)
        {
            JustifyContent = justifyContent;
            Title = title;
            Body = body;
            ImageUrl = imageUrl;
        }
    }
}
