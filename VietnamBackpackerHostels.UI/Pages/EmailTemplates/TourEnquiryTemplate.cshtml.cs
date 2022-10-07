using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.EmailTemplates
{
    public class TourEnquiryTemplateModel
    {
        public string Name { get; set; }
        public string Tour { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfPeople { get; set; }
        public string AdditionalInformation { get; set; }

        public TourEnquiryTemplateModel(string name, string tour, string email, DateTime date, int numberOfPeople, string additionalInformation = null)
        {
            Name = name;
            Tour = tour;
            Email = email;
            Date = date;
            NumberOfPeople = numberOfPeople;
            AdditionalInformation = additionalInformation;
        }
    }
}
