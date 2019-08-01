using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        [Parameter]
        protected List<IBlazorSitecoreField> FieldsModel { get; set; }
    }
}
