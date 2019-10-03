

using Microsoft.JSInterop;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions
{
    //!TODO FIX with cookies 

    public class BlazorStateMachineResolver
    {
        private readonly SessionStorage _sessionStorage;
        private readonly IJSRuntime? _jsRuntime;



        public BlazorStateMachineResolver(SessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage ?? throw new ArgumentException("The value cannot be null", nameof(sessionStorage));
        }


        public BlazorStateMachineResolver(SessionStorage sessionStorage, IJSRuntime jsRuntime) : this(sessionStorage)
        {
            _jsRuntime = jsRuntime ?? throw new ArgumentException("The value cannot be null", nameof(jsRuntime));
        }

        public ValueTask<string> GetContextLanguageAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, _jsRuntime);
        }

        public ValueTask<object> SetContextLanguageAsync(string language)
        {

            return String.IsNullOrWhiteSpace(language)
                ? new ValueTask<object>()
                : _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.ContextLanguage, language, _jsRuntime);
        }


        public ValueTask<string> GetCurrentRouteIdAsync()
        {
            return _sessionStorage.GetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, _jsRuntime);
        }

        

        public ValueTask<object> SetCurrentRouteIdAsync(string routeId)
        {

            return String.IsNullOrWhiteSpace(routeId)
                ? new ValueTask<object>()
                : _sessionStorage.SetItemAsync(Constants.Storage.StorageKeys.CurrentRouteId, routeId, _jsRuntime);
        }


        public Task<IList<Tuple<DateTime, string, BlazorRoute>>> GetNavigatedRouteAsync()
        {
            return _sessionStorage.GetItemAsync<IList<Tuple<DateTime, string, BlazorRoute>>>(Constants.Storage.StorageKeys.NavigatedRoutes, _jsRuntime);
        }

        

        public ValueTask<object> SetCurrentNavigatedRoutesAsync(IList<Tuple<DateTime, string, BlazorRoute>> navigatedRoutes)
        {

            return navigatedRoutes == null
                ? new ValueTask<object>()
                : _sessionStorage.SetItemAsync<IList<Tuple<DateTime, string, BlazorRoute>>>(Constants.Storage.StorageKeys.NavigatedRoutes, navigatedRoutes, _jsRuntime);
        }


        //public Func<string, string> GetNavigationRootItemJson = (lang) => _sessionStorage.GetItem<string>("navigationRootItem_Json_" + lang);

        //internal Action<string, string> SetNavigationRootItemJson = (lang, json) => _sessionStorage.SetItem("navigationRootItem_Json_" + lang, json);





    }
}
