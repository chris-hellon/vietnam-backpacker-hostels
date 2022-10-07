using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.AboutUs
{
    public class IndexModel : BasePageModel
    {
        private readonly IJobVacanciesRepository _jobVacanciesRepository;

        public PageSection Categories { get; set; }
        public PageSectionContent Category { get; set; }
        public PageSection RelatedCategories { get; set; }

        public PageSection HostelLocationsSection { get; set; }
        public PageSection TripLocationsSection { get; set; }

        public IEnumerable<PageSection> CityGuides { get; set; }
        public IEnumerable<JobVacancy> JobVacancies { get; set; }

        public string SelectedCategory { get; set; } = null;

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IJobVacanciesRepository jobVacanciesRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService, 5)
        {
            _jobVacanciesRepository = jobVacanciesRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                await base.OnGetPageContentAsync();

                var pageSections = await _pageSectionsRepository.GetPageSections(_pageId.Value);

                if (pageSections.Any())
                {
                    List<int> categoryIds = new List<int>() { 27, 28, 29, 30 };
                    Categories = pageSections.Where(ps => categoryIds.Contains(ps.Id)).FirstOrDefault();
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

