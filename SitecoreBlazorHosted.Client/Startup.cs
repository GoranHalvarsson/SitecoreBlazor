using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SitecoreBlazorHosted.Client
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestService, RestService>();
            services.AddForFoundationBlazorExtensions();
            services.AddForFeatureNavigation();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<Project.BlazorSite.App>("app");
        }
    }
}
