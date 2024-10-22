using Microsoft.AspNetCore.Razor.TagHelpers;
using TailwindMerge;

namespace ComponentLibrary.Demo.TagHelpers;

public abstract class ComponentTagHelper : TagHelper
{
    protected readonly TwMerge Tailwind;
    
    public ComponentTagHelper(TwMerge twMerge)
    {
        Tailwind = twMerge;
    }
}