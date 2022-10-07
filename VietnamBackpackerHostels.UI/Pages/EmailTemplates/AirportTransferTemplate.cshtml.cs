using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VietnamBackpackerHostels.UI.Pages.EmailTemplates
{
    public class AirportTransferTemplateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string Destination { get; set; }
        public string TypeOfTransport { get; set; }
        public string NumberOfPax { get; set; }
        public string FlightDetails { get; set; }

        public AirportTransferTemplateModel(string name, string email, DateTime dateOfArrival, string destination, string typeOfTransport, string numberOfPax, string flightDetails)
        {
            Name = name;
            Email = email;
            DateOfArrival = dateOfArrival;
            Destination = destination;
            TypeOfTransport = typeOfTransport;
            NumberOfPax = numberOfPax;
            FlightDetails = flightDetails;
        }
    }
}
