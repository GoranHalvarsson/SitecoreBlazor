using Foundation.BlazorExtensions.Components;
using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{



    /// <summary>
    /// Custom route component that displays whichever other component corresponds to the
    /// current navigation location.
    /// </summary>
    public sealed class TheRouter : IComponent, IHandleAfterRender, IDisposable
    {
        static readonly char[] _queryOrHashStartChar = new[] { '?', '#' };
        static readonly ReadOnlyDictionary<string, object> _emptyParametersDictionary
           = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        private RenderHandle _renderHandle;
        private string _baseUri;
        private string _locationAbsolute;
        bool _navigationInterceptionEnabled;
        ILogger<Router> _logger;

       

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        [Inject] private INavigationInterception NavigationInterception { get; set; }


        [Inject] private ILoggerFactory LoggerFactory { get; set; }

        [Inject]
        private LanguageService LanguageService { get; set; }


        [Parameter] public RouterDataRoot RouteValues { get; set; }


        [Parameter] public Assembly AppAssembly { get; set; }


        [Parameter] public RenderFragment NotFound { get; set; }

        /// <summary>
        /// Gets or sets the content to display when a match is found for the requested route.
        /// </summary>
        [Parameter] public RenderFragment<RouteData> Found { get; set; }

        /// <summary>
        /// We need it in order to set the current route language parameter
        /// </summary>
        [CascadingParameter]
        public ContextStateProvider ContextStateProvider { get; set; }

        private RouteTable Routes { get; set; }


        public void Attach(RenderHandle renderHandle)
        {
            _logger = LoggerFactory.CreateLogger<Router>();
            _renderHandle = renderHandle;
            _baseUri = NavigationManager.BaseUri;
            _locationAbsolute = NavigationManager.Uri;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            Routes = RouteTableFactory.CreateFromRouterDataRoot(RouteValues);
            //Routes = RouteTableFactory.Create(AppAssembly);
            Refresh(isNavigationIntercepted: false);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        private string StringUntilAny(string str, char[] chars)
        {
            var firstIndex = str.IndexOfAny(chars);
            return firstIndex < 0
                ? str
                : str.Substring(0, firstIndex);
        }

       
        private void Refresh(bool isNavigationIntercepted)
        {
            var locationPath = NavigationManager.ToBaseRelativePath(_locationAbsolute);
            locationPath = StringUntilAny(locationPath, _queryOrHashStartChar);


            // Custom
            if (string.IsNullOrWhiteSpace(locationPath))
                locationPath = LanguageService.GetDefaultLanguage().TwoLetterCode;


            var context = new RouteContext(locationPath);
            Routes.Route(context);

            // Custom - Not valid route
            if ((context.Handler == null && NotFound == null) || !LanguageService.HasValidLanguageInUrl(locationPath))
                context = SetErrorContext(locationPath);


            if (context.Handler != null)
            {
                if (!typeof(IComponent).IsAssignableFrom(context.Handler))
                {
                    throw new InvalidOperationException($"The type {context.Handler.FullName} " +
                        $"does not implement {typeof(IComponent).FullName}.");
                }

                Log.NavigatingToComponent(_logger, context.Handler, locationPath, _baseUri);

                //Custom - Adding language param if missing, also set default language 
                if (!context.Parameters.ContainsKey("Language"))
                    context.Parameters.Add("Language", LanguageService.GetDefaultLanguage().TwoLetterCode.ToString());

                //Custom - Has nu language, set default language
                if (string.IsNullOrWhiteSpace(context.Parameters["Language"].ToString()))
                    context.Parameters["Language"] = LanguageService.GetDefaultLanguage().TwoLetterCode.ToString();

                //Custom - Set the language CascadingParameter 
                ContextStateProvider.RouteLanguage = context.Parameters["Language"].ToString();

                RouteData routeData = new RouteData(
                   context.Handler,
                   context.Parameters ?? _emptyParametersDictionary);
                 

                _renderHandle.Render(Found(routeData));

            }
            else
            {
                if (!isNavigationIntercepted)
                {

                    Console.WriteLine("aaaaa");

                    Log.DisplayingNotFound(_logger, locationPath, _baseUri);

                    // We did not find a Component that matches the route.
                    // Only show the NotFound content if the application developer programatically got us here i.e we did not
                    // intercept the navigation. In all other cases, force a browser navigation since this could be non-Blazor content.
                    _renderHandle.Render(NotFound);
                }
                else
                {
                    Console.WriteLine("ccccc");


                   Log.NavigatingToExternalUri(_logger, _locationAbsolute, locationPath, _baseUri);
                    NavigationManager.NavigateTo(_locationAbsolute, forceLoad: true);
                }
            }



        }


        private RouteContext SetErrorContext(string locationPath)
        {
            Language currentLanguage = LanguageService.GetLanguageFromUrl(locationPath);

            var errContext = new RouteContext($"{currentLanguage.TwoLetterCode}?{Constants.Route.RouteError}");
            Routes.Route(errContext);
            RouteContext context = errContext;
            return context;
        }


        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            _locationAbsolute = args.Location;
            if (_renderHandle.IsInitialized && Routes != null)
            {
                Refresh(args.IsNavigationIntercepted);
            }
        }

        Task IHandleAfterRender.OnAfterRenderAsync()
        {
            if (!_navigationInterceptionEnabled)
            {
                _navigationInterceptionEnabled = true;
                return NavigationInterception.EnableNavigationInterceptionAsync();
            }

            return Task.CompletedTask;
        }


        private static class Log
        {
            private static readonly Action<ILogger, string, string, Exception> _displayingNotFound =
                LoggerMessage.Define<string, string>(LogLevel.Debug, new EventId(1, "DisplayingNotFound"), $"Displaying {nameof(NotFound)} because path '{{Path}}' with base URI '{{BaseUri}}' does not match any component route");

            private static readonly Action<ILogger, Type, string, string, Exception> _navigatingToComponent =
                LoggerMessage.Define<Type, string, string>(LogLevel.Debug, new EventId(2, "NavigatingToComponent"), "Navigating to component {ComponentType} in response to path '{Path}' with base URI '{BaseUri}'");

            private static readonly Action<ILogger, string, string, string, Exception> _navigatingToExternalUri =
                LoggerMessage.Define<string, string, string>(LogLevel.Debug, new EventId(3, "NavigatingToExternalUri"), "Navigating to non-component URI '{ExternalUri}' in response to path '{Path}' with base URI '{BaseUri}'");

            internal static void DisplayingNotFound(ILogger logger, string path, string baseUri)
            {
                _displayingNotFound(logger, path, baseUri, null);
            }

            internal static void NavigatingToComponent(ILogger logger, Type componentType, string path, string baseUri)
            {
                _navigatingToComponent(logger, componentType, path, baseUri, null);
            }

            internal static void NavigatingToExternalUri(ILogger logger, string externalUri, string path, string baseUri)
            {
                _navigatingToExternalUri(logger, externalUri, path, baseUri, null);
            }
        }
    }
}
