using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.UI.Pages.Shared;
using Microsoft.CodeAnalysis;
using ProjectBase.Core.Utils;
using System.Text;

namespace VietnamBackpackerHostels.UI.Pages.Home
{
    public class IndexModel : BasePageModel
    {
        public IEnumerable<LocalTripBannerPartialModel> Introduction { get; set; }
        public PageSection CheckItOut { get; set; }
        public PageSection WhereToNext { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService, 1)
        {

        }

        public async Task<IActionResult> OnGet()
        {
            await OnGetPageContentAsync();

            try
            {
                var pageSections = await _pageSectionsRepository.GetPageSections(_pageId.Value);

                if (pageSections.Any())
                {
                    Introduction = pageSections.Where(ps => ps.Id == 1).FirstOrDefault().PageSectionContent.Select(psc => new LocalTripBannerPartialModel(psc.Title, psc.Body, psc.ImageUrl));
                    CheckItOut = pageSections.Where(ps => ps.Id == 2).FirstOrDefault();
                }

                var whereToNextContent = new List<PageSectionContent>();

                var tripLocationsSection = _pageSectionsRepository.GetTripLocationsSectionsPageSection(_pageId.Value);
                var hostelLocationsSection = _pageSectionsRepository.GetHostelLocationsPageSection(_pageId.Value);

                await Task.WhenAll(tripLocationsSection, hostelLocationsSection);

                whereToNextContent.AddRange(hostelLocationsSection.Result.PageSectionContent);
                whereToNextContent.AddRange(tripLocationsSection.Result.PageSectionContent);

                WhereToNext = new PageSection(0, _pageId.Value, whereToNextContent, "Where to next?");

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

