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
        private IBlazorItemField? CreateBlazorItemField<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField)
        {
            if (blazorRouteField.Value == null)
                return null;


            return new BlazorItemField<T>
            {
                FieldName = blazorRouteField.Key,
                Value = GetFieldValue<T>(blazorRouteField),
                Editable = blazorRouteField.Value?.Editable,
                Type = blazorRouteField.Value?.Type
            };

        }

        private static T GetFieldValue<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField)
        {
            try
            {
                var fieldValue = blazorRouteField.Value.Type switch
                {
                    FieldTypes.HtmlField => (T)Convert.ChangeType(blazorRouteField.Value.Value.ToString(), typeof(T)),
                    FieldTypes.PlainTextField => (T)Convert.ChangeType(blazorRouteField.Value.Value.ToString(), typeof(T)),
                    FieldTypes.CheckboxField => (T)Convert.ChangeType(blazorRouteField.Value.Value.ToString(), typeof(T)),
                    _ => JsonSerializer.Deserialize<T>(blazorRouteField.Value.Value.ToString())
                };

                return fieldValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing/serialize field {blazorRouteField.Value.Value}. Error { ex.Message}");
            }

            return default;
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

        private IBlazorItemField? CreateComplexBlazorItemField<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField) where T : class
        {
            if (blazorRouteField.Value == null)
                return null;

           

            return new BlazorItemField<T>
            {
                FieldName = blazorRouteField.Key,
                Value = GetComplexFieldValue<T>(blazorRouteField),
                Editable = blazorRouteField.Value?.Editable,
                Type = blazorRouteField.Value?.Type
            };


        }

        private T GetComplexFieldValue<T>(KeyValuePair<string, BlazorRouteField> blazorRouteField) where T : class
        {


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

                            BlazorFieldValueMultiListItem multiListItem = new BlazorFieldValueMultiListItem()
                            {
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

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating complex field {fieldValue?.GetType()}. Error { ex.Message}");

            }

            return fieldValue;


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
