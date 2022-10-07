using System;
using System.Collections;

namespace VietnamBackpackerHostels.Core.Models
{
    public class PageSectionContent
    {
        public int Id { get; set; }
        public int PageSectionId { get; set; }
        public string PageSectionTitle { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string NavigatePage { get; set; } = null;
        public string NavigatePageCategory { get; set; } = null;
        public string Category { get; set; } = null;
        public string TripName { get; set; } = null;
        public string IconClass { get; set; } = null;
        public string Location { get; set; } = null;
        public string CssClass { get; set; } = string.Empty;
        public double AnimationDelay { get; set; } = 400;

        public PageSectionContent()
        {

        }

        public PageSectionContent(int id, int pageSectionId, string imageUrl, string title, string body, string navigatePage = null, string navigatePageCategory = null, string category = null, string iconClass = null, string tripName = null, string location = null)
        {
            Id = id;
            PageSectionId = pageSectionId;
            ImageUrl = imageUrl;
            Title = title;
            Body = body;
            NavigatePage = navigatePage;
            NavigatePageCategory = navigatePageCategory;
            Category = category;
            IconClass = iconClass;
            TripName = tripName;
            Location = location;
        }
    }
}

