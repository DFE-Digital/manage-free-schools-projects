using Dfe.ManageFreeSchoolProjects.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-summary-item", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SummaryItemTagHelper : TagHelper
    {
        [HtmlAttributeName("label")]
        public string Label { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("href")]
        public string Href { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-summary-list__row");

            output.Content.SetHtmlContent(
               $@"<dt class=""govuk-summary-list__key"">
                    {Label}
               </dt>
               <dd class=""govuk-summary-list__value"" data-testid=""projectid"">
                    {GetValue()}
               </dd>
               {GetChangeLink()}
            ");

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetValue()
        {
            const string empty = @"<span class=""empty"">Empty</span>";

            if(For.Model == null)
            {
                return empty;
            }

            if (For.ModelExplorer.ModelType == typeof(bool))
            {
                return ((bool)For.Model).ToYesNoString();
            }

            if (For.ModelExplorer.ModelType == typeof(bool?))
            {
                if(For.Model == null)
                {
                    return empty;
                }
                return ((bool)For.Model).ToYesNoString();
            }

            if (For.ModelExplorer.ModelType == typeof(DateTime))
            {
                return ((DateTime)For.Model).ToDateString();
            }

            if (For.ModelExplorer.ModelType == typeof(DateTime?))
            {
                if (For.Model == null)
                {
                    return empty;
                }
                return ((DateTime)For.Model).ToDateString();
            }

            if (For.ModelExplorer.ModelType.IsEnum)
            {
                var enumDescription = For.ModelExplorer.Model.ToDescription();
                
                if (enumDescription == "NotSet")
                {
                    return empty;
                }
            }

            if (Nullable.GetUnderlyingType(For.ModelExplorer.ModelType)?.IsEnum == true)
            {
                if (For.Model == null)
                {
                    return empty;
                }

                return For.Model.ToDescription();
            }
            
            var value = For.Model.ToString();

            if (string.IsNullOrEmpty(value) || value == "NotSet")
            {
                return @"<span class=""empty"">Empty</span>";
            }

            return value;

        }

        private string GetChangeLink()
        {
            if (string.IsNullOrEmpty(Href))
            {
                return string.Empty;
            }

            return $@"<dd class=""govuk-summary-list__actions"">
                        <a class=""govuk-link"" href={Href}>
                            Change<span class=""govuk-visually-hidden"">{Label}</span>
                        </a>                   
                     </dd>";
        }
    }
}
