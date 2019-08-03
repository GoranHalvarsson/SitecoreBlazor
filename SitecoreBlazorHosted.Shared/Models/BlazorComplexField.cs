using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorFieldComplex
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<string, BlazorRouteField> Fields { get; set; }
    }
}
