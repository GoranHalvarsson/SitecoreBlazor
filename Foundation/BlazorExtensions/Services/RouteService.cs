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
        private readonly Microsoft.AspNetCore.Blazor.Services.IUriHelper _uriHelper;
        private readonly SitecoreItemService _sitecoreItemService;
        private readonly LanguageService _languageService;

        public RouteService(RestService restService, Microsoft.AspNetCore.Blazor.Services.IUriHelper uriHelper, SitecoreItemService sitecoreItemService, BlazorContext blazorContext, LanguageService languageService)
        {
            _restService = restService;
            _uriHelper = uriHelper;
            _sitecoreItemService = sitecoreItemService;
            BlazorContext = blazorContext;
            _languageService = languageService;
        }

        private BlazorContext BlazorContext { get; }
        private Route CurrentRoute { get; set; }

        public IEnumerable<KeyValuePair<string, IList<Placeholder>>> FlattenPlaceholders { get; set; }

       
        public string BuildRouteApiUrl(string language)
        {
            string baseUrl = $"{_uriHelper.GetBaseUri()}/data/routes";

            string relativeUrl = $"{_uriHelper.ToBaseRelativePath(_uriHelper.GetBaseUri(), _uriHelper.GetAbsoluteUri())}";

            //Language is wrong
            if (!_languageService.IsValidLanguage(language))
            {
                language = _languageService.GetDefaultLanguage()?.TwoLetterCode;
                return $"{baseUrl}/error/{language}.json";
            }

            ISitecoreItem rootItem = _sitecoreItemService.GetSitecoreItemRootMock(language);

            if (rootItem.GetItSelfAndDescendants().Any(item => item.Url == "/" + relativeUrl) || relativeUrl == "")
                return $"{baseUrl}{relativeUrl.Substring(language.Length)}/{language}.json";

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

        public async Task<(Route route, IEnumerable<KeyValuePair<string, IList<Placeholder>>> flattenedPlaceholders)> LoadRoute(string language = null)
        {
            if (language == null && CurrentRoute?.ItemLanguage == null)
                language = _languageService.GetDefaultLanguage()?.TwoLetterCode;

            if (language == null && CurrentRoute?.ItemLanguage != null)
                language = CurrentRoute.ItemLanguage;

            if (!_languageService.IsValidLanguage(language))
                language = _languageService.GetDefaultLanguage()?.TwoLetterCode;

          
            string routeUrl = BuildRouteApiUrl(language);

            this.CurrentRoute = await _restService.ExecuteRestMethod<Route>(routeUrl);

            this.FlattenPlaceholders = CurrentRoute.Placeholders.FlattenPlaceholders();

            await BlazorContext.SetCurrentRouteIdAsync(CurrentRoute.Id);
            await BlazorContext.SetContextLanguageAsync(language);


            return new ValueTuple<Route, IEnumerable<KeyValuePair<string, IList<Placeholder>>>>(CurrentRoute, FlattenPlaceholders);
        }

    }
}
