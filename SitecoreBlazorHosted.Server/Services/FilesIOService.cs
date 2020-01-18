using Foundation.BlazorExtensions.Services;
using SitecoreBlazorHosted.Server.Providers;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SitecoreBlazorHosted.Server.Services
{
    public class FilesIOService : IRestService
    {
        private readonly IPathProvider _pathProvider;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FilesIOService(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;

            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<string> ExecuteRestMethod(string url)
        {
            Uri uri = new Uri(url);

            string physicalFilePath = _pathProvider.MapPath(uri.AbsolutePath.TrimStart(new char[] { '/' }));

            if (!System.IO.File.Exists(physicalFilePath))
                return string.Empty;    
                
            using StreamReader sr = new StreamReader(physicalFilePath);
            return await sr.ReadToEndAsync();

        }

        public Task<T> ExecuteRestMethod<T>(string url) where T : class
        {
            return ExecuteRestMethodWithJsonSerializerOptions<T>(url);
        }

        public async Task<T> ExecuteRestMethodWithJsonSerializerOptions<T>(string url, JsonSerializerOptions options = null)
        {
            string rawResultData = await ExecuteRestMethod(url);

            return JsonSerializer.Deserialize<T>(rawResultData, options ?? _jsonSerializerOptions);
        }
    }
}
