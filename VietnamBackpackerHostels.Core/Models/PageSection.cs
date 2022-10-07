using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace VietnamBackpackerHostels.Core.Models
{
    public class PageSection
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; } = string.Empty;
        public IEnumerable<PageSectionContent> PageSectionContent { get; set; } = Enumerable.Empty<PageSectionContent>();

        public PageSection()
        {

        }

        public PageSection(int id, int pageId, IEnumerable<PageSectionContent> pageSectionContent, string title = null)
        {
            Id = id;
            PageId = pageId;
            Title = title;
            PageSectionContent = pageSectionContent;
        }
    }
}

