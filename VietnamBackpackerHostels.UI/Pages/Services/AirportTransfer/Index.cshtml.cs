using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Services.AirportTransfer
{
    public class IndexModel : BasePageModel
    {
        private readonly IEmailService _emailService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRenderer;

        public PageSection Services { get; set; }
        public PageSectionContent Service { get; set; }
        public PageSection RelatedServices { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
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

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IEmailService emailService, IRazorPartialToStringRenderer razorPartialToStringRenderer) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
            _emailService = emailService;
            _razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public async Task<IActionResult> OnGet()
        {
            var page = await OverridePageId("airport-transfer");

            await OnGetPageContentAsync();

            try
            {
                var services = await _pageSectionsRepository.GetPageSections(_pageId.Value);
                Services = services.FirstOrDefault();

                if (Services != null)
                {
                    Service = Services.PageSectionContent.Where(s => s.Title == "Airport Transfer").FirstOrDefault();
                    RelatedServices = new PageSection(1, 2, Services.PageSectionContent.Where(s => s.Id != Service.Id));
                }

                if (User.Identity.IsAuthenticated)
                {
                    var authenticatedUser = await _userManager.FindByEmailAsync(User.Identity.Name);

                    if (authenticatedUser != null)
                    {
                        Input = new InputModel
                        {
                            Name = $"{authenticatedUser.FirstName} {authenticatedUser.Surname}",
                            Email = authenticatedUser.Email
                        };
                    }
                }

                return Page();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var emailHtml = await _razorPartialToStringRenderer.RenderPartialToStringAsync("/Pages/EmailTemplates/AirportTransferTemplate.cshtml", new EmailTemplates.AirportTransferTemplateModel(Input.Name, Input.Email, Input.DateOfArrival.Value, Input.Destination, Input.TypeOfTransport, Input.NumberOfPax, Input.FlightDetails));
                await _emailService.Send("info@vietnambackpackerhostels.com", "Airport Transfer Request", emailHtml, "Vietnam Backpacker Hostels", "vnbackpackerhostels@gmail.com", new[] { "chrisghellon@gmail.com" });
                ViewData["SuccessMessage"] = "<p>Your request has been sent successfully.</p><p>A member of our team will contact you shortly.</p>";
                ModelState.Clear();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                ViewData["ErrorMessage"] = "<p>There was an error submitting your enquiry.</p><p>Please try again, or contact us at info@vietnambackpackerhostels.com.</p>";
            }

            return await OnGet();
        }
    }
}
