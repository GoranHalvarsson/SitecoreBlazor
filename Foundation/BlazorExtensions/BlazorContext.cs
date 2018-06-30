

using SitecoreBlazorHosted.Shared.Models;
using System;

namespace Foundation.BlazorExtensions
{


  public class BlazorContext
  {
    private static SessionStorage _sessionStorage;

    public BlazorContext(SessionStorage sessionStorage)
    {
      _sessionStorage = sessionStorage;
    }


    public string ContextLanguage
    {
      get
      {

        if (_sessionStorage?.GetItem("contextLanguage") == null)
          _sessionStorage.SetItem("contextLanguage", "en");

        return _sessionStorage.GetItem("contextLanguage");
      }
    }


    public Route ContextRoute
    {
      get
      {
        return _sessionStorage.GetItem<Route>("contextRoute");
      }
    }


    public Func<string, string> GetNavigationRootItemJson = (lang) => _sessionStorage.GetItem<string>("navigationRootItem_Json_" + lang);

    internal Action<string, string> SetNavigationRootItemJson = (lang, json) => _sessionStorage.SetItem("navigationRootItem_Json_" + lang, json);





  }
}
