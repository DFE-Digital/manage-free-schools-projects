using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-radios-list")]
    public class RadiosListTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "fieldset";
            output.Attributes.SetAttribute("class", "govuk-fieldset");

            output.PreContent.SetHtmlContent(
               $@"<div class=""govuk-radios"" data-module=""govuk-radios"">");

            output.PostContent.SetHtmlContent(
                "</div>"
            );

            output.TagMode = TagMode.StartTagAndEndTag;
        }

    }
}
