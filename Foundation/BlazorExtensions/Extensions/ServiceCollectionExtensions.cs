using Foundation.BlazorExtensions.Factories;
using Foundation.BlazorExtensions.Services;
using Microsoft.Extensions.DependencyInjection;
using SitecoreBlazorHosted.Shared;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static void AddStorage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<LocalStorage>();
            serviceCollection.AddScoped<SessionStorage>();
        }


        public static void AddForFoundationBlazorExtensions(this IServiceCollection serviceCollection)
        {
            AddStorage(serviceCollection);
            serviceCollection.AddScoped<SitecoreItemService>();
            serviceCollection.AddScoped<InteropService>();
            serviceCollection.AddScoped<BlazorContext>();
            serviceCollection.AddScoped<RouteService>();
            serviceCollection.AddScoped<LayoutService>();
            serviceCollection.AddScoped<ComponentFactory>();
            serviceCollection.AddScoped<LanguageService>();
            serviceCollection.AddScoped<BlazorStateMachine>();

        }

    }
}
