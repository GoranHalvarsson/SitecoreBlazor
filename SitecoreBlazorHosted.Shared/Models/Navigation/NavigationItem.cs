
namespace SitecoreBlazorHosted.Shared.Models.Navigation
{

    using SitecoreBlazorHosted.Shared.Models.Sitecore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NavigationItem
    {
        public ISitecoreItem Item  { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public List<NavigationItem> Children { get; set; }
        public string Target { get; set; }
        public string Class { get; set; }
        public bool ShowChildren { get; set; }
    }
}
