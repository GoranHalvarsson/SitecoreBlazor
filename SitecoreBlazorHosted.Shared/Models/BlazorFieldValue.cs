using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class BlazorFieldValue<T> where T : class
    {
        public T  Value { get; set; }

    }
}
