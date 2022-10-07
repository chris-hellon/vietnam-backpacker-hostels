using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Core.Models;

namespace VietnamBackpackerHostels.Infrastructure.Repositories
{
    public class ImagesRepository : BaseRepository, IImagesRepository
    {
        private IPageSectionsRepository _pageSectionsRepository;

        public ImagesRepository(IDapperContext dapperContext, IPageSectionsRepository pageSectionsRepository) : base(dapperContext)
        {
            _pageSectionsRepository = pageSectionsRepository;
        }

        public async Task<IEnumerable<Image>> GetBannerImages(int pageId)
        {
            return await Task.Run(() =>
            {
                List<Image> bannerImages = new List<Image>();

                switch (pageId)
                {
                    case 1: // home page
                        return new List<Image>() {
                        new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/main/home-5.jpg",
                                Subtitle = "<h2 class=\"mb-0 mt-n3 fs-4\">Backpacker Hostels</h2><small class=\"text-end mt-n2 me-1 mb-4 header-font d-block\">est. 2004</small> <h4 class=\"mb-4\">#MORETHANJUSTABED</h4>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0 fs-1\">Vietnam</h1>"
                            },
                        new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/main/home-8.jpg",
                                Subtitle = "<h2 class=\"mb-0 mt-n3 fs-4\">Backpacker Hostels</h2><small class=\"text-end mt-n2 me-1 mb-4 header-font d-block\">est. 2004</small> <h4 class=\"mb-4\">#MORETHANJUSTABED</h4>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0 fs-1\">Vietnam</h1>"
                            },
                        new Image()
                        {
                            ImageUrl = "https://vietnambackpackerhostels.azureedge.net/hostels/hoi-an-banner-3.jpg",
                                Subtitle = "<h2 class=\"mb-0 mt-n3 fs-4\">Backpacker Hostels</h2><small class=\"text-end mt-n2 me-1 mb-4 header-font d-block\">est. 2004</small> <h4 class=\"mb-4\">#MORETHANJUSTABED</h4>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0 fs-1\">Vietnam</h1>"
                        },
                        new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/main/home-3.jpg",
                                Subtitle = "<h2 class=\"mb-0 mt-n3 fs-4\">Backpacker Hostels</h2><small class=\"text-end mt-n2 me-1 mb-4 header-font d-block\">est. 2004</small> <h4 class=\"mb-4\">#MORETHANJUSTABED</h4>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0 fs-1\">Vietnam</h1>"
                        },
                        new Image()
                        {
                            ImageUrl = "https://vietnambackpackerhostels.azureedge.net/hostels/hue-banner-2.jpg",
                                Subtitle = "<h2 class=\"mb-0 mt-n3 fs-4\">Backpacker Hostels</h2><small class=\"text-end mt-n2 me-1 mb-4 header-font d-block\">est. 2004</small> <h4 class=\"mb-4\">#MORETHANJUSTABED</h4>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0 fs-1\">Vietnam</h1>"
                        }
                    };
                    case 2:
                        return new List<Image>() { new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/main/home-9.jpg",
                                Subtitle = "<h5 class=\"mb-0\">No matter where you are in Vietnam,</h5><h5 class=\"mb-0\">VBH has got you covered...</h5>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Sleep & Eat</h1>"
                            },
                    new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/hostels/hoi-an-banner-1.jpg",
                                Subtitle = "<h5 class=\"mb-0\">No matter where you are in Vietnam,</h5><h5 class=\"mb-0\">VBH has got you covered...</h5>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Sleep & Eat</h1>"
                            },
                    new Image() {
                                ImageUrl = "https://vietnambackpackerhostels.azureedge.net/hostels/hue-banner-1.jpg",
                                Subtitle = "<h5 class=\"mb-0\">No matter where you are in Vietnam,</h5><h5 class=\"mb-0\">VBH has got you covered...</h5>",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Sleep & Eat</h1>"
                            }};
                    case 3:
                        bannerImages = new List<Image>();

                        for (int i = 0; i < 5; i++)
                        {
                            bannerImages.Add(new Image()
                            {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/trips/hai-van-pass-{i + 1}.jpg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Explore</h1>",
                                Subtitle = "<h5 class=\"mb-0\">It's rude to keep your passport waiting...</h5>",
                            });
                        }

                        return bannerImages;
                    case 15: // trips & travel
                        return new List<Image>()
                        {
                            new Image()
                             {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/main/home-2.jpg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Trips & Travel</h1>",
                                Subtitle = "<h5 class=\"mb-0\">Where to next?</h5>",
                             },
                             new Image()
                             {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/main/trips-and-travel-1.jpg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Trips & Travel</h1>",
                                Subtitle = "<h5 class=\"mb-0\">Where to next?</h5>",
                             },
                             new Image()
                             {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/hostels/hoi-an-banner-3.jpg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Trips & Travel</h1>",
                                Subtitle = "<h5 class=\"mb-0\">Where to next?</h5>",
                             },
                             new Image()
                             {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/main/trips-and-travel-2.jpg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Trips & Travel</h1>",
                                Subtitle = "<h5 class=\"mb-0\">Where to next?</h5>",
                             },
                             new Image()
                             {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/main/home-1.jpeg",
                                Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Trips & Travel</h1>",
                                Subtitle = "<h5 class=\"mb-0\">Where to next?</h5>",
                             }
                        };
                    case 29:
                    case 31: // trip category
                             //var page = await _pageSectionsRepository.GetPage(pageId);

                        //if (page != null)
                        //{
                        //    bannerImages = new List<Image>();

                        //    for (int i = 0; i < 5; i++)
                        //    {
                        //        bannerImages.Add(new Image()
                        //        {
                        //            ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/trips/hai-van-pass-{i + 1}.jpg",
                        //            Subtitle = $"<h5 class=\"mb-0\">{page.PageTitle}</h5>",
                        //            Title = "<h1 class=\"display-1 lh-1 m-0 p-0\">Explore</h1>"
                        //        });
                        //    }

                        //    return bannerImages;
                        //}

                        bannerImages = new List<Image>();

                        for (int i = 0; i < 5; i++)
                        {
                            bannerImages.Add(new Image()
                            {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/trips/hai-van-pass-{i + 1}.jpg",
                            });
                        }

                        return bannerImages;
                    case 17: // hue

                        bannerImages = new List<Image>();
                        for (int i = 0; i < 4; i++)
                        {
                            bannerImages.Add(new Image()
                            {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/hostels/hue-banner-{i + 1}.jpg",
                                //Subtitle = $"<h5 class=\"mb-0\">Hue</h5>",
                                //Title = $"<img class='img-fluid m-3 col-6 col-md-4 col-lg-3' src='{SelectedHostel.LogoImageUrl}' /><h1 class=\"display-1 lh-1 m-0 p-0\">{hostelLocation.Name}</h1>"
                            });
                        }
                        return bannerImages;
                    case 18: // hoi an

                        bannerImages = new List<Image>();
                        for (int i = 0; i < 6; i++)
                        {
                            bannerImages.Add(new Image()
                            {
                                ImageUrl = $"https://vietnambackpackerhostels.azureedge.net/hostels/hoi-an-banner-{i + 1}.jpg",
                                //Subtitle = $"<h5 class=\"mb-0\">Hue</h5>",
                                //Title = $"<img class='img-fluid m-3 col-6 col-md-4 col-lg-3' src='{SelectedHostel.LogoImageUrl}' /><h1 class=\"display-1 lh-1 m-0 p-0\">{hostelLocation.Name}</h1>"
                            });
                        }
                        return bannerImages;
                }

                return Enumerable.Empty<Image>();
            });


            

            //return await DapperConnection.ExecuteGetStoredProcedureList<Image>("GetImages", new
            //{
            //    PageId = pageId
            //});
        }

        public async Task<IEnumerable<Image>> GetTripDaysImages(int tripId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Image>("GetImages", new
            {
                TripId = tripId
            });
        }

        public async Task<Image> CreateImage(string fileName, string imageUrl, string createId, string title = null, string subTitle = null, string url1 = null, string url2 = null, int? hostelId = null, int? tripId = null, int? tripDayId = null, int? pageId = null)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle<Image>("CreateImage", new
            {
                Name = fileName,
                ImageUrl = imageUrl,
                CreateId = createId,
                Title = title,
                SubTitle = subTitle,
                Url1 = url1,
                Url2 = url2,
                HostelId = hostelId,
                TripId = tripId,
                TripDayId = tripDayId,
                PageId = pageId
            });
        }
    }
}

