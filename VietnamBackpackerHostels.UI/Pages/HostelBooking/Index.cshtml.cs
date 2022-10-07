using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Core.Models;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.UI.Pages.HostelBooking
{
    public class IndexModel : BasePageModel
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IEmailService _emailService;
        private ApplicationUser _applicationUser { get; set; } = null;

        public string HostelName { get; set; } = string.Empty;
        public int HostelId { get; set; }
        public string UserId { get; set; }
        public string IframeUrl { get; set; }

        public IndexModel(IEmailService emailService, IBookingsRepository bookingsRepository, IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {
            _bookingsRepository = bookingsRepository;
            _emailService = emailService;
        }

        public async Task<IActionResult> OnGet(string hostelName, string checkInDate = null, string checkOutDate = null, string userId = null)
        {
            await OnGetPageContentAsync();

            try
            {
                BannerImages = Enumerable.Empty<Image>();

                if (userId != null)
                    _applicationUser = await _userManager.FindByIdAsync(userId);

                UserId = userId;

                var hostel = Hostels.Where(h => h.Name.UrlFriendly() == hostelName).FirstOrDefault();

                if (hostel != null)
                {
                    HostelName = hostel.Name;
                    HostelId = hostel.Id;
                    IframeUrl = $"https://hotels.cloudbeds.com/reservation/{hostel.CloudBedsKey}";

                    if (_applicationUser != null)
                    {
                        IframeUrl += "?";

                        if (!string.IsNullOrEmpty(_applicationUser.FirstName))
                            IframeUrl += $"firstName={_applicationUser.FirstName}";

                        if (!string.IsNullOrEmpty(_applicationUser.Surname))
                            IframeUrl += $"&lastName={_applicationUser.Surname}";

                        if (!string.IsNullOrEmpty(_applicationUser.Email))
                            IframeUrl += $"&email={_applicationUser.Email}";

                        if (!string.IsNullOrEmpty(_applicationUser.Nationality))
                            IframeUrl += $"&country={_applicationUser.Nationality}";

                        if (!string.IsNullOrEmpty(_applicationUser.PhoneNumber))
                            IframeUrl += $"&phone={_applicationUser.PhoneNumber}";
                    }

                    if (!string.IsNullOrEmpty(checkInDate) || !string.IsNullOrEmpty(checkOutDate))
                    {
                        IframeUrl += "#";

                        if (!string.IsNullOrEmpty(checkInDate))
                            IframeUrl += $"&checkin={checkInDate}";

                        if (!string.IsNullOrEmpty(checkOutDate))
                            IframeUrl += $"&checkout={checkOutDate}";
                    }

                    return Page();
                }

                return LocalRedirect("/error");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> OnPostCreateHostelBooking([FromBody] Core.Models.HostelReservation hostelBooking)
        {
            try
            {
                int hostelReservationId = await _bookingsRepository.CreateHostelReservation(hostelBooking.CheckInDate, hostelBooking.CheckOutDate, hostelBooking.RoomQuantity, hostelBooking.Amount, hostelBooking.CurrencyCode, hostelBooking.HostelId, hostelBooking.UserId, hostelBooking.CloudbedsReservationId, hostelBooking.CloudbedsPropertyId);

                if (hostelBooking.Rooms.Any())
                {
                    List<Task> roomsTasks = new List<Task>();
                    foreach (var room in hostelBooking.Rooms)
                    {
                        roomsTasks.Add(_bookingsRepository.CreateHostelReservationRoom(hostelReservationId, room.RoomName, room.Amount, room.Nights, room.CheckInDate, room.CheckOutDate, room.GuestFirstName, room.GuestLastName, room.CloudbedsGuestId));
                    }

                    await Task.WhenAll(roomsTasks);
                }

                return new JsonResult(hostelReservationId);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e, hostelBooking);
            }

            return new JsonResult("fail");
        }
    }
}

