using Microsoft.AspNetCore.Blazor;
using SitecoreBlazorHosted.Shared;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.JSInterop;

namespace Foundation.BlazorExtensions.Factories
{
  public class ComponentFactory
  {
    private IBlazorSitecoreField CreateSitecoreField<T>(KeyValuePair<string, BlazorField> sitecoreField) where T : class
    {
      if (sitecoreField.Value == null)
        return null;

      BlazorSitecoreField<T> field = null;

      T fieldValue = default;

      try
      {

        switch (sitecoreField.Value.Type)
        {
          case FieldTypes.HtmlField:
          case FieldTypes.PlainTextField:
            fieldValue = sitecoreField.Value.Value as T;
            break;

          default:
            fieldValue = Json.Deserialize<T>(sitecoreField.Value.Value.ToString());
            break;
        }



        field = new BlazorSitecoreField<T>
        {
          FieldName = sitecoreField.Key,
          Value = fieldValue,
          Editable = sitecoreField.Value?.Editable,
          Type = sitecoreField.Value?.Type
        };

      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error cretaing field {fieldValue.GetType()}. Error { ex.Message}");
      }

      return field;


    }


    private IBlazorSitecoreField CreateComplexSitecoreField<T>(KeyValuePair<string, BlazorField> field) where T : class
    {
      if (field.Value == null)
        return null;

      BlazorSitecoreField<T> sitecoreField = null;

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

              BlazorFieldValueMultiListItem multiListItem = Json.Deserialize<BlazorFieldValueMultiListItem>(item.ToString());

              var model = CreateComponentModel(multiListItem.Fields);

              if (!model.hasModel)
                continue;

              multiListItem.SitecoreFields = model.model;

              fieldValueMultiList.Values.Add(multiListItem);

            }


            fieldValue = fieldValueMultiList as T;


            break;

          default:
            break;
        }


        sitecoreField = new BlazorSitecoreField<T>
        {
          FieldName = field.Key,
          Value = fieldValue,
          Editable = field.Value?.Editable,
          Type = field.Value?.Type
        };


      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error cretaing complex field {fieldValue.GetType()}. Error { ex.Message}");

      }

      return sitecoreField;

    }
    private (List<IBlazorSitecoreField> model, bool hasModel) CreateComponentModel(Dictionary<string, BlazorField> fields)
    {
      if (fields == null || !fields.Any())
        return (null, false);


      List<IBlazorSitecoreField> list = new List<IBlazorSitecoreField>();


      foreach (KeyValuePair<string, BlazorField> field in fields)
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

      return (list, true);


    }



    public RenderFragment CreateComponent(Placeholder placeholderData)
    {

      if (placeholderData == null)
        return null;

      Type type = Type.GetType($"{placeholderData.ComponentName}, {placeholderData.Assembly}");

      System.Reflection.Assembly assembly = type.Assembly;

      (List<IBlazorSitecoreField> model, bool hasModel) componentModel = CreateComponentModel(placeholderData.Fields);

      return BuildRenderTree =>
      {
        BuildRenderTree.OpenComponent(0, assembly.GetType(placeholderData.ComponentName));

        if (componentModel.hasModel)
          BuildRenderTree.AddAttribute(1, "FieldsModel", componentModel.model);

        BuildRenderTree.CloseComponent();
      };


    }



  }
}
