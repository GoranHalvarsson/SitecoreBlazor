using Foundation.BlazorExtensions.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
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
    //[Obsolete("Preview 7", true)]
    //public class CustomRouter : IComponent, IHandleAfterRender, IDisposable
    //{
    //    static readonly char[] _queryOrHashStartChar = new[] { '?', '#' };
    //    private RenderHandle _renderHandle;
    //    private string _baseUri;
    //    private string _locationAbsolute;
    //    bool _navigationInterceptionEnabled;

    //    [Inject]
    //    private IUriHelper UriHelper { get; set; }

    //    [Inject] private INavigationInterception NavigationInterception { get; set; }

    //    [Inject] private IComponentContext ComponentContext { get; set; }

    //    [Inject]
    //    private LanguageService LanguageService { get; set; }


    //    [Parameter] private RouterDataRoot RouteValues { get; set; }
    //    /// <summary>
    //    /// Gets or sets the assembly that should be searched, along with its referenced
    //    /// assemblies, for components matching the URI.
    //    /// </summary>
    //    [Parameter] private Assembly AppAssembly { get; set; }

    //    /// <summary>
    //    /// Gets or sets the type of the component that should be used as a fallback when no match is found for the requested route.
    //    /// </summary>
    //    [Parameter] public RenderFragment NotFoundContent { get; private set; }

    //    /// <summary>
    //    /// The content that will be displayed if the user is not authorized.
    //    /// </summary>
    //    [Parameter] public RenderFragment<AuthenticationState> NotAuthorizedContent { get; private set; }

    //    /// <summary>
    //    /// The content that will be displayed while asynchronous authorization is in progress.
    //    /// </summary>
    //    [Parameter] public RenderFragment AuthorizingContent { get; private set; }

    //    private RouteTable Routes { get; set; }


    //    public void Configure(RenderHandle renderHandle)
    //    {
    //        _renderHandle = renderHandle;
    //        _baseUri = UriHelper.GetBaseUri();
    //        _locationAbsolute = UriHelper.GetAbsoluteUri();
    //        UriHelper.OnLocationChanged += OnLocationChanged;
    //    }

    //    public Task SetParametersAsync(ParameterView parameters)
    //    {
    //        parameters.SetParameterProperties(this);

    //        Routes = RouteTable.CreateNew(RouteValues);
    //        Refresh(isNavigationIntercepted: false);
    //        return Task.CompletedTask;
    //    }

    //    /// <inheritdoc />
    //    public void Dispose()
    //    {
    //        UriHelper.OnLocationChanged -= OnLocationChanged;
    //    }

    //    private string StringUntilAny(string str, char[] chars)
    //    {
    //        var firstIndex = str.IndexOfAny(chars);
    //        return firstIndex < 0
    //            ? str
    //            : str.Substring(0, firstIndex);
    //    }

    //    protected virtual void Render(RenderTreeBuilder builder, Type handler, IDictionary<string, object> parameters)
    //    {
    //        builder.OpenComponent(0, typeof(PageDisplay));
    //        builder.AddAttribute(1, nameof(PageDisplay.Page), handler);
    //        builder.AddAttribute(2, nameof(PageDisplay.PageParameters), parameters);
    //        builder.AddAttribute(3, nameof(PageDisplay.NotAuthorizedContent), NotAuthorizedContent);
    //        builder.AddAttribute(4, nameof(PageDisplay.AuthorizingContent), AuthorizingContent);
    //        builder.CloseComponent();
    //    }

    //    private void Refresh(bool isNavigationIntercepted)
    //    {
    //        var locationPath = UriHelper.ToBaseRelativePath(_baseUri, _locationAbsolute);
    //        locationPath = StringUntilAny(locationPath, _queryOrHashStartChar);

    //        // Custom
    //        if (string.IsNullOrWhiteSpace(locationPath))
    //            locationPath = LanguageService.GetDefaultLanguage().TwoLetterCode;


    //        var context = new RouteContext(locationPath);
    //        Routes.Route(context);

    //        // Custom - Not valid route
    //        if (context.Handler == null || !LanguageService.HasValidLanguageInUrl(_baseUri, locationPath))
    //            context = SetErrorContext(locationPath);

    //        if (context.Handler != null)
    //        {
    //            if (!typeof(IComponent).IsAssignableFrom(context.Handler))
    //            {
    //                throw new InvalidOperationException($"The type {context.Handler.FullName} " +
    //                    $"does not implement {typeof(IComponent).FullName}.");
    //            }

    //            _renderHandle.Render(builder => Render(builder, context.Handler, context.Parameters));
    //        }
    //        else
    //        {
    //            if (!isNavigationIntercepted && NotFoundContent != null)
    //            {
    //                // We did not find a Component that matches the route.
    //                // Only show the NotFoundContent if the application developer programatically got us here i.e we did not
    //                // intercept the navigation. In all other cases, force a browser navigation since this could be non-Blazor content.
    //                _renderHandle.Render(NotFoundContent);
    //            }
    //            else
    //            {
    //                UriHelper.NavigateTo(_locationAbsolute, forceLoad: true);
    //            }
    //        }
    //    }

       
    //    private RouteContext SetErrorContext(string locationPath)
    //    {
    //        Language currentLanguage = LanguageService.GetLanguageFromUrl(_baseUri, locationPath);

    //        var errContext = new RouteContext($"{currentLanguage.TwoLetterCode}?{Constants.Route.RouteError}");
    //        Routes.Route(errContext);
    //        RouteContext context = errContext;
    //        return context;
    //    }


    //    private void OnLocationChanged(object sender, LocationChangedEventArgs args)
    //    {
    //        _locationAbsolute = args.Location;
    //        if (_renderHandle.IsInitialized && Routes != null)
    //        {
    //            Refresh(args.IsNavigationIntercepted);
    //        }
    //    }

    //    Task IHandleAfterRender.OnAfterRenderAsync()
    //    {
    //        if (!_navigationInterceptionEnabled && ComponentContext.IsConnected)
    //        {
    //            _navigationInterceptionEnabled = true;
    //            return NavigationInterception.EnableNavigationInterceptionAsync();
    //        }

    //        return Task.CompletedTask;
    //    }

    //    public void Attach(RenderHandle renderHandle)
    //    {
    //        _renderHandle = renderHandle;
    //        _baseUri = UriHelper.GetBaseUri();
    //        _locationAbsolute = UriHelper.GetAbsoluteUri();
    //        UriHelper.OnLocationChanged += OnLocationChanged;
    //    }


    //}
}
