using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using ProjectBase.Core.Models;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Trip
{
    public class IndexModel : BasePageModel
    {
        private readonly IEmailService _emailService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRenderer;

        public TripLocation TripLocation { get; set; }
        public Core.Models.Trip SelectedTrip { get; set; }
        public IEnumerable<TripDay> TripDays { get; set; }
        public IEnumerable<TripImportantInformation> ImportantInformation { get; set; }
        public PageSection RelatedTrips { get; set; }

        [BindProperty]
        public EnquireNowInputModel EnquireNowInput { get; set; }

        public class EnquireNowInputModel
        {
            public string TripName { get; set; }
            public string Category { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            public string Email { get; set; }

            [Display(Name = "Requested Date")]
            [Required(ErrorMessage = "This field is required.")]
            public DateTime? Date { get; set; }

            [Display(Name = "Number of People")]
            [Required(ErrorMessage = "This field is required.")]
            public int? NumberOfPeople { get; set; }

            [Display(Name = "Any special requests")]
            public string AdditionalInformation { get; set; }
        }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IEmailService emailService, IRazorPartialToStringRenderer razorPartialToStringRenderer) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
            _emailService = emailService;
            _razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public async Task<IActionResult> OnGet(string category, string tripName)
        {
            var page = await OverridePageId(tripName);

            await OnGetPageContentAsync();

            try
            {
                if (page != null)
                {
                    TripLocation = TripLocations.Where(tl => tl.Id == page.TripLocationId.Value).FirstOrDefault();

                    if (TripLocation != null && Trips.Any())
                    {
                        SelectedTrip = Trips.Where(t => t.Title.UrlFriendly() == tripName).FirstOrDefault();

                        var tripId = SelectedTrip.Id;
                        TripDays = await _tripsRepository.GetTripDays(tripId);

                        var tripDaysImages = _imagesRepository.GetTripDaysImages(tripId);
                        var tripDays = _tripsRepository.GetTripDays(tripId);
                        var tripImportantInformation = _tripsRepository.GetTripImportantInformation(tripId);

                        await Task.WhenAll(tripDaysImages, tripDays, tripImportantInformation);

                        TripDays = tripDays.Result;
                        TripDays = TripDays.Select(td => { td.Images = tripDaysImages.Result.Where(tdi => tdi.TripDayId.HasValue && tdi.TripDayId.Value == td.Id); return td; });
                        ImportantInformation = tripImportantInformation.Result;

                        var relatedTrips = Trips.Where(t => t.Id != tripId).ToList();
                        relatedTrips.Shuffle();

                        relatedTrips = relatedTrips.Take(4).ToList();

                        RelatedTrips = new PageSection(0, _pageId.Value, relatedTrips.Select(rt => new PageSectionContent(0, 0, rt.BackgroundImageUrl, rt.Title, rt.Description.Split("</p>")[0] + "</p>", "/Trip/Index", rt.Location.Name.UrlFriendly(), "Explore", "fa-car", rt.Title.UrlFriendly())));
                        EnquireNowInput = new EnquireNowInputModel();

                        if (User.Identity.IsAuthenticated)
                        {
                            var authenticatedUser = await _userManager.FindByEmailAsync(User.Identity.Name);

                            if (authenticatedUser != null)
                            {
                                
                                EnquireNowInput.Name = $"{authenticatedUser.FirstName} {authenticatedUser.Surname}";
                                EnquireNowInput.Email = authenticatedUser.Email;
                            }
                        }

                        EnquireNowInput.TripName = SelectedTrip.Title;
                        EnquireNowInput.Category = SelectedTrip.Location.Name;
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var emailHtml = await _razorPartialToStringRenderer.RenderPartialToStringAsync("/Pages/EmailTemplates/TourEnquiryTemplate.cshtml", new Pages.EmailTemplates.TourEnquiryTemplateModel(EnquireNowInput.Name, EnquireNowInput.TripName, EnquireNowInput.Email, EnquireNowInput.Date.Value, EnquireNowInput.NumberOfPeople.Value, EnquireNowInput.AdditionalInformation));
                await _emailService.Send("info@vietnambackpackerhostels.com", "Tour Booking Enquiry", emailHtml, "Vietnam Backpacker Hostels", "vnbackpackerhostels@gmail.com", new[] { "chrisghellon@gmail.com" });
                ViewData["SuccessMessage"] = "<p>Your request has been sent successfully.</p><p>A member of our team will contact you shortly.</p>";
                ModelState.Clear();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                ViewData["ErrorMessage"] = "<p>There was an error submitting your enquiry.</p><p>Please try again, or contact us at info@vietnambackpackerhostels.com.</p>";
            }

            return await OnGet(EnquireNowInput.Category.UrlFriendly(), EnquireNowInput.TripName.UrlFriendly());
        }
    }
}


