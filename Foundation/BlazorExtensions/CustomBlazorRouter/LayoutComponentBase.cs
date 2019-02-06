using Microsoft.AspNetCore.Components;

namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
    /// <summary>
    /// Optional base class for components that represent a layout.
    /// Alternatively, components may implement <see cref="IComponent"/> directly
    /// and declare their own parameter named <see cref="Body"/>.
    /// </summary>
    public abstract class LayoutComponentBase : ComponentBase
    {
        internal const string BodyPropertyName = nameof(Body);

        /// <summary>
        /// Gets the content to be rendered inside the layout.
        /// </summary>
        [Parameter]
        protected RenderFragment Body { get; private set; }
    }
}
