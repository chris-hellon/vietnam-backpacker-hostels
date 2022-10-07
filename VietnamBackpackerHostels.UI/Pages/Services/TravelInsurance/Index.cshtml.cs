using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Services.TravelInsurance
{
    public class IndexModel : BasePageModel
    {
        public PageSection Services { get; set; }
        public PageSectionContent Service { get; set; }
        public PageSection RelatedServices { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            var page = await OverridePageId("travel-insurance");

            await OnGetPageContentAsync();

            try
            {
                var services = await _pageSectionsRepository.GetPageSections(_pageId.Value);
                Services = services.FirstOrDefault();

                if (Services != null)
                {
                    Service = Services.PageSectionContent.Where(s => s.Title == "Travel Insurance").FirstOrDefault();
                    RelatedServices = new PageSection(1, 2, Services.PageSectionContent.Where(s => s.Id != Service.Id));
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
