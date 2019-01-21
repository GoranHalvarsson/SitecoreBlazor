using Foundation.BlazorExtensions.Factories;
using Foundation.BlazorExtensions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static void AddStorage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LocalStorage>();
            serviceCollection.AddSingleton<SessionStorage>();
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
            serviceCollection.AddScoped<RestService>();
            serviceCollection.AddScoped<LanguageService>();

        }

    }
}
