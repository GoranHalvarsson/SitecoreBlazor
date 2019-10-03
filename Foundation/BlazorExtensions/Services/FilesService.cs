using Foundation.BlazorExtensions.Extensions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class FilesService : IRestService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        
        public FilesService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<string> ExecuteRestMethod(string url)
        {

            url = url.RemoveFilePrefix();

            return await Task.Run(() => !System.IO.File.Exists(url) ? "" : System.IO.File.ReadAllText(url));
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
