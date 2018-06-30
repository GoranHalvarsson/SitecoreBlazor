using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;

namespace Foundation.BlazorExtensions.Components
{
  public class RawHtml : BlazorComponent
  {
    [Parameter]
    string Value { get; set; }
    private ElementRef element;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
      //if (Value == null) return;
      builder.OpenElement(0, "rawHtml");
      builder.AddElementReferenceCapture(0, (elementReference) =>
      {
        this.element = elementReference;
      });
      builder.CloseElement();
    }


    protected override void OnAfterRender()
    {
      //if (Value == null)
      //    return;

      Microsoft.AspNetCore.Blazor.Browser.Interop.RegisteredFunction.Invoke<bool>("Foundation.BlazorExtensions.BlazorExtensionsInterop.RawHtml", element, Value);
    }
  }
}
