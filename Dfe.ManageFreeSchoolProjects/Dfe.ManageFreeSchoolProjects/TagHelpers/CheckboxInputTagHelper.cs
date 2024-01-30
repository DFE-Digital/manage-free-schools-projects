using Dfe.ManageFreeSchoolProjects.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-checkbox-input", TagStructure = TagStructure.WithoutEndTag)]
    public class CheckboxInputTagHelper : InputTagHelperBase
    {
        [HtmlAttributeName("heading-label")]
        public string HeadingLabel { get; set; }

        public CheckboxInputTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        protected override async Task<IHtmlContent> RenderContentAsync()
        {
            CheckboxInputViewModel model = new() { Id = Id, Name = Name, Label = Label, HeadingLabel = HeadingLabel, Value = For.Model?.ToString() };

            return await _htmlHelper.PartialAsync("_CheckboxInput", model);
        }
    }
}
