using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class RouteService
    {
        private readonly IRestService _restService;
        private readonly NavigationManager _navigationManager;
        private readonly BlazorItemsService _blazorItemsService;
        private readonly BlazorStateMachine _blazorStateMachine;

        public RouteService(IRestService restService, NavigationManager navigationManager, BlazorItemsService blazorItemsService, BlazorStateMachine blazorStateMachine)
        {
            _restService = restService;
            _navigationManager = navigationManager;
            _blazorItemsService = blazorItemsService;
            _blazorStateMachine = blazorStateMachine;
        }

        
        public string BuildRouteApiUrl(string language, bool? hasRouteError)
        {
            string baseUrl = $"{_navigationManager.BaseUri}/data/routes";

            string relativeUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

            //Remove hash
            if(relativeUrl.IndexOf("#", StringComparison.Ordinal) > 0)
                relativeUrl = relativeUrl.Substring(0, relativeUrl.LastIndexOf("#", StringComparison.Ordinal));
           
            //Incorrect url
            if (hasRouteError.HasValue && hasRouteError.Value)
                return $"{baseUrl}/error/{language}.json";
           
            IBlazorItem rootItem = _blazorItemsService.GetBlazorItemRootMock(language);

            if (rootItem.GetItSelfAndDescendants().Any(item => item.Url == "/" + relativeUrl) || relativeUrl == "")
            {
                if(relativeUrl.Length<=language.Length)
                    return $"{baseUrl}/{language}.json";

                return $"{baseUrl}{relativeUrl.Substring(language.Length)}/{language}.json";

            }
                

            return $"{baseUrl}/error/{language}.json";


        }


        [Obsolete("Not used", true)]
        public (bool IsCurrentUrl, string CurrentUrl)? UrlIsCurrent()
        {
            string relativeUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

            if (string.IsNullOrWhiteSpace(_blazorStateMachine.CurrentRoute?.ItemLanguage))
                return (false, $"/{relativeUrl}");

            IBlazorItem rootItem = _blazorItemsService.GetBlazorItemRootMock(_blazorStateMachine?.CurrentRoute?.ItemLanguage);

            return _blazorStateMachine?.CurrentRoute != null && rootItem.GetItSelfAndDescendants().Any(item => item.Url == "/" + relativeUrl && item.Id == _blazorStateMachine.CurrentRoute.Id)
              ? (true, $"/{relativeUrl}")
              : (false, $"/{relativeUrl}");
        }

        public async Task LoadRoute(string language, bool hasRouteError = false )
        {
            string routeUrl = BuildRouteApiUrl(language, hasRouteError);

            BlazorRoute? routeExists = _blazorStateMachine.GetNavigatedRoute(routeUrl);
            
            if (routeExists == null) {
                _blazorStateMachine.CurrentRoute = await _restService.ExecuteRestMethodWithJsonSerializerOptions<BlazorRoute>(routeUrl, null);
                _blazorStateMachine.AddNavigatedRoute(routeUrl, _blazorStateMachine.CurrentRoute);
            }
            else
            {
                _blazorStateMachine.CurrentRoute = routeExists;

            }

           
            _blazorStateMachine.CurrentRoute.Url = routeUrl;
            _blazorStateMachine.CurrentPlaceholders = _blazorStateMachine.CurrentRoute.Placeholders.FlattenThePlaceholders();
            _blazorStateMachine.Language = language;
            _blazorStateMachine.RouteId = _blazorStateMachine.CurrentRoute.Id;
           
        }

    }
}
