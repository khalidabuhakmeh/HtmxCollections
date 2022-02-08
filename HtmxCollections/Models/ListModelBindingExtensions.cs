using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HtmxCollections.Models;

public static class ListModelBindingExtensions
{
    static readonly Regex StripIndexerRegex = new(@"\[(?<index>\d+)\]", RegexOptions.Compiled);

    private static string GetIndexerFieldName(this TemplateInfo templateInfo) {
        var fieldName = templateInfo.GetFullHtmlFieldName("Index");
        fieldName = StripIndexerRegex.Replace(fieldName, string.Empty);
        if (fieldName.StartsWith(".")) {
            fieldName = fieldName.Substring(1);
        }
        return fieldName;
    }

    private static int GetIndex(this TemplateInfo templateInfo)
    {
        var fieldName = templateInfo.GetFullHtmlFieldName("Index");
        var match = StripIndexerRegex.Match(fieldName);
        return match.Success ? int.Parse(match.Groups["index"].Value) : 0;
    }

    public static HtmlString HiddenIndexerInputForModel<TModel>(this HtmlHelper<TModel> html) {
        var name = html.ViewData.TemplateInfo.GetIndexerFieldName();
        object value = html.ViewData.TemplateInfo.GetIndex();
        var markup = $@"<input type=""hidden"" name=""{name}"" value=""{value}"" />";
        return new HtmlString(markup);
    }
}