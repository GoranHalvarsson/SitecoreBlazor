using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;

namespace SitecoreBlazorHosted.Electron
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO! Should FileService be in the Electron project?
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
