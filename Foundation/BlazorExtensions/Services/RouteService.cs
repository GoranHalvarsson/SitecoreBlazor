using Foundation.BlazorExtensions.Extensions;
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
        private readonly Microsoft.AspNetCore.Components.Services.IUriHelper _uriHelper;
        private readonly SitecoreItemService _sitecoreItemService;
        
        public RouteService(RestService restService, Microsoft.AspNetCore.Components.Services.IUriHelper uriHelper, SitecoreItemService sitecoreItemService, BlazorContext blazorContext)
        {
            _restService = restService;
            _uriHelper = uriHelper;
            _sitecoreItemService = sitecoreItemService;
            BlazorContext = blazorContext;
        }

        private BlazorContext BlazorContext { get; }
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

        public async Task<(Route route, IEnumerable<KeyValuePair<string, IList<Placeholder>>> flattenedPlaceholders)> LoadRoute(string language = null, bool hasRouteError = false )
        {
            string routeUrl = BuildRouteApiUrl(language, hasRouteError);

            this.CurrentRoute = await _restService.ExecuteRestMethod<Route>(routeUrl);

            this.CurrentPlaceholders = CurrentRoute.Placeholders.FlattenThePlaceholders();

            await BlazorContext.SetCurrentRouteIdAsync(CurrentRoute.Id);
            await BlazorContext.SetContextLanguageAsync(language);

            return new ValueTuple<Route, IEnumerable<KeyValuePair<string, IList<Placeholder>>>>(CurrentRoute, CurrentPlaceholders);
        }

    }
}
