using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-summary-list", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SummaryListTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "dl";
            output.Attributes.SetAttribute("class", "govuk-summary-list");
        }
    }
}
