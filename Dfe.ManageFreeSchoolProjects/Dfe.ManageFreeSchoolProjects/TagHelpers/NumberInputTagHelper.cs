using Dfe.ManageFreeSchoolProjects.ViewModels;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-number-input", TagStructure = TagStructure.WithoutEndTag)]
    public class NumberInputTagHelper : InputTagHelperBase
    {
        public NumberInputTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        protected override async Task<IHtmlContent> RenderContentAsync()
        {
            var model = new NumberInputViewModel
            {
                Id = Id,
                TestId = TestId,
                Name = Name,
                Value = For.Model?.ToString(),
            };

            if (ViewContext.ModelState.TryGetValue(Name, out var entry) && entry.Errors.Count > 0)
            {
                model.ErrorMessage = entry.Errors[0].ErrorMessage;
            }

            return await _htmlHelper.PartialAsync("_NumberInput", model);
        }
    }

    public class NumberInputViewModel
    {
        public string Id { get; set; }
        public string TestId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}
