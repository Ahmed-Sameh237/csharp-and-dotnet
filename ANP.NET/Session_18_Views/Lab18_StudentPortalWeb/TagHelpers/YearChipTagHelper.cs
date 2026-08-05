using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

// ==========================
//          PART D
// ==========================

namespace StudentPortalWeb.TagHelpers
{
    [HtmlTargetElement("year-chip",TagStructure = TagStructure.WithoutEndTag)]
    public class YearChipTagHelper: TagHelper
    {
        public int For {  get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int CHIP_YEAR = 1;
            string CHIP_LABEL = "Year 1";
            string cssClass;
            string label;
            if(For == CHIP_YEAR)
            {
                cssClass = "bg-warning text-dark";
                label = CHIP_LABEL;
            }
            else
            {
                cssClass = "bg-light text-dark";
                label="Year " + For.ToString();
            }
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            
            output.Attributes.SetAttribute("class",$"badge {cssClass}");
            output.Attributes.SetAttribute("title", "rendered by ahmed");
            output.Content.SetContent($"{For} - {label}");
        }
    }
}
