
using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SitecoreBlazorHosted.Client
{


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddForFoundationBlazorExtensions();
            services.AddForFeatureNavigation();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
