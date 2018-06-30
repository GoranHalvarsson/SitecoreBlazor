using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorSitecoreField<T> : IBlazorSitecoreField
    {
        public T Value { get; set; }

        public string Editable { get; set; }

        public string Type { get; set; }

        public string FieldName { get; set; }
    }
}
