using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class Route
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string ItemLanguage { get; set; }

        public Dictionary<string, BlazorField> Fields { get; set; }

        public Dictionary<string,IList<Placeholder>> Placeholders { get; set; }
       

    }


}
