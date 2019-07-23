using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public interface IRestService
    {
        Task<string> ExecuteRestMethod(string url);
        Task<T> ExecuteRestMethod<T>(string url) where T : class;
        Task<T> ExecuteRestMethodWithJsonSerializerOptions<T>(string url, JsonSerializerOptions options = null);
    }
}