﻿using System;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Utils
{
    public static class Helpers
    {
        public static Dictionary<string, string> GetNationalities()
        {
            return new Dictionary<string, string>()
            {
                { "", "" },
                { "AF",  "Afghanistan" },
                                        { "AX", "Åland Islands" },
                                        { "AL", "Albania" },
                                        { "DZ", "Algeria" },
                                        { "AS", "American Samoa" },
                                        { "AD", "Andorra" },
                                        { "AO", "Angola" },
                                        { "AI", "Anguilla" },
                                        { "AQ", "Antarctica" },
                                        { "AG", "Antigua and Barbuda" },
                                        { "AR", "Argentina" },
                                        { "AM", "Armenia" },
                                        { "AW", "Aruba" },
                                        { "AU", "Australia" },
                                        { "AT", "Austria" },
                                        { "AZ", "Azerbaijan" },
                                        { "BS", "Bahamas" },
                                        { "BH", "Bahrain" },
                                        { "BD", "Bangladesh" },
                                        { "BB", "Barbados" },
                                        { "BY", "Belarus" },
                                        { "BE", "Belgium" },
                                        { "BZ", "Belize" },
                                        { "BJ", "Benin" },
                                        { "BM", "Bermuda" },
                                        { "BT", "Bhutan" },
                                        { "BO", "Bolivia" },
                                        { "BA", "Bosnia and Herzegovina" },
                                        { "BW", "Botswana" },
                                        { "BV", "Bouvet Island" },
                                        { "BR", "Brazil" },
                                        { "IO", "British Indian Ocean Territory" },
                                        { "VG", "British Virgin Islands" },
                                        { "BN", "Brunei" },
                                        { "BG", "Bulgaria" },
                                        { "BF", "Burkina Faso" },
                                        { "BI", "Burundi" },
                                        { "KH", "Cambodia" },
                                        { "CM", "Cameroon" },
                                        { "CA", "Canada" },
                                        { "CV", "Cape Verde" },
                                        { "BQ", "Caribbean Netherlands" },
                                        { "KY", "Cayman Islands" },
                                        { "CF", "Central African Republic" },
                                        { "TD", "Chad" },
                                        { "CL", "Chile" },
                                        { "CN", "China" },
                                        { "CX", "Christmas Island" },
                                        { "CC", "Cocos(Keeling) Islands" },
                                        { "CO", "Colombia" },
                                        { "KM", "Comoros" },
                                        { "CK", "Cook Islands" },
                                        { "CR", "Costa Rica" },
                                        { "CI", "Cote d\'Ivoire" },
                                        { "HR", "Croatia" },
                                        { "CU", "Cuba" },
                                        { "CW", "Curacao" },
                                        { "CY", "Cyprus" },
                                        { "CZ", "Czech Republic" },
                                        { "CD", "Democratic Republic of the Congo" },
                                        { "DK", "Denmark" },
                                        { "DJ", "Djibouti" },
                                        { "DM", "Dominica" },
                                        { "DO", "Dominican Republic" },
                                        { "EC", "Ecuador" },
                                        { "EG", "Egypt" },
                                        { "SV", "El Salvador" },
                                        { "GQ", "Equatorial Guinea" },
                                        { "ER", "Eritrea" },
                                        { "EE", "Estonia" },
                                        { "ET", "Ethiopia" },
                                        { "FK", "Falkland Islands (Islas Malvinas)" },
                                        { "FO", "Faroe Islands" },
                                        { "FJ", "Fiji" },
                                        { "FI", "Finland" },
                                        { "FR", "France" },
                                        { "GF", "French Guiana" },
                                        { "PF", "French Polynesia" },
                                        { "TF", "French Southern Territories" },
                                        { "GA", "Gabon" },
                                        { "GM", "Gambia" },
                                        { "GE", "Georgia" },
                                        { "DE", "Germany" },
                                        { "GH", "Ghana" },
                                        { "GI", "Gibraltar" },
                                        { "GR", "Greece" },
                                        { "GL", "Greenland" },
                                        { "GD", "Grenada" },
                                        { "GP", "Guadeloupe" },
                                        { "GU", "Guam" },
                                        { "GT", "Guatemala" },
                                        { "GG", "Guernsey" },
                                        { "GN", "Guinea" },
                                        { "GW", "Guinea - Bissau" },
                                        { "GY", "Guyana" },
                                        { "HT", "Haiti" },
                                        { "HM", "Heard & amp; McDonald Islands" },
                                        { "HN", "Honduras" },
                                        { "HK", "Hong Kong" },
                                        { "HU", "Hungary" },
                                        { "IS", "Iceland" },
                                        { "IN", "India" },
                                        { "ID", "Indonesia" },
                                        { "IR", "Iran" },
                                        { "IQ", "Iraq" },
                                        { "IE", "Ireland" },
                                        { "IM", "Isle of Man" },
                                        { "IL", "Israel" },
                                        { "IT", "Italy" },
                                        { "JM", "Jamaica" },
                                        { "JP", "Japan" },
                                        { "JE", "Jersey" },
                                        { "JO", "Jordan" },
                                        { "KZ", "Kazakhstan" },
                                        { "KE", "Kenya" },
                                        { "KI", "Kiribati" },
                                        { "XK", "Kosovo" },
                                        { "KW", "Kuwait" },
                                        { "KG", "Kyrgyzstan" },
                                        { "LA", "Laos" },
                                        { "LV", "Latvia" },
                                        { "LB", "Lebanon" },
                                        { "LS", "Lesotho" },
                                        { "LR", "Liberia" },
                                        { "LY", "Libya" },
                                        { "LI", "Liechtenstein" },
                                        { "LT", "Lithuania" },
                                        { "LU", "Luxembourg" },
                                        { "MO", "Macau SAR China" },
                                        { "MK", "Macedonia" },
                                        { "MG", "Madagascar" },
                                        { "MW", "Malawi" },
                                        { "MY", "Malaysia" },
                                        { "MV", "Maldives" },
                                        { "ML", "Mali" },
                                        { "MT", "Malta" },
                                        { "MH", "Marshall Islands" },
                                        { "MQ", "Martinique" },
                                        { "MR", "Mauritania" },
                                        { "MU", "Mauritius" },
                                        { "YT", "Mayotte" },
                                        { "MX", "Mexico" },
                                        { "FM", "Micronesia" },
                                        { "MD", "Moldova" },
                                        { "MC", "Monaco" },
                                        { "MN", "Mongolia" },
                                        { "ME", "Montenegro" },
                                        { "MS", "Montserrat" },
                                        { "MA", "Morocco" },
                                        { "MZ", "Mozambique" },
                                        { "MM", "Myanmar" },
                                        { "NA", "Namibia" },
                                        { "NR", "Nauru" },
                                        { "NP", "Nepal" },
                                        { "NL", "Netherlands" },
                                        { "AN", "Netherlands Antilles" },
                                        { "NC", "New Caledonia" },
                                        { "NZ", "New Zealand" },
                                        { "NI", "Nicaragua" },
                                        { "NE", "Niger" },
                                        { "NG", "Nigeria" },
                                        { "NU", "Niue" },
                                        { "NF", "Norfolk Island" },
                                        { "KP", "North Korea" },
                                        { "MP", "Northern Mariana Islands" },
                                        { "NO", "Norway" },
                                        { "OM", "Oman" },
                                        { "PK", "Pakistan" },
                                        { "PW", "Palau" },
                                        { "PS", "Palestinian Territory" },
                                        { "PA", "Panama" },
                                        { "PG", "Papua New Guinea" },
                                        { "PY", "Paraguay" },
                                        { "PE", "Peru" },
                                        { "PH", "Philippines" },
                                        { "PN", "Pitcairn Islands" },
                                        { "PL", "Poland" },
                                        { "PT", "Portugal" },
                                        { "PR", "Puerto Rico" },
                                        { "QA", "Qatar" },
                                        { "CG", "Republic of the Congo" },
                                        { "RE", "Reunion" },
                                        { "RO", "Romania" },
                                        { "RU", "Russia" },
                                        { "RW", "Rwanda" },
                                        { "KN", "Saint Kitts and Nevis" },
                                        { "LC", "Saint Lucia" },
                                        { "SX", "Saint Martin" },
                                        { "VC", "Saint Vincent and the Grenadines" },
                                        { "WS", "Samoa" },
                                        { "SM", "San Marino" },
                                        { "ST", "Sao Tome and Principe" },
                                        { "SA", "Saudi Arabia" },
                                        { "SN", "Senegal" },
                                        { "RS", "Serbia" },
                                        { "SC", "Seychelles" },
                                        { "SL", "Sierra Leone" },
                                        { "SG", "Singapore" },
                                        { "MF", "Sint Maarten" },
                                        { "SK", "Slovakia" },
                                        { "SI", "Slovenia" },
                                        { "SB", "Solomon Islands" },
                                        { "SO", "Somalia" },
                                        { "ZA", "South Africa" },
                                        { "GS", "South Georgia &amp; South Sandwich Islands" },
                                        { "KR", "South Korea" },
                                        { "SS", "South Sudan" },
                                        { "ES", "Spain" },
                                        { "LK", "Sri Lanka" },
                                        { "BL", "St.Barthélemy" },
                                        { "SH", "St.Helena" },
                                        { "PM", "St.Pierre & amp; Miquelon" },
                                        { "SD", "Sudan" },
                                        { "SR", "Suriname" },
                                        { "SJ", "Svalbard and Jan Mayen" },
                                        { "SZ", "Swaziland" },
                                        { "SE", "Sweden" },
                                        { "CH", "Switzerland" },
                                        { "SY", "Syria" },
                                        { "TW", "Taiwan" },
                                        { "TJ", "Tajikistan" },
                                        { "TZ", "Tanzania" },
                                        { "TH", "Thailand" },
                                        { "TL", "Timor - Leste" },
                                        { "TG", "Togo" },
                                        { "TK", "Tokelau" },
                                        { "TO", "Tonga" },
                                        { "TT", "Trinidad and Tobago" },
                                        { "TN", "Tunisia" },
                                        { "TR", "Turkey" },
                                        { "TM", "Turkmenistan" },
                                        { "TC", "Turks & amp; Caicos Islands" },
                                        { "TV", "Tuvalu" },
                                        { "UM", "U.S.Outlying Islands" },
                                        { "VI", "U.S.Virgin Islands" },
                                        { "UG", "Uganda" },
                                        { "UA", "Ukraine" },
                                        { "AE", "United Arab Emirates" },
                                        { "GB", "United Kingdom" },
                                        { "US", "United States of America" },
                                        { "UY", "Uruguay" },
                                        { "UZ", "Uzbekistan" },
                                        { "VU", "Vanuatu" },
                                        { "VA", "Vatican City" },
                                        { "VE", "Venezuela" },
                                        { "VN", "Vietnam" },
                                        { "WF", "Wallis and Futuna" },
                                        { "EH", "Western Sahara" },
                                        { "YE", "Yemen" },
                                        { "ZM", "Zambia" },
                                        { "ZW", "Zimbabwe" }

            };
        }
    }
}

