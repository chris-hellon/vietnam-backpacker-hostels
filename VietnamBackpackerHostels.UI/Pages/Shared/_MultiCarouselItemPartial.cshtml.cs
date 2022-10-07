using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class MultiCarouselItemPartialModel
    {
        public string ImageSrc { get; set; }
        public string AltText { get; set; }
        public string ModalId { get; set; }
        public string ModalTitle { get; set; }
        public string FooterTitle { get; set; }

        public MultiCarouselItemPartialModel(string imageSrc, string altText = null, string modalId = null, string modalTitle = null, string footerTitle = null)
        {
            ImageSrc = imageSrc;
            AltText = altText;
            ModalId = modalId;
            ModalTitle = modalTitle;
            FooterTitle = footerTitle;
        }
    }
}
