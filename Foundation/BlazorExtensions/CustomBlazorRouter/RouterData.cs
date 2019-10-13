using System.Collections.Generic;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
    /// <summary>
    /// Class for custom routes  
    /// </summary>
    public class RouterData
    {
        /// <summary>
        /// Route path
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Page assembly - Project.BlazorSite.Components.Shared.MasterBlaster, Project.BlazorSite
        /// </summary>
        public string? Page { get; set; }

        /// <summary>
        /// Children to the page
        /// </summary>
        public List<RouterData>? Children { get; set; }
    }

    /// <summary>
    /// Start root/page containing a list of pages
    /// </summary>
    public class RouterDataRoot
    {
        public List<RouterData>? Routes { get; set; }
    }


}
