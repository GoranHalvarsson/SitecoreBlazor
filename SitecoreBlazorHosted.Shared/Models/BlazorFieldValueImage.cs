using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
 
    public class BlazorFieldValueImage : IBlazorFieldValue
    {
        public string Src { get; set; }

        public string Alt { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

    }
}
