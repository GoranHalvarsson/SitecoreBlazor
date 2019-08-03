using System.Collections.Generic;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorRoute
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string ItemLanguage { get; set; }

        public Dictionary<string, BlazorRouteField> Fields { get; set; }

        public Dictionary<string,IList<Placeholder>> Placeholders { get; set; }
       

    }


}
