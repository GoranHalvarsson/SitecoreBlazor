using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitecoreBlazorHosted.Shared.Models
{
    public class ImageSizeParameters
    {
        /// <summary>
        /// Fixed width of the image
        /// </summary>
        public int? W { get; set; }

        /// <summary>
        /// Fixed height of the image
        /// </summary>
        public int? H { get; set; }

        /// <summary>
        /// Max width of the image
        /// </summary>
        public int? Mw { get; set; }

        /// <summary>
        /// Max height of the image
        /// </summary>
        public int? Mh { get; set; }

        /// <summary>
        /// Ignore aspect ratio
        /// </summary>
        public bool? Iar { get; set; }

        /// <summary>
        /// Allow stretch
        /// </summary>
        public bool? As { get; set; }

        /// <summary>
        /// Image scale. Defaults to 1.0
        /// </summary>
        public int? Sc { get; set; }

        public ImageSizeParameters()
        {
        }

        public ImageSizeParameters(ImageSizeParameters imageSizeParameters)
        {
            W = imageSizeParameters?.W;
            H = imageSizeParameters?.H;
            Mw = imageSizeParameters?.Mw;
            Mh = imageSizeParameters?.Mh;
            Iar = imageSizeParameters?.Iar;
            As = imageSizeParameters?.As;
            Sc = imageSizeParameters?.Sc;
        }

        public override string ToString()
        {
            var parameters = new Dictionary<string, string>();
            foreach (var property in GetType().GetProperties())
            {
                var value = property.GetValue(this);
                if (value == null) continue;

                var key = property.Name.ToLower();
                parameters.Add(key, value.ToString().ToLower());
            }
            return string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
