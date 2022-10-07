using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class CarouselPartialModel
    {
        public IEnumerable<CarouselItemPartialModel> Items { get; set; }
        
        public string Id { get; set; }

        public bool Rounded { get; set; }

        public CarouselPartialModel(string id, IEnumerable<CarouselItemPartialModel> items, bool rounded = false)
        {
            Id = id;
            Items = items;
            Rounded = rounded;
        }
    }
}
