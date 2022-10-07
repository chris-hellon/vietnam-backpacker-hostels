using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;

namespace VietnamBackpackerHostels.UI.Pages.AboutUs.Community
{
    public class IndexModel : BasePageModel
    {
        public PageSection Categories { get; set; }
        public PageSectionContent Category { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {

        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var page = await OverridePageId("community");

                await OnGetPageContentAsync();

                var pageSections = await _pageSectionsRepository.GetPageSections(_pageId.Value);

                if (pageSections.Any())
                {
                    List<int> categoryIds = new List<int>() { 27, 28, 29, 30 };
                    Categories = pageSections.Where(ps => categoryIds.Contains(ps.Id)).FirstOrDefault();

                    if (Categories != null)
                        Category = Categories.PageSectionContent.Where(s => s.NavigatePageCategory == "community").FirstOrDefault();
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
