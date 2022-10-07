using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Services
{
    public class IndexModel : BasePageModel
    {
        public PageSection Services { get; set; }
        public PageSectionContent Service { get; set; }
        public PageSection RelatedServices { get; set; }

        [BindProperty]
        public AirportTransferInputModel AirportTransferInput { get; set; }

        public class AirportTransferInputModel
        {
            [Required(ErrorMessage = "This field is required.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Date Of Arrival")]
            public DateTime? DateOfArrival { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Destination")]
            public string Destination { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Type of Transport")]
            public string TypeOfTransport { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "No. of Pax")]
            public string NumberOfPax { get; set; }

            [Display(Name = "Flight Details")]
            public string FlightDetails { get; set; }
        }

        [BindProperty]
        public VisaLetterInputModel VisaLetterInput { get; set; }

        public class VisaLetterInputModel
        {
            [Required]
            public string Name { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            public string Nationality { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Date Of Arrival")]
            public DateTime? DateOfArrival { get; set; }

            public SelectList Nationalities { get; set; }
        }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService, 4)
        {

        }

        public async Task<IActionResult> OnGet()
        {
            await base.OnGetPageContentAsync();

            try
            {
                var services = await _pageSectionsRepository.GetPageSections(_pageId.Value);

                Services = services.FirstOrDefault();

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

