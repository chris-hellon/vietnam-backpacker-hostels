using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Interfaces.Services;
using VietnamBackpackerHostels.UI.Models.Shared;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;

namespace VietnamBackpackerHostels.UI.Pages.Hostel
{
    public class IndexModel : BasePageModel
    {
        public VietnamBackpackerHostels.Core.Models.Hostel SelectedHostel { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, IErrorLoggerService errorLoggerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostelsRepository hostelsRepository, ITripsRepository tripsRepository, IImagesRepository imagesRepository, IPageSectionsRepository pageSectionsRepository) : base(httpContextAccessor, userManager, signInManager, hostelsRepository, tripsRepository, imagesRepository, pageSectionsRepository, errorLoggerService)
        {

        }

        public async Task<IActionResult> OnGet(string category)
        {
            var page = await OverridePageId(category);

            await OnGetPageContentAsync();

            try
            {
                var hostelLocation = HostelLocations.Where(hl => hl.Name.UrlFriendly() == category).FirstOrDefault();
                if (hostelLocation != null)
                {
                    SelectedHostel = hostelLocation.Hostels.FirstOrDefault();
                    if (SelectedHostel != null)
                    {
                        SelectedHostel.Location = hostelLocation;

                        var hostelRooms = _hostelsRepository.GetHostelRooms(SelectedHostel.Id);
                        var localTrips = _hostelsRepository.GetLocalTrips(SelectedHostel.Id);

                        await Task.WhenAll(hostelRooms, localTrips);

                        SelectedHostel.Rooms = hostelRooms.Result;
                        SelectedHostel.LocalTrips = localTrips.Result;
                        SelectedHostel.BannerImages = BannerImages;

                        if (SelectedHostel.Id == 3)
                        {
                            var slideshowImages = new List<string>();
                            for (int i = 0; i < 4; i++)
                            {
                                slideshowImages.Add($"https://vietnambackpackerhostels.azureedge.net/hostels/hue-slideshow-{i + 1}.jpg");
                            }

                            SelectedHostel.SlideshowImages = slideshowImages;
                        }
                        else if (SelectedHostel.Id == 9)
                        {
                            var slideshowImages = new List<string>();
                            for (int i = 0; i < 4; i++)
                            {
                                slideshowImages.Add($"https://vietnambackpackerhostels.azureedge.net/hostels/hoi-an-slideshow-{i + 1}.jpg");
                            }

                            SelectedHostel.SlideshowImages = slideshowImages;
                        }

                        category = SelectedHostel.Name.Split('-')[1].TrimStart();
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
    }


}

