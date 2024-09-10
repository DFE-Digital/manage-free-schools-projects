using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection.Emit;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-summary-card", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SummaryCardTagHelper: TagHelper
    {
        [HtmlAttributeName("label")]
        public string Label { get; set; }

        [HtmlAttributeName("id")]

        public string Id { get; set; }

        [HtmlAttributeName("href")]

        public string href { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-summary-card");
            output.Attributes.SetAttribute("id", Id);

            output.PreContent.SetHtmlContent(
               $@"<div class=""govuk-summary-card__title-wrapper"">
                    <h2 class=""govuk-summary-card__title"">{Label}</h2>
                        <div class=""govuk-summary-card__actions"">
                            {GetChangeLink()}
                        </div>
                    </div>
                  <div class=""govuk-summary-card__content"">
            ");

            output.PostContent.SetHtmlContent(
                "</div>"
            );

                 

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetChangeLink()
        {
            if (href != null)
                return $@"<a class=""govuk-link"" href=""{href}"" data-testid=""change-{Id}"">
                                Change<span class=""govuk-visually-hidden"">{Label}</span>
                      </a>
            ";

            return string.Empty;
        }

    }
}
