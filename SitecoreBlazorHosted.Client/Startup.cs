
using System;
using System.Linq;
using System.Net.Http;
using Feature.Navigation.Extensions;
using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SitecoreBlazorHosted.Client
{


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddForFoundationBlazorExtensions();
            services.AddForFeatureNavigation();

            // Server Side Blazor doesn't register HttpClient by default
            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<IUriHelper>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.GetBaseUri())
                    };
                });
            }

        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
