using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Factories
{
    public class ComponentFactory
    {
        private readonly FieldFactory _fieldFactory;

        public ComponentFactory(FieldFactory fieldFactory)
        {
            _fieldFactory = fieldFactory;
        }

        

        public RenderFragment? CreateComponent(Placeholder placeholderData)
        {

            if (placeholderData == null)
                return null;

            try
            {
                Type componentType = Type.GetType($"{placeholderData.ComponentName}, {placeholderData.Assembly}");

                IList<IBlazorItemField> componentModel = _fieldFactory.CreateBlazorItemFields(placeholderData.Fields);

                return BuildRenderTree =>
                {

                    BuildRenderTree.OpenComponent(0, componentType);

                    BuildRenderTree.AddAttribute(1, "FieldsModel", componentModel);

                    BuildRenderTree.CloseComponent();
                };

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error:" + ex.Message);
                Console.WriteLine("Name " + placeholderData?.Name);
                Console.WriteLine("Assembly " + placeholderData?.Assembly);
                Console.WriteLine("ComponentName " + placeholderData?.ComponentName);
                return null;
            }
           


        }



    }
}
