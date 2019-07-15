using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;
using SitecoreBlazorHosted.Shared;
using System.Net.Http;

namespace SitecoreBlazorHosted.Electron
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>((s) => new HttpClient());
            services.AddScoped<IRestService, FilesService>();
            services.AddForFoundationBlazorExtensions();
            services.AddForFeatureNavigation();
            

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
