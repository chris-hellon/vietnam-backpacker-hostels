using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace VietnamBackpackerHostels.UI.Pages.EmailTemplates
{
    public class ContactTemplateModel 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public ContactTemplateModel(string name, string email, string message)
        {
            Name = name;
            Email = email;
            Message = message;
        }
    }
}
