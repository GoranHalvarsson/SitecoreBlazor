using Microsoft.AspNetCore.Http;

namespace SitecoreBlazorHosted.Shared
{
    public class HttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }   

        public HttpContext Context => _httpContextAccessor.HttpContext;
    }
}
