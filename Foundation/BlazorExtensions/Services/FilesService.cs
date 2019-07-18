using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json.Serialization;
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

            return await Task<string>.Run(() =>
            {
                if (!System.IO.File.Exists(url))
                {
                    return "";
                }
                else
                {
                    return System.IO.File.ReadAllText(url);
                }

            });
        }

        public Task<T> ExecuteRestMethod<T>(string url) where T : class
        {
            return ExecuteRestMethodWithJsonSerializerOptions<T>(url);
        }

        public async Task<T> ExecuteRestMethodWithJsonSerializerOptions<T>(string url, JsonSerializerOptions options = null)
        {
            string rawResultData = await ExecuteRestMethod(url);

            return JsonSerializer.Parse<T>(rawResultData, options == null ? _jsonSerializerOptions : options);
        }

    }
}
