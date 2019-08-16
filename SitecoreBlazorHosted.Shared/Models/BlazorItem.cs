namespace SitecoreBlazorHosted.Shared.Models
{

    using System.Collections.Generic;
    using System.Linq;

    public class BlazorItem : IBlazorItem
    {
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
        public IBlazorItem Parent { get; set; }
        public IEnumerable<IBlazorItem> Children { get; set; }
        public List<IBlazorItemField> Fields { get; set; }
    }
}
