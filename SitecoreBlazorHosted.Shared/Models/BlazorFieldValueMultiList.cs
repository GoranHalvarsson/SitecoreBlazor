using System.Collections.Generic;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorFieldValueMultiList : IBlazorFieldValue
    {
        public List<BlazorFieldValueMultiListItem> Values { get; set; }
    }

    public class BlazorFieldValueMultiListItem
    {
        public string Id { get; set; }
        public string Url { get; set; }

        public Dictionary<string, BlazorField> Fields { get; set; }
        public List<IBlazorSitecoreField> SitecoreFields { get; set; }

    }


}
