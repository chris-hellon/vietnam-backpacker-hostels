using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VietnamBackpackerHostels.UI.Pages.Shared
{
    public class HeaderBannerBookNowPartialModel
    {
        public IEnumerable<Core.Models.Hostel> Hostels { get; set; } = Enumerable.Empty<Core.Models.Hostel>();

        public bool HostelPage { get; set; }

        [Required(ErrorMessage = "Please choose a check in date")]
        public DateTime? CheckInDate { get; set; }

        [Required(ErrorMessage = "Please choose a check out date")]
        public DateTime? CheckOutDate { get; set; }

        [Required(ErrorMessage = "Please choose a hostel", AllowEmptyStrings = false)]
        public int? Id { get; set; } = null;

        [BindProperty]
        public LoginModel LoginInput { get; set; }

        public class LoginModel : Areas.Identity.Pages.Account.LoginModel.InputModel
        {
            [Required(ErrorMessage = "Please choose a check in date")]
            public DateTime? CheckInDate { get; set; }

            [Required(ErrorMessage = "Please choose a check out date")]
            public DateTime? CheckOutDate { get; set; }

            [Required(ErrorMessage = "Please choose a hostel")]
            public int? Id { get; set; } = null;
        }

        [BindProperty]
        public RegisterModel RegisterInput { get; set; }

        public class RegisterModel : Areas.Identity.Pages.Account.RegisterModel.InputModel
        {
            [Required(ErrorMessage = "Please choose a check in date")]
            public DateTime? CheckInDate { get; set; }

            [Required(ErrorMessage = "Please choose a check out date")]
            public DateTime? CheckOutDate { get; set; }

            [Required(ErrorMessage = "Please choose a hostel")]
            public int? Id { get; set; } = null;
        }

        public HeaderBannerBookNowPartialModel()
        {
             
        }

        public HeaderBannerBookNowPartialModel(IEnumerable<Core.Models.Hostel> hostels, bool hostelPage = false)
        {
            Hostels = hostels;
            HostelPage = hostelPage;
        }
    }
}