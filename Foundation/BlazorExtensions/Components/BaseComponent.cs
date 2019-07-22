using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        [Parameter]
        protected List<SitecoreBlazorHosted.Shared.Models.IBlazorSitecoreField> FieldsModel { get; set; }
    }
}
