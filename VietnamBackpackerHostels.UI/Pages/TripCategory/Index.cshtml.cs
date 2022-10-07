using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Models;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.TripCategory
{
    public class IndexModel : BasePageModel
    {
        public PageSection Locations { get; set; }
        public TripLocation TripLocation { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
        }
        public async Task<IActionResult> OnGet(string category)
        {
            var page = await OverridePageId(category);

            await OnGetPageContentAsync();

            try
            {
                if (page != null && page.TripLocationId.HasValue)
                    TripLocation = TripLocations.Where(tl => tl.Id == page.TripLocationId.Value).FirstOrDefault();
                else
                    TripLocation = TripLocations.Where(tl => tl.Name.UrlFriendly() == category).FirstOrDefault();

                if (TripLocation != null && page != null)
                {
                    var pageSections = await _pageSectionsRepository.GetPageSections(_pageId.Value);

                    if (pageSections.Any())
                        Locations = pageSections.FirstOrDefault();
                }

                return Page();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }
    }
}
