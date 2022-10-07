using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.UI.Models.Shared;

namespace VietnamBackpackerHostels.UI.Pages.Error
{
    public class IndexModel : BasePageModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ExceptionMessage { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {

        }

        public async Task<IActionResult> OnGet(int? statusCode)
        {
            await base.OnGetPageContentAsync();

            var statusCodeReExecuteFeature = Request.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (statusCode == 404 && statusCodeReExecuteFeature != null)
            {
                var path = statusCodeReExecuteFeature.OriginalPath;
                _errorLoggerService.LogError($"Page not found: {path}");
            }
            
            var errorMessage = "An error occurred while processing your request.";

            if (statusCode.HasValue)
            {
                switch (statusCode.Value)
                {
                    case 404:
                        ViewData["Title"] = "Page not found";
                        errorMessage = "Oops, we can't find that page.";
                        break;
                    case 401:
                        ViewData["Title"] = "Unauthorized";
                        errorMessage = "You are not authorized to access this page.";
                        break;
                    default:
                        ViewData["Title"] = "Error";
                        errorMessage = "An error occurred while processing your request.";
                        break;
                }
            }

            ExceptionMessage = errorMessage;

            return Page();
        }
    }
}
