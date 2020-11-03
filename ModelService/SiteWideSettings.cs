using System;
namespace ModelService
{
    public class SiteWideSettings
    {
        public string WebsiteName { get; set; }
        public string WebsiteTitle { get; set; }
        public string WebsiteDescription { get; set; }
        public string WebsiteKeywords { get; set; }
        public string WebsiteFooter { get; set; }
        public string WebsiteAuthor { get; set; }
        public string WebsiteStatus { get; set; }
        public bool WebsiteRegistration { get; set; }
        public bool AgeVerification { get; set; }
    }
}
