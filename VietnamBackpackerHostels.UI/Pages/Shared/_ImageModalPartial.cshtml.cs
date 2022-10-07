using System;
using System.Collections.Generic;
using System.Linq;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class ImageModalPartialModel
    {
        public string ModalId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Body { get; set; }

        public ImageModalPartialModel(string modalId, string title, string imageUrl, string body)
        {
            ModalId = modalId;
            Title = title;
            ImageUrl = imageUrl;
            Body = body;
        }
    }
}
