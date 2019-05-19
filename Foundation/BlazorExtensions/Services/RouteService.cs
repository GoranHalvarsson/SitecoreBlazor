using Foundation.BlazorExtensions.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SitecoreBlazorHosted.Shared;
using SitecoreBlazorHosted.Shared.Models;
using SitecoreBlazorHosted.Shared.Models.Sitecore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class RouteService
    {
        private readonly RestService _restService;
        private readonly IUriHelper _uriHelper;
        private readonly SitecoreItemService _sitecoreItemService;
        private readonly PoorManSessionState _poorManSessionState;

        public RouteService(RestService restService, IUriHelper uriHelper, SitecoreItemService sitecoreItemService, PoorManSessionState poorManSessionState)
        {
            _restService = restService;
            _uriHelper = uriHelper;
            _sitecoreItemService = sitecoreItemService;
            _poorManSessionState = poorManSessionState;
        }

        private Route CurrentRoute { get; set; }

        public IEnumerable<KeyValuePair<string, IList<Placeholder>>> CurrentPlaceholders { get; set; }

       
        public string BuildRouteApiUrl(string language, bool? hasRouteError)
        {
            string baseUrl = $"{_uriHelper.GetBaseUri()}/data/routes";

            string relativeUrl = $"{_uriHelper.ToBaseRelativePath(_uriHelper.GetBaseUri(), _uriHelper.GetAbsoluteUri())}";

            //Incorrect url
            if (hasRouteError.HasValue && hasRouteError.Value)
                return $"{baseUrl}/error/{language}.json";
           
            ISitecoreItem rootItem = _sitecoreItemService.GetSitecoreItemRootMock(language);

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

            if (string.IsNullOrWhiteSpace(CurrentRoute?.ItemLanguage))
                return (false, $"/{relativeUrl}");

            ISitecoreItem rootItem = _sitecoreItemService.GetSitecoreItemRootMock(CurrentRoute.ItemLanguage);

            return CurrentRoute != null && rootItem.GetItSelfAndDescendants().Any(item => item.Url == "/" + relativeUrl && item.Id == CurrentRoute.Id)
              ? (true, $"/{relativeUrl}")
              : (false, $"/{relativeUrl}");
        }

        public async Task LoadRoute(string language = null, bool hasRouteError = false )
        {
            string routeUrl = BuildRouteApiUrl(language, hasRouteError);

            Route routeExists = _poorManSessionState.GetNavigatedRoute(routeUrl);
            
            if (routeExists == null) { 
                this.CurrentRoute = await _restService.ExecuteRestMethod<Route>(routeUrl);
                _poorManSessionState.AddNavigatedRoute(routeUrl, this.CurrentRoute);
            }
            else
            {
                this.CurrentRoute = routeExists;

            }

            this.CurrentRoute.Url = routeUrl;

            this.CurrentPlaceholders = CurrentRoute.Placeholders.FlattenThePlaceholders();

            _poorManSessionState.Language = language;
            _poorManSessionState.RouteId = CurrentRoute.Id;
           
        }

    }
}
