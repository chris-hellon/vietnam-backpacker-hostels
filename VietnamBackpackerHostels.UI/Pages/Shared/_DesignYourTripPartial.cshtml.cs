using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class DesignYourTripPartialModel
    {
        public string BackgroundCssClass { get; set; }

        public DesignYourTripPartialModel(string backgroundCssClass = "bg-black")
        {
            BackgroundCssClass = backgroundCssClass;
        }
    }
}
