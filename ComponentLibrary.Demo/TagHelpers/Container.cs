using ComponentLibrary.Demo.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TailwindMerge;

namespace ComponentLibrary.Demo.TagHelpers;


[HtmlTargetElement("ui-container")]
public class ContainerTagHelper : ComponentTagHelper
{
    private const string BaseClasses = "container";
    
    public ContainerTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClasses(Tailwind.Merge(BaseClasses, context.GetExistingClasses()));
    }
}