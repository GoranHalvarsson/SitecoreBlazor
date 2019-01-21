using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Foundation.BlazorExtensions.CustomBlazorRouter;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.AspNetCore.Blazor.Services;

namespace Foundation.BlazorExtensions.DynamicRouter
{
    public class DynamicRouter : IComponent, IDisposable
    {
        private static readonly char[] _queryOrHashStartChar = new char[2]
        {
      '?',
      '#'
        };
        private RenderHandle _renderHandle;
        private string _baseUri;
        private string _locationAbsolute;

        [Inject]
        private IUriHelper UriHelper { get; set; }

        /// <summary>
        /// Gets or sets the assembly that should be searched, along with its referenced
        /// assemblies, for components matching the URI.
        /// </summary>
        [Parameter]
        private Assembly AppAssembly { get; set; }

        [Parameter] private RouterDataRoot RouteValues { get; set; }

        private RouteTable Routes { get; set; }

        /// <inheritdoc />
        public void Init(RenderHandle renderHandle)
        {
            this._renderHandle = renderHandle;
            this._baseUri = this.UriHelper.GetBaseUri();
            this._locationAbsolute = this.UriHelper.GetAbsoluteUri();
            this.UriHelper.OnLocationChanged += new EventHandler<string>(this.OnLocationChanged);
        }

        /// <inheritdoc />
        public void SetParameters(ParameterCollection parameters)
        {
            parameters.AssignToProperties((object)this);
            //this.Routes = RouteTable.Create(ComponentResolver.ResolveComponents(this.AppAssembly));
            Routes = RouteTable.CreateNew(RouteValues);
            this.Refresh();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.UriHelper.OnLocationChanged -= new EventHandler<string>(this.OnLocationChanged);
        }

        private string StringUntilAny(string str, char[] chars)
        {
            int length = str.IndexOfAny(chars);
            if (length >= 0)
                return str.Substring(0, length);
            return str;
        }

        /// <inheritdoc />
        protected virtual void Render(RenderTreeBuilder builder, Type handler, IDictionary<string, object> parameters)
        {
            builder.OpenComponent(0, typeof(LayoutDisplay));
            builder.AddAttribute(1, "Page", (object)handler);
            builder.AddAttribute(2, "PageParameters", (object)parameters);
            builder.CloseComponent();
        }

        private void Refresh()
        {
            string path = this.StringUntilAny(this.UriHelper.ToBaseRelativePath(this._baseUri, this._locationAbsolute), DynamicRouter._queryOrHashStartChar);

            if (string.IsNullOrWhiteSpace(path))
                path = "en";
            
            RouteContext context = new RouteContext(path);
            this.Routes.Route(context);
            if (context.Handler == (Type)null)
                throw new InvalidOperationException("'Router' cannot find any component with a route for '/" + path + "'.");
            if (!typeof(IComponent).IsAssignableFrom(context.Handler))
                throw new InvalidOperationException("The type " + context.Handler.FullName + " does not implement " + typeof(IComponent).FullName + ".");
            this._renderHandle.Render((RenderFragment)(builder => this.Render(builder, context.Handler, context.Parameters)));
        }

        private void OnLocationChanged(object sender, string newAbsoluteUri)
        {
            this._locationAbsolute = newAbsoluteUri;
            if (!this._renderHandle.IsInitialized)
                return;
            this.Refresh();
        }
    }
}
