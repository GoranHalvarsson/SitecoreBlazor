
namespace SitecoreBlazorHosted.Shared.Models
{

    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBlazorItem
    {
        string DisplayName { get; set; }

        bool HasChildren { get; }
        string Language { get; set; }
        string Id { get; set; }

        string Name { get; set; }

        List<IBlazorItemField> Fields { get; set; }
        IBlazorItem Parent { get; set; }
        string Url { get; set; }
        IEnumerable<IBlazorItem> Children { get; set; }
    }
}
