using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{



    /// <summary>
    /// A component that displays whichever other component corresponds to the
    /// current navigation location.
    /// </summary>
    public class TheRouter : IComponent, IHandleAfterRender, IDisposable
    {
        static readonly char[] _queryOrHashStartChar = new[] { '?', '#' };
        static readonly ReadOnlyDictionary<string, object> _emptyParametersDictionary
           = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        private RenderHandle _renderHandle;
        private string _baseUri;
        private string _locationAbsolute;
        bool _navigationInterceptionEnabled;
        ILogger<Router> _logger;

       

        //[Inject]
        //private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }


        [Inject] private INavigationInterception NavigationInterception { get; set; }


        [Inject] private IComponentContext ComponentContext { get; set; }


        [Inject] private ILoggerFactory LoggerFactory { get; set; }

        [Inject]
        private LanguageService LanguageService { get; set; }


        [Parameter] public RouterDataRoot RouteValues { get; set; }


        [Parameter] public Assembly AppAssembly { get; set; }


        [Parameter] public RenderFragment NotFound { get; set; }


        [Parameter] public RenderFragment<RouteData> Found { get; set; }

        /// <summary>
        /// The content that will be displayed if the user is not authorized.
        /// </summary>
        [Parameter] public RenderFragment<AuthenticationState> NotAuthorizedContent { get; private set; }

        /// <summary>
        /// The content that will be displayed while asynchronous authorization is in progress.
        /// </summary>
        [Parameter] public RenderFragment AuthorizingContent { get; private set; }

        private RouteTable Routes { get; set; }


        public void Attach(RenderHandle renderHandle)
        {
            _logger = LoggerFactory.CreateLogger<Router>();
            _renderHandle = renderHandle;
            //_baseUri = NavigationManagerResolver().BaseUri;
            //_locationAbsolute = NavigationManagerResolver().Uri;
            //NavigationManagerResolver().LocationChanged += OnLocationChanged;

            //_renderHandle = renderHandle;
            _baseUri = UriHelper.GetBaseUri();
            _locationAbsolute = UriHelper.GetAbsoluteUri();
            UriHelper.OnLocationChanged += OnLocationChanged;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);


            // Found content is mandatory, because even though we could use something like <RouteView ...> as a
            // reasonable default, if it's not declared explicitly in the template then people will have no way
            // to discover how to customize this (e.g., to add authorization).
            //if (Found == null)
            //{
            //    throw new InvalidOperationException($"The {nameof(Router)} component requires a value for the parameter {nameof(Found)}.");
            //}

            // NotFound content is mandatory, because even though we could display a default message like "Not found",
            // it has to be specified explicitly so that it can also be wrapped in a specific layout
            //if (NotFound == null)
            //{
            //    throw new InvalidOperationException($"The {nameof(Router)} component requires a value for the parameter {nameof(NotFound)}.");
            //}


            Routes = RouteTableFactory.CreateFromRouterDataRoot(RouteValues);
            //Routes = RouteTableFactory.Create(AppAssembly);
            Refresh(isNavigationIntercepted: false);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            UriHelper.OnLocationChanged -= OnLocationChanged;
        }

        private string StringUntilAny(string str, char[] chars)
        {
            var firstIndex = str.IndexOfAny(chars);
            return firstIndex < 0
                ? str
                : str.Substring(0, firstIndex);
        }

        protected virtual void Render(RenderTreeBuilder builder, Type handler, IDictionary<string, object> parameters)
        {
            builder.OpenComponent(0, typeof(PageDisplay));
            builder.AddAttribute(1, nameof(PageDisplay.Page), handler);
            builder.AddAttribute(2, nameof(PageDisplay.PageParameters), parameters);
            builder.AddAttribute(3, nameof(PageDisplay.NotAuthorizedContent), NotAuthorizedContent);
            builder.AddAttribute(4, nameof(PageDisplay.AuthorizingContent), AuthorizingContent);
            builder.CloseComponent();
        }

        private void Refresh(bool isNavigationIntercepted)
        {

            var locationPath = UriHelper.ToBaseRelativePath(_baseUri, _locationAbsolute);
            locationPath = StringUntilAny(locationPath, _queryOrHashStartChar);


            // Custom
            if (string.IsNullOrWhiteSpace(locationPath))
                locationPath = LanguageService.GetDefaultLanguage().TwoLetterCode;


            var context = new RouteContext(locationPath);
            Routes.Route(context);

            // Custom - Not valid route
            if (context.Handler == null || !LanguageService.HasValidLanguageInUrl(_baseUri, locationPath))
                context = SetErrorContext(locationPath);


            if (context.Handler != null)
            {
                if (!typeof(IComponent).IsAssignableFrom(context.Handler))
                {
                    throw new InvalidOperationException($"The type {context.Handler.FullName} " +
                        $"does not implement {typeof(IComponent).FullName}.");
                }

                Log.NavigatingToComponent(_logger, context.Handler, locationPath, _baseUri);

                var routeData = new RouteData(
                    context.Handler,
                    context.Parameters ?? _emptyParametersDictionary);


                _renderHandle.Render(builder => Render(builder, context.Handler, context.Parameters));
                
            }
            else
            {
                if (!isNavigationIntercepted)
                {
                    Log.DisplayingNotFound(_logger, locationPath, _baseUri);

                    // We did not find a Component that matches the route.
                    // Only show the NotFound content if the application developer programatically got us here i.e we did not
                    // intercept the navigation. In all other cases, force a browser navigation since this could be non-Blazor content.
                    _renderHandle.Render(NotFound);
                }
                else
                {
                    Log.NavigatingToExternalUri(_logger, _locationAbsolute, locationPath, _baseUri);
                    UriHelper.NavigateTo(_locationAbsolute, forceLoad: true);
                }
            }



        }


        private RouteContext SetErrorContext(string locationPath)
        {
            Language currentLanguage = LanguageService.GetLanguageFromUrl(_baseUri, locationPath);

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
            if (!_navigationInterceptionEnabled && ComponentContext.IsConnected)
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
