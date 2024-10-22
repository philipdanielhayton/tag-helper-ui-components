using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ComponentLibrary.Demo.Extensions;

public static class TagHelperOutputExtensions
{
    public static void AddClasses(this TagHelperOutput output, IEnumerable<string> values, HtmlEncoder? encoder = null)
    {
        foreach (var value in values)
        {
            output.AddClass(value, encoder ?? HtmlEncoder.Default);
        }
    }

    public static void AddClasses(this TagHelperOutput output, string? values, string delimiter = " ", HtmlEncoder? encoder = null)
    {
        if (values is null)
            return;
        
        var entries = values.Split(delimiter);

        foreach (var item in entries)
        {
            output.AddClass(item, encoder ?? HtmlEncoder.Default);
        }
    }
    
    public static void SetClasses(this TagHelperOutput output, string? values, string delimiter = " ", HtmlEncoder? encoder = null)
    {
        if (values is null)
            return;
        
        var entries = values.Split(delimiter);
        output.Attributes.RemoveAll("class");

        foreach (var item in entries)
        {
            output.AddClass(item, encoder ?? HtmlEncoder.Default);
        }
    }
}