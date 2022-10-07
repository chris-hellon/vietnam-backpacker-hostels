using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Interfaces.Services;

namespace VietnamBackpackerHostels.UI.Pages.Explore
{
    public class IndexModel : BasePageModel
    {
        public PageSection Locations { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService, 3)
        {
        }
        public async Task<IActionResult> OnGet()
        {
            await OnGetPageContentAsync();

            Locations = await _pageSectionsRepository.GetTripLocationsSectionsPageSection(_pageId.Value);

            return Page();
        }
    }
}
