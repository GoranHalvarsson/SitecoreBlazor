using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class RouteService
    {
        private readonly IRestService _restService;
        private readonly IUriHelper _uriHelper;
        private readonly BlazorItemsService _blazorItemsService;
        private readonly BlazorStateMachine _blazorStateMachine;

        public RouteService(IRestService restService, IUriHelper uriHelper, BlazorItemsService blazorItemsService, BlazorStateMachine blazorStateMachine)
        {
            _restService = restService;
            _uriHelper = uriHelper;
            _blazorItemsService = blazorItemsService;
            _blazorStateMachine = blazorStateMachine;
        }

        
        public string BuildRouteApiUrl(string language, bool? hasRouteError)
        {
            string baseUrl = $"{_uriHelper.GetBaseUri()}/data/routes";

            string relativeUrl = $"{_uriHelper.ToBaseRelativePath(_uriHelper.GetBaseUri(), _uriHelper.GetAbsoluteUri())}";

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



        public (bool IsCurrentUrl, string CurrentUrl) UrlIsCurrent()
        {
            string relativeUrl = _uriHelper.ToBaseRelativePath(_uriHelper.GetBaseUri(), _uriHelper.GetAbsoluteUri());

            if (string.IsNullOrWhiteSpace(_blazorStateMachine.CurrentRoute?.ItemLanguage))
                return (false, $"/{relativeUrl}");

            IBlazorItem rootItem = _blazorItemsService.GetBlazorItemRootMock(_blazorStateMachine.CurrentRoute.ItemLanguage);

            return _blazorStateMachine.CurrentRoute != null && rootItem.GetItSelfAndDescendants().Any(item => item.Url == "/" + relativeUrl && item.Id == _blazorStateMachine.CurrentRoute.Id)
              ? (true, $"/{relativeUrl}")
              : (false, $"/{relativeUrl}");
        }

        public async Task LoadRoute(string language = null, bool hasRouteError = false )
        {
            string routeUrl = BuildRouteApiUrl(language, hasRouteError);

            BlazorRoute routeExists = _blazorStateMachine.GetNavigatedRoute(routeUrl);
            
            if (routeExists == null) {
                _blazorStateMachine.CurrentRoute = await _restService.ExecuteRestMethodWithJsonSerializerOptions<BlazorRoute>(routeUrl);
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
