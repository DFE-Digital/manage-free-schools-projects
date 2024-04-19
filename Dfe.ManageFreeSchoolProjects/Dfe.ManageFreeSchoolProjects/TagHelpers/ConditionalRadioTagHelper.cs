using Dfe.ManageFreeSchoolProjects.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-conditional-radio", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ConditionalRadioTagHelper : TagHelper
    {
        [HtmlAttributeName("label")]
        public string Label { get; set; }

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-form-group");

            string value = GetValue();

            output.PreContent.SetHtmlContent(GetPreContent(value));

            output.PostContent.SetHtmlContent(GetPostContent(value));

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetValue()
        {
            if (For.Model == null)
            {
                return null;
            }

            if (For.ModelExplorer.ModelType == typeof(bool) || For.ModelExplorer.ModelType == typeof(bool?))
            {
                return ((bool)For.Model).ToYesNoString();
            }

            return For.Model?.ToString();
        }

        private string GetPreContent(string value)
        {
            string checkedTemplate = "checked=\"checked\"";
            string showChecked = value == "Yes" ? checkedTemplate : string.Empty;

            string output = $@"<fieldset class=""govuk-fieldset"" id=""{Id}"">
                    <legend class=""govuk-fieldset__legend govuk-fieldset__legend--m"">
                        {Label}
                    </legend>
                    <div class=""govuk-radios"" data-module=""govuk-radios"">
                        <div class=""govuk-radios__item"">
                            <input 
                                class=""govuk-radios__input"" 
                                id=""{Id}-Yes"" 
                                name=""{Id}"" 
                                type=""radio"" 
                                value=""Yes"" 
                                data-testid=""{Id}-Yes""
                                {showChecked}
                                data-aria-controls=""conditional-{Id}-Yes"">
            
                            <label class=""govuk-label govuk-radios__label"" for=""{Id}-Yes"">
                                Yes
                            </label>
                        </div>
                        <div class=""govuk-radios__conditional"" id=""conditional-{Id}-Yes"">";

            return output;
        }

        private string GetPostContent(string value)
        {
            string checkedTemplate = "checked=\"checked\"";
            string showChecked = value == "No" ? checkedTemplate : string.Empty;

            string output = $@"</div>
                        <div class=""govuk-radios__item"">
                            <input 
                                class=""govuk-radios__input"" 
                                id=""{Id}-No""
                                name=""{Id}""
                                type=""radio"" 
                                aria-label=""{Id}-No""
                                value=""No""
                                {showChecked}
                                data-testid=""{Id}-No"">
            
                            <label class=""govuk-label govuk-radios__label"" for=""{Id}-No"">
                                No
                            </label>
                        </div>
                    </div>
                </fieldset>";

            return output;
        }
    }
}
