using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ComponentLibrary.Demo.Extensions;

public static class TagHelperContextExtensions
{
    public static string GetExistingClasses(this TagHelperContext context)
    {
        return context.AllAttributes.TryGetAttribute("class", out var tagAttribute)
            ? tagAttribute.Value?.ToString() ?? string.Empty 
            : string.Empty;
    }
}