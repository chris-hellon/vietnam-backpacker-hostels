using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Services.VisaLetters
{
    public class IndexModel : BasePageModel
    {
        public PageSection Services { get; set; }
        public PageSectionContent Service { get; set; }
        public PageSection RelatedServices { get; set; }

        private readonly IEmailService _emailService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRenderer;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
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

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IEmailService emailService, IRazorPartialToStringRenderer razorPartialToStringRenderer) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
            _emailService = emailService;
            _razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public async Task<IActionResult> OnGet()
        {
            var page = await OverridePageId("visa-letters");

            await OnGetPageContentAsync();

            try
            {
                var services = await _pageSectionsRepository.GetPageSections(_pageId.Value);
                Services = services.FirstOrDefault();

                if (Services != null)
                {
                    Service = Services.PageSectionContent.Where(s => s.Title == "Visa Letters").FirstOrDefault();
                    RelatedServices = new PageSection(1, 2, Services.PageSectionContent.Where(s => s.Id != Service.Id));
                }

                Input = new InputModel()
                {
                    Nationalities = new SelectList(Core.Utils.Helpers.GetNationalities(), "Key", "Value", "")
                };

                if (User.Identity.IsAuthenticated)
                {
                    var authenticatedUser = await _userManager.FindByEmailAsync(User.Identity.Name);
                    if (authenticatedUser != null)
                    {
                        Input.Name = $"{authenticatedUser.FirstName} {authenticatedUser.Surname}";
                        Input.Email = authenticatedUser.Email;
                        Input.Nationality = authenticatedUser.Nationality;
                        Input.Nationalities = new SelectList(Core.Utils.Helpers.GetNationalities(), "Key", "Value", authenticatedUser.Nationality);
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
                var emailHtml = await _razorPartialToStringRenderer.RenderPartialToStringAsync("/Pages/EmailTemplates/VisaLettersTemplate.cshtml", new EmailTemplates.VisaLettersTemplateModel(Input.Name, Input.Email, Input.Nationality, Input.DateOfArrival.Value));
                await _emailService.Send("info@vietnambackpackerhostels.com", "Visa Letters Request", emailHtml, "Vietnam Backpacker Hostels", "vnbackpackerhostels@gmail.com", new[] { "chrisghellon@gmail.com" });
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
