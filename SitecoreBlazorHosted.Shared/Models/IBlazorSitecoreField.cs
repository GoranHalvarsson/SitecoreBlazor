using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public interface IBlazorSitecoreField
    {
        string FieldName { get; set; }
        string Type { get; set; }
    }
}
