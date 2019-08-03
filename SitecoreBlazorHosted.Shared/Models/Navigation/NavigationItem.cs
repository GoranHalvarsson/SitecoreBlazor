
namespace SitecoreBlazorHosted.Shared.Models.Navigation
{

    using SitecoreBlazorHosted.Shared.Models;
    using System.Collections.Generic;

    public class NavigationItem
    {
        public IBlazorItem Item  { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public List<NavigationItem> Children { get; set; }
        public string Target { get; set; }
        public string Class { get; set; }
        public bool ShowChildren { get; set; }
    }
}
