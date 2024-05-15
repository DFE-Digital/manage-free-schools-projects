using Dfe.ManageFreeSchoolProjects.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-summary-list-item", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SummaryItemListTagHelper : TagHelper

    {
        private readonly IHtmlHelper _htmlHelper;
        
        const string empty = @"<span class=""empty"">Empty</span>";

        [HtmlAttributeName("label")]
        public string Label { get; set; }
        
        [HtmlAttributeName("id")]
        public string Id { get; set; }
        
        [HtmlAttributeName("list")]
        public List<string> List { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("href")]
        public string Href { get; set; }

        [HtmlAttributeName("render-link")]
        public bool RenderLink { get; set; }
        
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        public SummaryItemListTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_htmlHelper is IViewContextAware viewContextAware)
            {
                viewContextAware.Contextualize(ViewContext);
            }

            var model = new SummaryItemListViewModel()
            {
                Href = Href,
                Id = Id,
                Label = Label,
                List = List
            };
            
           

            var content = await _htmlHelper.PartialAsync("_SummaryList", model);

            output.TagName = null;
            output.PostContent.AppendHtml(content);
           
        }

      
    }
    
    public class SummaryItemListViewModel
    {
        public string Id { get; set; }
        
        public string Href { get; set; }
        public string Label { get; set; }
        public List<string> List { get; set; }
    }
}
