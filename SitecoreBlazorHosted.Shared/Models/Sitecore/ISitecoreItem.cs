
namespace SitecoreBlazorHosted.Shared.Models.Sitecore
{

    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISitecoreItem
    {
        string DisplayName { get; set; }

        bool HasChildren { get; }
        string Language { get; set; }
        string Id { get; set; }

        string Name { get; set; }

        List<IBlazorSitecoreField> Fields { get; set; }
        ISitecoreItem Parent { get; set; }
        string Url { get; set; }
        IEnumerable<ISitecoreItem> Children { get; set; }
    }
}
