using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var typeModel = helper.ViewData.ModelMetadata.ModelType;
        var htmlFields = typeModel.GetProperties().Select(p => p.ConvertPropertyToHtml(helper.ViewData.Model!));
        IHtmlContentBuilder result = new HtmlContentBuilder();
        return htmlFields.Aggregate(result, (current, content) => current.AppendHtml(content));
    }

    private static IHtmlContent ConvertPropertyToHtml(this PropertyInfo propertyInfo, object model)
    {
        var div = new TagBuilder("div");
        div.Attributes.Add("class", "mb-3");
        
        div.InnerHtml.AppendHtml(CreatePropertyLabel(propertyInfo));
        div.InnerHtml.AppendHtml(CreatePropertyInput(propertyInfo, model));
        div.InnerHtml.AppendHtml(CreatePropertyValidationSign(propertyInfo, model)!);
        return div;
    }

    private static string GetPropertyName(MemberInfo propertyInfo) =>
        propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ??
        propertyInfo.Name.SplitByCamelCase();

    private static string SplitByCamelCase(this string s) =>
        Regex.Replace(s, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

    private static IHtmlContent CreatePropertyLabel(MemberInfo propertyInfo)
    {
        var label = new TagBuilder("label");
        label.Attributes.Add("class", "form-label");
        label.Attributes.Add("for", propertyInfo.Name);

        label.InnerHtml.AppendHtmlLine(GetPropertyName(propertyInfo));
        return label;
    }

    private static IHtmlContent CreatePropertyInput(this PropertyInfo propertyInfo, object model) =>
        propertyInfo.PropertyType.IsEnum
            ? CreateSelect(propertyInfo, model)
            : CreateInput(propertyInfo, model);

    private static IHtmlContent CreateInput(PropertyInfo propertyInfo, object? model)
    {
        var input = new TagBuilder("input");
        input.Attributes.Add("class", "form-control");
        input.Attributes.Add("name", propertyInfo.Name);
        input.Attributes.Add("id", propertyInfo.Name);
        input.Attributes.Add("type", propertyInfo.PropertyType == typeof(int) ? "number" : "text");
        input.Attributes.Add("value", model is not null ? propertyInfo.GetValue(model)?.ToString() ?? "" : "");

        return input;
    }
    
    private static IHtmlContent CreateSelect(PropertyInfo propertyInfo, object? model)
    {
        var select = new TagBuilder("select");
        select.Attributes.Add("class", "form-control");
        select.Attributes.Add("name", propertyInfo.Name);

        var modelValue = model is not null ? propertyInfo.GetValue(model) : 0;
        var enumItems = propertyInfo.PropertyType
            .GetFields(BindingFlags.Public | BindingFlags.Static);
        
        foreach (var enumItem in enumItems)
        {
            var option = CreateSelectOption(enumItem, modelValue);
            select.InnerHtml.AppendHtml(option);
        }

        return select;
    }

    private static IHtmlContent CreateSelectOption(FieldInfo enumItem, object? modelValue)
    {
        var enumType = enumItem.DeclaringType;
        var option = new TagBuilder("option");
        option.Attributes.Add("value", enumItem.Name);

        if (enumItem.GetValue(enumType)!.Equals(modelValue))
            option.MergeAttribute("selected", "true");
        
        option.InnerHtml.AppendHtmlLine(GetPropertyName(enumItem));
        return option;
    }

    private static IHtmlContent? CreatePropertyValidationSign(PropertyInfo propertyInfo, object? model)
    {
        if (model == null) 
            return null;

        var results = new List<ValidationResult>();
        var context = new ValidationContext(model)
        {
            DisplayName = GetPropertyName(propertyInfo),
            MemberName = propertyInfo.Name
        };

        if (Validator.TryValidateProperty(propertyInfo.GetValue(model), context, results) || results.Count == 0)
            return null;

        return CreateValidationSpan(results[0].ErrorMessage!, GetPropertyName(propertyInfo));
    }

    private static IHtmlContent? CreateValidationSpan(string errorMessage, string propertyName)
    {
        var span = new TagBuilder("span");
        span.MergeAttribute("class", "text-danger col-form-label");
        span.MergeAttribute("data-for", propertyName);
        span.InnerHtml.SetContent(errorMessage);
        return span;
    }
} 