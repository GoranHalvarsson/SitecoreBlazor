using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;


namespace SitecoreBlazorHosted.Client
{


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>((s) => new HttpClient());
            services.AddForFoundationBlazorExtensions();
            services.AddForFeatureNavigation();

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<Project.BlazorSite.App>("app");
        }
    }
}
