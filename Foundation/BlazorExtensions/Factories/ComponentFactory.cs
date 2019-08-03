using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Foundation.BlazorExtensions.Factories
{
    public class ComponentFactory
    {
        private IBlazorItemField CreateSitecoreField<T>(KeyValuePair<string, BlazorRouteField> sitecoreField)
        {
            if (sitecoreField.Value == null)
                return null;

            BlazorItemField<T> field = null;

            T fieldValue = default;

          
            try
            {

                switch (sitecoreField.Value.Type)
                {
                    case FieldTypes.HtmlField:
                    case FieldTypes.PlainTextField:
                    case FieldTypes.CheckboxField:
                        fieldValue = (T)Convert.ChangeType(sitecoreField.Value.Value.ToString(), typeof(T));
                        break;
                    default:
                        fieldValue = JsonSerializer.Deserialize<T>(sitecoreField.Value.Value.ToString());
                        break;
                }



                field = new BlazorItemField<T>
                {
                    FieldName = sitecoreField.Key,
                    Value = fieldValue,
                    Editable = sitecoreField.Value?.Editable,
                    Type = sitecoreField.Value?.Type
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating field {sitecoreField.Value.Value}. Error { ex.Message}");
            }

            return field;


        }


        private Dictionary<string, BlazorRouteField> CreateDefaultField()
        {
            SitecoreBlazorHosted.Shared.Models.BlazorRouteField defaultField = new BlazorRouteField()
            {
                Editable = null,
                Value = "",
                Type = FieldTypes.PlainTextField
            };

            return new Dictionary<string, BlazorRouteField>()
            {
               { "DefaultField",defaultField }
            };
            
        }

        private IBlazorItemField CreateComplexSitecoreField<T>(KeyValuePair<string, BlazorRouteField> field) where T : class
        {
            if (field.Value == null)
                return null;

            BlazorItemField<T> sitecoreField = null;

            T fieldValue = default;

            try
            {
                switch (field.Value.Type)
                {
                    case FieldTypes.MultiListField:

                        BlazorFieldValueMultiList fieldValueMultiList = new BlazorFieldValueMultiList()
                        {
                            Values = new List<BlazorFieldValueMultiListItem>()
                        };


                        foreach (var item in field.Value.Values)
                        {
                            if (item == null || string.IsNullOrWhiteSpace(item.ToString()))
                                continue;

                            BlazorFieldValueMultiListItem multiListItem = new BlazorFieldValueMultiListItem() {
                                Id = item.Id,
                                Url = item.Url
                            };



                            var model = CreateComponentModel(item.Fields);

                            //if (!model.hasModel)
                            //    continue;

                            multiListItem.SitecoreFields = model;

                            fieldValueMultiList.Values.Add(multiListItem);

                        }


                        fieldValue = fieldValueMultiList as T;


                        break;

                    default:
                        break;
                }


                sitecoreField = new BlazorItemField<T>
                {
                    FieldName = field.Key,
                    Value = fieldValue,
                    Editable = field.Value?.Editable,
                    Type = field.Value?.Type
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating complex field {fieldValue?.GetType()}. Error { ex.Message}");

            }

            return sitecoreField;

        }
        public List<IBlazorItemField> CreateComponentModel(Dictionary<string, BlazorRouteField> fields)
        {
            if (fields == null || !fields.Any())
                fields = CreateDefaultField();


            List<IBlazorItemField> list = new List<IBlazorItemField>();


            foreach (KeyValuePair<string, BlazorRouteField> field in fields)
            {
                if (field.Value == null)
                    continue;

                string fieldType = field.Value.Type;

                if (string.IsNullOrWhiteSpace(fieldType))
                    continue;

                switch (fieldType)
                {
                    case FieldTypes.HtmlField:
                    case FieldTypes.PlainTextField:
                        list.Add(CreateSitecoreField<string>(field));
                        break;
                    case FieldTypes.CheckboxField:
                        list.Add(CreateSitecoreField<bool>(field));
                        break;

                    case FieldTypes.LinkField:
                        list.Add(CreateSitecoreField<BlazorFieldValueLink>(field));
                        break;

                    case FieldTypes.ImageField:
                        list.Add(CreateSitecoreField<BlazorFieldValueImage>(field));
                        break;

                    case FieldTypes.MultiListField:
                        list.Add(CreateComplexSitecoreField<BlazorFieldValueMultiList>(field));
                        break;

                    default:
                        break;
                }

            }

            return list;


        }



        public RenderFragment CreateComponent(Placeholder placeholderData)
        {

            if (placeholderData == null)
                return null;

            try
            {
                Type componentType = Type.GetType($"{placeholderData.ComponentName}, {placeholderData.Assembly}");

                IList<IBlazorItemField> componentModel = CreateComponentModel(placeholderData.Fields);


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
