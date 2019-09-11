using Microsoft.AspNetCore.Components;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Foundation.BlazorExtensions.Factories
{
    public class FieldFactory
    {
        private IBlazorItemField CreateBlazorItemField<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField)
        {
            if (blazorRouteField.Value == null)
                return null;

            BlazorItemField<T> field = null;

            T fieldValue = default;

          
            try
            {

                switch (blazorRouteField.Value.Type)
                {
                    case FieldTypes.HtmlField:
                    case FieldTypes.PlainTextField:
                    case FieldTypes.CheckboxField:
                        fieldValue = (T)Convert.ChangeType(blazorRouteField.Value.Value.ToString(), typeof(T));
                        break;
                    default:
                        fieldValue = JsonSerializer.Deserialize<T>(blazorRouteField.Value.Value.ToString());
                        break;
                }



                field = new BlazorItemField<T>
                {
                    FieldName = blazorRouteField.Key,
                    Value = fieldValue,
                    Editable = blazorRouteField.Value?.Editable,
                    Type = blazorRouteField.Value?.Type
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating field {blazorRouteField.Value.Value}. Error { ex.Message}");
            }

            return field;


        }


        private Dictionary<string, BlazorRouteField> CreateDefaultBlazorRouteField()
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

        private IBlazorItemField CreateComplexBlazorItemField<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField) where T : class
        {
            if (blazorRouteField.Value == null)
                return null;

            BlazorItemField<T> blazorItemField = null;

            T fieldValue = default;

            try
            {
                switch (blazorRouteField.Value.Type)
                {
                    case FieldTypes.MultiListField:

                        BlazorFieldValueMultiList fieldValueMultiList = new BlazorFieldValueMultiList()
                        {
                            Values = new List<BlazorFieldValueMultiListItem>()
                        };


                        foreach (var item in blazorRouteField.Value.Values)
                        {
                            if (item == null || string.IsNullOrWhiteSpace(item.ToString()))
                                continue;

                            BlazorFieldValueMultiListItem multiListItem = new BlazorFieldValueMultiListItem() {
                                Id = item.Id,
                                Url = item.Url
                            };


                            var fields = CreateBlazorItemFields(item.Fields);

                            multiListItem.BlazorItemFields = fields;

                            fieldValueMultiList.Values.Add(multiListItem);

                        }


                        fieldValue = fieldValueMultiList as T;
                        break;

                    default:
                        break;
                }


                blazorItemField = new BlazorItemField<T>
                {
                    FieldName = blazorRouteField.Key,
                    Value = fieldValue,
                    Editable = blazorRouteField.Value?.Editable,
                    Type = blazorRouteField.Value?.Type
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating complex field {fieldValue?.GetType()}. Error { ex.Message}");

            }

            return blazorItemField;

        }
        
        public List<IBlazorItemField> CreateBlazorItemFields(Dictionary<string, BlazorRouteField> blazorRouteFields)
        {
            if (blazorRouteFields == null || !blazorRouteFields.Any())
                blazorRouteFields = CreateDefaultBlazorRouteField();


            List<IBlazorItemField> list = new List<IBlazorItemField>();


            foreach (KeyValuePair<string, BlazorRouteField> field in blazorRouteFields)
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
                        list.Add(CreateBlazorItemField<string>(field));
                        break;
                    case FieldTypes.CheckboxField:
                        list.Add(CreateBlazorItemField<bool>(field));
                        break;

                    case FieldTypes.LinkField:
                        list.Add(CreateBlazorItemField<BlazorFieldValueLink>(field));
                        break;

                    case FieldTypes.ImageField:
                        list.Add(CreateBlazorItemField<BlazorFieldValueImage>(field));
                        break;

                    case FieldTypes.MultiListField:
                        list.Add(CreateComplexBlazorItemField<BlazorFieldValueMultiList>(field));
                        break;

                    default:
                        break;
                }

            }

            return list;


        }


    }
}
