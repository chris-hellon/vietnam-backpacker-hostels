using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using System.ComponentModel.DataAnnotations;
using ProjectBase.Core.Interfaces.Services;
using Org.BouncyCastle.Crypto.Macs;
using AspNetCore.ReCaptcha;

namespace VietnamBackpackerHostels.UI.Pages.Contact
{
    [ValidateReCaptcha]
    public class IndexModel : BasePageModel
    {
        private readonly IEmailService _emailService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRenderer;

        [BindProperty]
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "This field is required.")]
        public string Message { get; set; } = string.Empty;

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IEmailService emailService, IRazorPartialToStringRenderer razorPartialToStringRenderer) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
            _emailService = emailService;
            _razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public async Task<IActionResult> OnGet()
        {
            await OnGetPageContentAsync();

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var authenticatedUser = await _userManager.FindByEmailAsync(User.Identity.Name);

                    if (authenticatedUser != null)
                    {
                        Name = $"{authenticatedUser.FirstName} {authenticatedUser.Surname}";
                        Email = authenticatedUser.Email;
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

        public async Task<IActionResult> OnPost([FromServices] IReCaptchaService service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["ErrorMessage"] = "<p>Please complete the Google Captcha to send your message.</p>";
                    return await OnGet();
                }

                var emailHtml = await _razorPartialToStringRenderer.RenderPartialToStringAsync("/Pages/EmailTemplates/ContactTemplate.cshtml", new EmailTemplates.ContactTemplateModel(Name, Email, Message));
                await _emailService.Send("info@vietnambackpackerhostels.com", "Website Enquiry", emailHtml, "Vietnam Backpacker Hostels", "vnbackpackerhostels@gmail.com", new[] { "chrisghellon@gmail.com" });
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

