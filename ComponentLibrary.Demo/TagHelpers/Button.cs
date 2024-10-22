using ComponentLibrary.Demo.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TailwindMerge;

namespace ComponentLibrary.Demo.TagHelpers;

[HtmlTargetElement("ui-button")]
public class ButtonTagHelper : ComponentTagHelper
{
    [HtmlAttributeName("size")]
    public ButtonSize Size { get; set; } = ButtonSize.Small;
    
    [HtmlAttributeName("variant")]
    public ButtonVariant Variant { get; set; } = ButtonVariant.Secondary;

    [HtmlAttributeName("tag")]
    public string Tag { get; set; } = "button";
    
    private const string BaseClasses =
        "button ring-offset-background focus-visible:ring-ring inline-flex items-center justify-center whitespace-nowrap rounded-full font-medium transition-colors duration-400 ease-in-out focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50";

    public ButtonTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = output.Attributes.ContainsName("href") ? "a" : Tag;

        output.AddClasses(Tailwind.Merge(
            BaseClasses, 
            ButtonSizeUtils.GetButtonSizeClasses(Size),
            ButtonVariantUtils.GetButtonVariantClasses(Variant),
            context.GetExistingClasses()));
        
        if(!context.AllAttributes.ContainsName("type") && !context.AllAttributes.ContainsName("href"))
            output.Attributes.Add("type", "button");
    }
}

public enum ButtonSize
{
    Small,
    Medium,
    Large,
    Icon,
}

public enum ButtonVariant
{
    Primary,
    Secondary,
    Outline,
    Ghost,
    Destructive,
    Link
}

public static class ButtonSizeUtils
{
    public static ButtonSize Parse(string? value, ButtonSize defaultValue = ButtonSize.Small)
    {
        return value switch
        {
            "Small" => ButtonSize.Small,
            "Medium" => ButtonSize.Medium,
            "Large" => ButtonSize.Large,
            "Icon" => ButtonSize.Icon,
            _ => defaultValue
        };
    }
    
    public static string GetButtonSizeClasses(ButtonSize size)
    {
        return size switch
        {
            ButtonSize.Small => "h-9 rounded-md px-3",
            ButtonSize.Medium => "h-10 px-4 py-2",
            ButtonSize.Large => "h-11 rounded-md px-8",
            ButtonSize.Icon => "h-10 w-10",
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }
}

public static class ButtonVariantUtils
{

    public static ButtonVariant Parse(string? value, ButtonVariant defaultValue = ButtonVariant.Secondary)
    {
        return value switch
        {
            "Primary" => ButtonVariant.Primary,
            "Secondary" => ButtonVariant.Secondary,
            "Outline" => ButtonVariant.Outline,
            "Ghost" => ButtonVariant.Ghost,
            "Destructive" => ButtonVariant.Destructive,
            "Link" => ButtonVariant.Link,
            _ => defaultValue
        };
    }
    
    public static string GetButtonVariantClasses(ButtonVariant variant)
    {
        return variant switch
        {
            ButtonVariant.Primary => "button--primary bg-primary text-primary-foreground hover:bg-primary/90",
            ButtonVariant.Secondary => "button--secondary bg-secondary text-secondary-foreground hover:bg-secondary/80",
            ButtonVariant.Outline => "button--outline border-input bg-background hover:bg-accent hover:text-accent-foreground border",
            ButtonVariant.Destructive => "button--destructive bg-destructive text-destructive-foreground hover:bg-destructive/90",
            ButtonVariant.Link => "button--link text-primary underline-offset-4 py-0 px-0 text-sm h-auto hover:underline focus:underline",
            ButtonVariant.Ghost => "button--ghost hover:bg-accent hover:text-accent-foreground",
            _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
        };
    }
}