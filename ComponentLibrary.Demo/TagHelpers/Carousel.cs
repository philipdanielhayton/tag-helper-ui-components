using System.Text.Json;
using System.Text.Json.Serialization;
using ComponentLibrary.Demo.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TailwindMerge;

namespace ComponentLibrary.Demo.TagHelpers;

#region Components
[HtmlTargetElement("ui-carousel")]
public class CarouselRootTagHelper : ComponentTagHelper
{
    #region Alpine Settings
    
    [HtmlAttributeName("id")]
    public string? Id { get; set; }
    
    #endregion
    #region Core Options
    [HtmlAttributeName("align")]
    public string? Align { get; set; }
    
    [HtmlAttributeName("axis")]
    public string? Axis { get; set; }
    
    [HtmlAttributeName("contain-scroll")]
    public object? ContainScroll { get; set; }
    
    [HtmlAttributeName("drag-free")]
    public bool DragFree { get; set; } = false;
    
    [HtmlAttributeName("drag-threshold")]
    public int? DragThreshold { get; set; }
    
    [HtmlAttributeName("duration")]
    public int? Duration { get; set; }
    
    [HtmlAttributeName("in-view-threshold")]
    public int? InViewThreshold { get; set; }
    
    [HtmlAttributeName("loop")]
    public bool Loop { get; set; } = false;
    
    [HtmlAttributeName("skip-snaps")]
    public bool SkipSnaps { get; set; } = false;
    
    [HtmlAttributeName("slides-to-scroll")]
    public int? SlidesToScroll { get; set; }
    
    [HtmlAttributeName("start-index")]
    public int? StartIndex { get; set; }
    
    [HtmlAttributeName("watch-drag")]
    public bool? WatchDrag { get; set; }
    
    [HtmlAttributeName("watch-resize")]
    public bool? WatchResize { get; set; }
    
    [HtmlAttributeName("watch-slides")]
    public bool? WatchSlides { get; set; }
    #endregion
    #region Fade Options
    [HtmlAttributeName("fade")]
    public bool Fade { get; set; } = false;
    #endregion
    #region AutoPlay Options
    [HtmlAttributeName("autoplay")]
    public bool AutoPlay { get; set; } = false;
    
    [HtmlAttributeName("autoplay-delay")]
    public int AutoPlayDelay { get; set; } = 4000;
    
    [HtmlAttributeName("autoplay-jump")]
    public bool AutoPlayJump { get; set; } = false;
    
    [HtmlAttributeName("autoplay-on-init")]
    public bool AutoPlayOnInit { get; set; } = true;
    
    [HtmlAttributeName("autoplay-stop-on-interaction")]
    public bool AutoPlayStopOnInteraction { get; set; } = true;
    
    [HtmlAttributeName("autoplay-stop-on-mouse-enter")]
    public bool AutoPlayStopOnMouseEnter { get; set; } = false;
    
    [HtmlAttributeName("autoplay-stop-on-focus-in")]
    public bool AutoPlayStopOnFocusIn { get; set; } = true;
    
    [HtmlAttributeName("autoplay-stop-on-last-snap")]
    public bool AutoPlayStopOnLastSnap { get; set; } = false;
    #endregion
    #region ClassName Options
    [HtmlAttributeName("class-name-snapped")]
    public string? ClassNameSnapped { get; set; }
    
    [HtmlAttributeName("class-name-in-view")]
    public string? ClassNameInView { get; set; }
    
    [HtmlAttributeName("class-name-draggable")]
    public string? ClassNameDraggable { get; set; }
    
    [HtmlAttributeName("class-name-dragging")]
    public string? ClassNameDragging { get; set; }
    #endregion
    #region AutoHeight Options
    [HtmlAttributeName("auto-height")]
    public bool AutoHeight { get; set; } = false;
    #endregion
    
    private const string BaseClasses = "overflow-hidden";

    public CarouselRootTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        var options = GetOptions();
        var jsonOptions = JsonSerializer.Serialize(options, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        
        output.Attributes.Add("x-data", $"carousel({jsonOptions})");
        output.Attributes.Add("role", "region");
        output.Attributes.Add("aria-roledescription", "carousel");
        
        var mergedClasses = Tailwind.Merge(BaseClasses, context.GetExistingClasses());
        output.AddClasses(mergedClasses);
    }
    
    
    private CarouselOptions GetOptions()
    {
        return new CarouselOptions
        {
            Id = Id,
            Options = new EmblaOptions
            {
                Align = Align,
                Axis = Axis,
                ContainScroll = ContainScroll,
                DragFree = DragFree,
                DragThreshold = DragThreshold,
                Duration = Duration,
                InViewThreshold = InViewThreshold,
                Loop = Loop,
                SkipSnaps = SkipSnaps,
                SlidesToScroll = SlidesToScroll,
                StartIndex = StartIndex,
                WatchDrag = WatchDrag,
                WatchResize = WatchResize,
                WatchSlides = WatchSlides
            },
            
            AutoPlay = new EmblaAutoPlayPluginOptions
            {
                Active = AutoPlay,
                Delay = AutoPlayDelay,
                Jump = AutoPlayJump,
                OnInit = AutoPlayOnInit,
                StopOnInteraction = AutoPlayStopOnInteraction,
                StopOnMouseEnter = AutoPlayStopOnMouseEnter,
                StopOnFocusIn = AutoPlayStopOnFocusIn,
                StopOnLastSnap = AutoPlayStopOnLastSnap,
            },
            
            ClassNames = new EmblaClassNamesPluginOptions
            {
                Active = new [] { ClassNameSnapped, ClassNameInView, ClassNameDraggable, ClassNameDragging }.Any(x => !string.IsNullOrEmpty(x)),
                Snapped = ClassNameSnapped,
                InView = ClassNameInView,
                Draggable = ClassNameDraggable, 
                Dragging = ClassNameDragging
            },
            
            Fade = new EmblaFadePluginOptions
            {
                Active = Fade
            },
            
            AutoHeight = new EmblaAutoHeightPluginOptions
            {
                Active = AutoHeight
            }
        };
    }
}

[HtmlTargetElement("ui-carousel-item")]
public class CarouselItemTagHelper : ComponentTagHelper
{
    private const string BaseClasses = "min-w-0 shrink-0 grow-0 basis-full";

    public CarouselItemTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.Attributes.Add("role", "group");
        output.Attributes.Add("aria-roledescription", "slide");
        
        var mergedClasses = Tailwind.Merge(BaseClasses, context.GetExistingClasses());
        output.AddClasses(mergedClasses);
    }
}

[HtmlTargetElement("ui-carousel-container")]
public class CarouselContainerTagHelper : ComponentTagHelper
{
    private const string BaseClasses = "flex";

    public CarouselContainerTagHelper(TwMerge twMerge) : base(twMerge)
    {
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";

        var mergedClasses = Tailwind.Merge(BaseClasses, context.GetExistingClasses());
        output.AddClasses(mergedClasses);
        output.Attributes.Add("x-ref", "container");
    }

}

#endregion
#region Directives

/// <summary>
/// Dispatches a goto event globally. Use this to control a carousel from outside
/// of it's containing element, by ID.
/// </summary>
[HtmlTargetElement(Attributes = "onclick:dispatch:carousel:goto")]
public class CarouselOnClickDispatchGotoTagHelper : TagHelper
{
    [HtmlAttributeName("onclick:dispatch:carousel:goto")]
    public CarouselGotoEvent EventDetails { get; set; } = new(string.Empty, default);
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var jsonEventDetail = JsonSerializer.Serialize(EventDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        
        output.Attributes.Add("x-on:click", $"$dispatch('carousel:goto', {jsonEventDetail})");
    }
}

[HtmlTargetElement(Attributes = "onclick:carousel:next")]
public class CarouselOnClickScrollNextTagHelper : TagHelper
{
    [HtmlAttributeName("onclick:carousel:next")]
    public bool ScrollNext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("x-on:click", "carousel.scrollNext()");
    }
}

[HtmlTargetElement(Attributes = "onclick:carousel:prev")]
public class CarouselOnClickScrollPrevTagHelper : TagHelper
{
    [HtmlAttributeName("onclick:carousel:prev")]
    public bool ScrollPrev { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("x-on:click", "carousel.scrollPrev()");
    }
}

[HtmlTargetElement(Attributes = "onclick:carousel:goto")]
public class CarouselOnClickScrollToTagHelper : TagHelper
{
    [HtmlAttributeName("onclick:carousel:goto")]
    public int Position { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("x-on:click", $"carousel.scrollTo({Position})");
    }
}

[HtmlTargetElement(Attributes = "onkeyup:carousel:next")]
public class CarouselOnKeyupScrollNextTagHelper : TagHelper
{
    [HtmlAttributeName("onkeyup:carousel:scroll-next")]
    public bool ScrollNext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("x-on:keyup.right.window", "carousel.scrollNext()");
    }
}

[HtmlTargetElement(Attributes = "onkeyup:carousel:prev")]
public class CarouselOnKeyupPrevNextTagHelper : TagHelper
{
    [HtmlAttributeName("onkeyup:carousel:prev")]
    public bool ScrollPrev { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("x-on:keyup.left.window", "carousel.scrollPrev()");
    }
}

#endregion
#region Events
public record CarouselGotoEvent(string Id, int Index, bool? Jump = false);
#endregion
#region Config
public sealed record CarouselOptions
{
    public string? Id { get; set; }
    public required EmblaOptions Options { get; set; }
    public required EmblaAutoPlayPluginOptions AutoPlay { get; set; }
    public required EmblaClassNamesPluginOptions ClassNames { get; set; }
    public required EmblaFadePluginOptions Fade { get; set; }
    public required EmblaAutoHeightPluginOptions AutoHeight { get; set; }
}

public sealed record EmblaAutoHeightPluginOptions : EmblaPluginOptions;

public sealed record EmblaAutoPlayPluginOptions : EmblaPluginOptions
{
    public int Delay { get; set; } = 4000;
    public bool Jump { get; set; } = false;
    public bool OnInit { get; set; } = true;
    public bool StopOnInteraction { get; set; } = true;
    public bool StopOnMouseEnter { get; set; } = false;
    public bool StopOnFocusIn { get; set; } = true;
    public bool StopOnLastSnap { get; set; } = false;
}

public sealed record EmblaClassNamesPluginOptions : EmblaPluginOptions
{
    public string? Snapped { get; set; }
    public string? InView { get; set; }
    public string? Draggable { get; set; }
    public string? Dragging { get; set; }
}

public sealed record EmblaFadePluginOptions : EmblaPluginOptions;

public sealed record EmblaOptions
{
    public bool? Active { get; set; }
    public string? Align { get; set; }
    public string? Axis { get; set; }
    public object? Breakpoints { get; set; }
    public object? ContainScroll { get; set; }
    public bool? DragFree { get; set; }
    public int? DragThreshold { get; set; }
    public int? Duration { get; set; }
    public int? InViewThreshold { get; set; }
    public bool? Loop { get; set; }
    public bool? SkipSnaps { get; set; }
    public int? SlidesToScroll { get; set; }
    public int? StartIndex { get; set; }
    public object? WatchDrag { get; set; }
    public object? WatchResize { get; set; }
    public object? WatchSlides { get; set; }
}

public abstract record EmblaPluginOptions
{
    public bool Active { get; set; } = false;
}
#endregion