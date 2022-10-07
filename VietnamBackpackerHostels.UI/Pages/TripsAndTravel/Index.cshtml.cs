using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Models;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.TripsAndTravel
{
    public class IndexModel : BasePageModel
    {
        public PageSection HostelLocationsSection { get; set; }
        public PageSection TripLocationsSection { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService, 15)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            await base.OnGetPageContentAsync();

            try
            {
                var hostelLocationsSection = _pageSectionsRepository.GetHostelLocationsPageSection(_pageId.Value);
                var tripsSection = _pageSectionsRepository.GetTripsPageSection(_pageId.Value);

                await Task.WhenAll(hostelLocationsSection, tripsSection);

                HostelLocationsSection = hostelLocationsSection.Result;
                TripLocationsSection = tripsSection.Result;

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


