using ComponentLibrary.Demo.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TailwindMerge;

namespace ComponentLibrary.Demo.TagHelpers;

[HtmlTargetElement("ui-input")]
public class InputTagHelper : ComponentTagHelper
{
    private const string BaseClasses = "flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50";

    public InputTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "input";
        output.TagMode = TagMode.SelfClosing;
        
        output.AddClasses(Tailwind.Merge(BaseClasses, context.GetExistingClasses()));
    }
}