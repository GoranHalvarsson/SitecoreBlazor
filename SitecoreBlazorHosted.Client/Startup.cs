using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.BlazorSite;


namespace SitecoreBlazorHosted.Client
{


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<HttpClient>((s) => new HttpClient());
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
