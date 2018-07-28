namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{

    using System.Collections.Generic;
    using System.Linq;

    public class ScItem : ISitecoreItem
    {
        public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

        public string Url { get; set; }
        public string Name { get; set; }

        public string Language { get; set; }
        public string DisplayName { get;  set; }
        public bool HasChildren
        {
            get
            {
                return Children != null && Children.Count() > 0;
            }
        }
        public string Id { get;  set; }
        public string Path { get;  set; }
        public ISitecoreItem Parent { get; set; }
        public IEnumerable<ISitecoreItem> Children { get; set; }
        public List<IBlazorSitecoreField> Fields { get; set; }
    }
}
