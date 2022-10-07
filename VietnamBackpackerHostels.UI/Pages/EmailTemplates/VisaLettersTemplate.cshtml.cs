using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VietnamBackpackerHostels.Core.Utils;

namespace VietnamBackpackerHostels.UI.Pages.EmailTemplates
{
    public class VisaLettersTemplateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfArrival { get; set; }

        public VisaLettersTemplateModel(string name, string email, string nationality, DateTime dateOfArrival)
        {
            Name = name;
            Email = email;
            Nationality = nationality;
            DateOfArrival = dateOfArrival;

            var nationalities = Helpers.GetNationalities();
            if (nationalities.Any())
                Nationality = nationalities[Nationality];
        }
    }
}
