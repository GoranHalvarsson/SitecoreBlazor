using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorRouteField
    {
        public Object Value { get; set; }

        public List<BlazorFieldComplex> Values { get; set; }

        public string Editable { get; set; }

        public string Type { get; set; }
    }
}
