using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-radios-list-item", TagStructure = TagStructure.WithoutEndTag)]
    public class RadiosListItemTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("value")]
        public string Value { get; set; }

        [HtmlAttributeName("description")]
        public string Description { get; set; }

        [HtmlAttributeName("hint")]
        public string Hint { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-radios__item");
            
            output.PreContent.SetHtmlContent(RadiosListItemBuilder.BuildRadioInput(Value, For));
            output.PreContent.AppendHtml(RadiosListItemBuilder.BuildLabel(Value, Description));
            output.PreContent.AppendHtml(RadiosListItemBuilder.BuildHint(Value, Hint));

            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
