using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
  public class RestService
  {
    private readonly HttpClient _httpClient;

    public RestService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }



    public async Task<T> ExecuteRestMethod<T>(string url) where T : class
    {
      return await _httpClient.GetJsonAsync<T>(url);
    }


  }
}
