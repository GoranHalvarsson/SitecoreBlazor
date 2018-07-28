using Foundation.BlazorExtensions.Extensions;
using Foundation.BlazorExtensions.Factories;
using Microsoft.AspNetCore.Blazor;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class LayoutService
  {
    private readonly ComponentFactory _componentFactory;
    private readonly RouteService _routeService;
    private readonly Microsoft.AspNetCore.Blazor.Services.IUriHelper _uriHelper;
    public LayoutService(ComponentFactory componentFactory, RouteService routeService, Microsoft.AspNetCore.Blazor.Services.IUriHelper uriHelper)
    {
      _componentFactory = componentFactory;
      _routeService = routeService;
      _uriHelper = uriHelper;
    }


    /// <summary>
    /// LoadRoute is called on state changed(each "request")
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public async Task LoadRoute(string language)
    {

      if (_routeService.FlattenPlaceholders == null || !_routeService.UrlIsCurrent().IsCurrentUrl)
      {

        RenderedComponentsInDynamicPlaceholdersPerStateChanged = new List<string>();

        await _routeService.LoadRoute(language);
      }
    }


    private List<string> RenderedComponentsInDynamicPlaceholdersPerStateChanged
    {
      get; set;
    }
  

    public Task<List<RenderFragment>> RenderPlaceholders(string placeholder, CancellationToken cancellationToken = default)
    {
      return Task.Run(() =>
      {
        List<RenderFragment> list = new List<RenderFragment>();


        try
        {

          IEnumerable<KeyValuePair<string, IList<Placeholder>>> placeHoldersList = _routeService.FlattenPlaceholders.Where(fp => fp.Key.ExtractPlaceholderName() == placeholder);

          foreach (KeyValuePair<string, IList<Placeholder>> keyVal in placeHoldersList)
          {

            cancellationToken.ThrowIfCancellationRequested();

            foreach (Placeholder placeholderData in keyVal.Value)
            {

              cancellationToken.ThrowIfCancellationRequested();

              if (placeholderData == null)
                continue;

              string keyName = $"{keyVal.Key}-{placeholderData.ComponentName}";

              if (RenderedComponentsInDynamicPlaceholdersPerStateChanged.Any(comp => comp == keyName))
                continue;


              list.Add(_componentFactory.CreateComponent(placeholderData));

              if (keyVal.Key.IsDynamicPlaceholder())
                RenderedComponentsInDynamicPlaceholdersPerStateChanged.Add(keyName);

            }
          }


        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error RenderPlaceholders {ex.Message}");
        }

        return list;

      }, cancellationToken);
    }

  }
}
