using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SitecoreBlazorHosted.Server.Providers
{
    public class PathProvider : IPathProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PathProvider(IWebHostEnvironment environment)
        {
            _webHostEnvironment = environment;
        }

        public string MapPath(string path)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            return filePath;
        }
    }
}
