namespace SitecoreBlazorHosted.Client.RouterExtended
{
    internal class RouteTemplate
    {
        public static readonly char[] Separators = new[] { '/' };

        public RouteTemplate(string TemplateText, TemplateSegment[] segments)
        {
            this.TemplateText = TemplateText;
            Segments = segments;
        }

        public string TemplateText { get; }

        public TemplateSegment[] Segments { get; }
    }
}
