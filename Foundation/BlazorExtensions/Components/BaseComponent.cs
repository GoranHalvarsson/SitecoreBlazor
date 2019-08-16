using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        [Parameter]
        public List<IBlazorItemField> FieldsModel { get; set; }
    }
}
