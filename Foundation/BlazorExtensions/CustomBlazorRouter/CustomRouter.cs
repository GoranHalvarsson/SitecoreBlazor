using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
    /// <summary>
    /// A component that displays whichever other component corresponds to the
    /// current navigation location.
    /// </summary>
    public class CustomRouter : Microsoft.AspNetCore.Components.IComponent, IDisposable
    {
        static readonly char[] _queryOrHashStartChar = new[] { '?', '#' };
        private RenderHandle _renderHandle;
        private string _baseUri;
        private string _locationAbsolute;
        
        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private LanguageService LanguageService { get; set; }

        [Parameter] private RouterDataRoot RouteValues { get; set; }
        /// <summary>
        /// Gets or sets the assembly that should be searched, along with its referenced
        /// assemblies, for components matching the URI.
        /// </summary>
        [Parameter] private Assembly AppAssembly { get; set; }


        /// <summary>
        /// Gets or sets the type of the component that should be used as a fallback when no match is found for the requested route.
        /// </summary>
        [Parameter] private Type FallbackComponent { get; set; }

        private RouteTable Routes { get; set; }


        public void Configure(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
            _baseUri = UriHelper.GetBaseUri();
            _locationAbsolute = UriHelper.GetAbsoluteUri();
            UriHelper.OnLocationChanged += OnLocationChanged;
        }

        public Task SetParametersAsync(ParameterCollection parameters)
        {
            parameters.SetParameterProperties(this);

            Routes = RouteTable.CreateNew(RouteValues);
            Refresh();
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
            builder.OpenComponent(0, typeof(LayoutDisplay));
            builder.AddAttribute(1, LayoutDisplay.NameOfPage, handler);
            builder.AddAttribute(2, LayoutDisplay.NameOfPageParameters, parameters);
            builder.CloseComponent();
        }

        

        private void Refresh()
        {
            //Custom
            var locationPath = UriHelper.ToBaseRelativePath(_baseUri, _locationAbsolute);
            locationPath = StringUntilAny(locationPath, _queryOrHashStartChar);

            //Custom
            if (string.IsNullOrWhiteSpace(locationPath))
                locationPath = LanguageService.GetDefaultLanguage().TwoLetterCode;


            var context = new RouteContext(locationPath);
            Routes.Route(context);

            //Custom - Not valid route
            if (context.Handler == null || !LanguageService.HasValidLanguageInUrl(_baseUri, locationPath))
                context = SetErrorContext(locationPath);

            if (context.Handler == null)
            {
                if (FallbackComponent != null)
                {
                    context.Handler = FallbackComponent;
                }
                else
                {
                    throw new InvalidOperationException($"'{nameof(CustomRouter)}' cannot find any component with a route for '/{locationPath}', and no fallback is defined.");
                }
            }

            if (!typeof(Microsoft.AspNetCore.Components.IComponent).IsAssignableFrom(context.Handler))
            {
                throw new InvalidOperationException($"The type {context.Handler.FullName} " +
                    $"does not implement {typeof(Microsoft.AspNetCore.Components.IComponent).FullName}.");
            }

            _renderHandle.Render(builder => Render(builder, context.Handler, context.Parameters));
        }

        private RouteContext SetErrorContext(string locationPath)
        {
            Language currentLanguage = LanguageService.GetLanguageFromUrl(_baseUri, locationPath);

            var errContext = new RouteContext($"{currentLanguage.TwoLetterCode}?{Constants.Route.RouteError}");
            Routes.Route(errContext);
            RouteContext context = errContext;
            return context;
        }

        private void OnLocationChanged(object sender, string newAbsoluteUri)
        {
            _locationAbsolute = newAbsoluteUri;
            if (_renderHandle.IsInitialized)
            {
                Refresh();
            }
        }


    }
}
