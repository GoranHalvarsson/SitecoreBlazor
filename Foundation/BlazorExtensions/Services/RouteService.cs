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

    public RouteService(RestService restService, Microsoft.AspNetCore.Blazor.Services.IUriHelper uriHelper, SessionStorage sessionStorage, SitecoreItemService sitecoreItemService)
    {
      _restService = restService;
      _uriHelper = uriHelper;
      SessionStorage = sessionStorage;
      _sitecoreItemService = sitecoreItemService;
    }

    private SessionStorage SessionStorage { get; set; }

    public Route CurrentRoute { get; set; }

    public IEnumerable<KeyValuePair<string, IList<Placeholder>>> FlattenPlaceholders { get; set; }


    //TODO Move to languageservice
    private bool IsValidLanguage(string language)
    {
      string[] validLanguages = new string[] { "sv", "en" };
      return validLanguages.Any(vl => vl == language);
    }

    public string BuildRouteApiUrl(string language)
    {
      string baseUrl = $"{_uriHelper.GetBaseUri()}/data/routes";

      string relativeUrl = $"{_uriHelper.ToBaseRelativePath(_uriHelper.GetBaseUri(), _uriHelper.GetAbsoluteUri())}";

      //Language is wrong
      if (!IsValidLanguage(language))
      {
        language = "en"; //TODO move to languageservice GetFallbackLanguage;
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
        language = "en"; //default language

      if (language == null && CurrentRoute?.ItemLanguage != null)
        language = CurrentRoute.ItemLanguage;

      if (!IsValidLanguage(language))
        language = "en"; //TODO move to languageservice GetFallbackLanguage;


      SessionStorage.SetItem("contextLanguage", language);

      string routeUrl = BuildRouteApiUrl(language);


      CurrentRoute = await _restService.ExecuteRestMethod<Route>(routeUrl);

      SessionStorage.SetItem("contextRoute", CurrentRoute);

      FlattenPlaceholders = CurrentRoute.Placeholders.FlattenPlaceholders();

      return new ValueTuple<Route, IEnumerable<KeyValuePair<string, IList<Placeholder>>>>(CurrentRoute, FlattenPlaceholders);
    }

  }
}
