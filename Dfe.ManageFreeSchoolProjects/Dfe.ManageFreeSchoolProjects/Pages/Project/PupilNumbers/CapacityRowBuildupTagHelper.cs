﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    [HtmlTargetElement("govuk-capacity-buildup-row", TagStructure = TagStructure.WithoutEndTag)]
    public class CapacityRowBuildupTagHelper : TagHelper
    {
        [HtmlAttributeName("label")]
        public string Label { get; set; }

        [HtmlAttributeName("bold-label")]
        public bool BoldLabel { get; set; }

        [HtmlAttributeName("asp-for")]
        public CapacityBuildupEntry For { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlHelper _htmlHelper;

        public CapacityRowBuildupTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (_htmlHelper is IViewContextAware viewContextAware)
            {
                viewContextAware.Contextualize(ViewContext);
            }

            var model = new CapacityRowBuildupViewModel()
            {
                Label = Label,
                CapacityBuildupEntry = For,
                BoldLabel = BoldLabel
            };

            var content = await _htmlHelper.PartialAsync("_CapacityBuildupRow", model);

            output.TagName = null;
            output.PostContent.AppendHtml(content);
        }
    }

    public class CapacityRowBuildupViewModel
    {
        public string Label { get; set; }

        public bool BoldLabel { get; set; }

        public CapacityBuildupEntry CapacityBuildupEntry { get; set; }
    }
}
