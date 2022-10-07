using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VietnamBackpackerHostels.Core.Models;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.UI.Pages.Shared;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Core.Utils;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;

namespace VietnamBackpackerHostels.UI.Models.Shared
{
    public class BasePageModel : PageModel
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly UserManager<ApplicationUser> _userManager;

        protected readonly IErrorLoggerService _errorLoggerService;
        protected readonly IHostelsRepository _hostelsRepository;
        protected readonly ITripsRepository _tripsRepository;
        protected readonly IImagesRepository _imagesRepository;
        protected readonly IPageSectionsRepository _pageSectionsRepository;
        protected int? _pageId { get; set; }

        public IEnumerable<HostelLocation> HostelLocations { get; set; } = Enumerable.Empty<HostelLocation>();
        public IEnumerable<Hostel> Hostels { get; set; } = Enumerable.Empty<Hostel>();
        public IEnumerable<TripLocation> TripLocations { get; set; } = Enumerable.Empty<TripLocation>();
        public IEnumerable<Trip> Trips { get; set; } = Enumerable.Empty<Trip>();
        public IEnumerable<Image> BannerImages { get; set; } = Enumerable.Empty<Image>();
        public IEnumerable<VietnamBackpackerHostels.Core.Models.Page> Pages { get; set; } = Enumerable.Empty<VietnamBackpackerHostels.Core.Models.Page>();

        public BasePageModel(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository, IErrorLoggerService errorLoggerService, int? pageId = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostelsRepository = hostelsRepository;
            _tripsRepository = tripsRepository;
            _imagesRepository = imagesRepository;
            _pageSectionsRepository = pageSectionsRepository;
            _errorLoggerService = errorLoggerService;
            _pageId = pageId;
        }

        public async Task OnGetPageContentAsync()
        {
            var hostels = _hostelsRepository.GetHostels();
            var hostelLocations = _hostelsRepository.GetHostelLocations();
            var tripLocations = _tripsRepository.GetTripLocations();
            var trips = _tripsRepository.GetTrips();
            var pages = _pageSectionsRepository.GetPages();

            await Task.WhenAll(hostels, hostelLocations, tripLocations, trips, pages);

            if (_pageId.HasValue)
                BannerImages = await _imagesRepository.GetBannerImages(_pageId.Value);

            Hostels = hostels.Result;
            HostelLocations = hostelLocations.Result;
            TripLocations = tripLocations.Result;
            Trips = trips.Result;
            Pages = pages.Result;

            HostelLocations = HostelLocations.Select(hl => { hl.Hostels = Hostels.Where(h => h.LocationId.HasValue && h.LocationId.Value == hl.Id); return hl; });
            Hostels = Hostels.Select(h => { h.Location = HostelLocations.Where(hl => hl.Id == h.LocationId).FirstOrDefault(); return h; });

            TripLocations = TripLocations.Select(tl => { tl.Trips = Trips.Where(t => t.LocationId == tl.Id); return tl; });
            Trips = Trips.Select(t => { t.Location = TripLocations.Where(tl => tl.Id == t.LocationId).FirstOrDefault(); return t; });

            _pageSectionsRepository.Hostels = Hostels;
            _pageSectionsRepository.HostelLocations = HostelLocations;
            _pageSectionsRepository.Trips = Trips;
            _pageSectionsRepository.TripLocations = TripLocations;
            _pageSectionsRepository.Pages = Pages;
        }

        private async Task<Hostel> GetHostel(int id)
        {
            return await _hostelsRepository.GetHostel(id);
        }

        public async Task<IActionResult> OnPostContinueAsGuestAndBook(HeaderBannerBookNowPartialModel model, bool? login = null)
        {
            try
            {
                string userId = Helpers.GetUserId(_httpContextAccessor);

                int? hostelId = model.Id;
                DateTime? checkInDate = model.CheckInDate;
                DateTime? checkOutDate = model.CheckOutDate;

                if (userId == null && login.HasValue)
                {
                    if (login.Value)
                    {
                        hostelId = model.LoginInput.Id;
                        checkInDate = model.LoginInput.CheckInDate;
                        checkOutDate = model.LoginInput.CheckOutDate;
                    }
                    else if (!login.Value)
                    {
                        hostelId = model.RegisterInput.Id;
                        checkInDate = model.RegisterInput.CheckInDate;
                        checkOutDate = model.RegisterInput.CheckOutDate;
                    }
                }

                var hostel = await GetHostel(hostelId.Value);

                if (hostel != null)
                {
                    string redirectUrl = $"/book-a-bed/{hostel.Name.UrlFriendly()}/{checkInDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}/{checkOutDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}";

                    if (userId != null)
                        redirectUrl += $"/{userId}";

                    return LocalRedirect(redirectUrl);
                }
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return LocalRedirect("/error");
        }

        public async Task<IActionResult> OnPostLoginAndBook(HeaderBannerBookNowPartialModel model)
        {
            try
            {
                string userId = Helpers.GetUserId(_httpContextAccessor);

                int? hostelId = model.LoginInput.Id;
                DateTime? checkInDate = model.LoginInput.CheckInDate;
                DateTime? checkOutDate = model.LoginInput.CheckOutDate;

                if (userId == null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.LoginInput.Email, model.LoginInput.Password, model.LoginInput.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(model.LoginInput.Email);

                        if (user != null)
                            userId = user.Id;
                    }
                }

                var hostel = await GetHostel(hostelId.Value);

                if (hostel != null)
                {
                    string redirectUrl = $"/book-a-bed/{hostel.Name.UrlFriendly()}/{checkInDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}/{checkOutDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}";

                    if (userId != null)
                        redirectUrl += $"/{userId}";

                    return LocalRedirect(redirectUrl);
                }

            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return LocalRedirect("/error");
        }

        public async Task<IActionResult> OnPostRegisterAndBook(HeaderBannerBookNowPartialModel model)
        {
            try
            {
                string userId = Helpers.GetUserId(_httpContextAccessor);

                int? hostelId = model.RegisterInput.Id;
                DateTime? checkInDate = model.RegisterInput.CheckInDate;
                DateTime? checkOutDate = model.RegisterInput.CheckOutDate;

                if (userId == null)
                {
                    var user = new ApplicationUser()
                    {
                        FirstName = model.RegisterInput.FirstName,
                        Surname = model.RegisterInput.Surname,
                        PhoneNumber = model.RegisterInput.PhoneNumber,
                        Gender = model.RegisterInput.Gender,
                        Nationality = model.RegisterInput.Nationality,
                        DateOfBirth = model.RegisterInput.DateOfBirth,
                        UserName = model.RegisterInput.Email,
                        Email = model.RegisterInput.Email,
                        SignUpDate = DateTime.Now
                    };

                    var result = await _userManager.CreateAsync(user, model.RegisterInput.Password);

                    if (result.Succeeded)
                    {
                        userId = await _userManager.GetUserIdAsync(user);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }

                var hostel = await GetHostel(hostelId.Value);

                if (hostel != null)
                {
                    string redirectUrl = $"/book-a-bed/{hostel.Name.UrlFriendly()}/{checkInDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}/{checkOutDate.Value.ToString("yyyy-MM-dd").UrlFriendly()}";

                    if (userId != null)
                        redirectUrl += $"/{userId}";

                    return LocalRedirect(redirectUrl);
                }
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return LocalRedirect("/error");
        }

        protected async Task<Core.Models.Page> OverridePageId(string pageTitle)
        {
            Core.Models.Page page = null;

            try
            {
                var pages = await _pageSectionsRepository.GetPages();

                if (pages.Any())
                {
                    page = pages.Where(p => p.PageTitle.UrlFriendly() == pageTitle).FirstOrDefault();

                    if (page != null)
                        _pageId = page.Id;
                }
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return page;
        }
    }
}

