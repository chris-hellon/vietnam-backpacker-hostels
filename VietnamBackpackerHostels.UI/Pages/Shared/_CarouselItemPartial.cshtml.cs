using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class CarouselItemPartialModel
    {
        public string ImageSrc { get; set; }
        public string AltText { get; set; }

        public CarouselItemPartialModel(string imageSrc, string altText = null)
        {
            ImageSrc = imageSrc;
            AltText = altText;
        }
    }
}
