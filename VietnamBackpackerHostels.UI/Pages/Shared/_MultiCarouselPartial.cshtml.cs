using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class MultiCarouselPartialModel
    {
        public IEnumerable<MultiCarouselItemPartialModel> Items { get; set; }

        public MultiCarouselPartialModel(IEnumerable<MultiCarouselItemPartialModel> items)
        {
            Items = items;
        }
    }
}
