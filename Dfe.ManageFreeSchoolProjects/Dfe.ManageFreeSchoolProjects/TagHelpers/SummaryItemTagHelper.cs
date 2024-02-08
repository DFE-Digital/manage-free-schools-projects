﻿using Dfe.ManageFreeSchoolProjects.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers
{
    [HtmlTargetElement("govuk-summary-item", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SummaryItemTagHelper : TagHelper
    {
        const string empty = @"<span class=""empty"">Empty</span>";

        [HtmlAttributeName("label")]
        public string Label { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("href")]
        public string Href { get; set; }

        [HtmlAttributeName("render-link")]
        public bool RenderLink { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-summary-list__row");

            output.Content.SetHtmlContent(
               $@"<dt class=""govuk-summary-list__key"">
                    {Label}
               </dt>
               <dd class=""govuk-summary-list__value"" data-testid=""projectid"">
                    {RenderValue()}
               </dd>
               {GetChangeLink()}
            ");

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string RenderValue()
        {
            var value = GetValue();

            if(value != empty && RenderLink)
            {
                return $@"<a class=""govuk-link"" href=""{For.Model}"">{value}</a>";
            }

            return value;
        }

        private string GetValue()
        {
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
                return empty;
            }

            return value;
            //RenderLink
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
