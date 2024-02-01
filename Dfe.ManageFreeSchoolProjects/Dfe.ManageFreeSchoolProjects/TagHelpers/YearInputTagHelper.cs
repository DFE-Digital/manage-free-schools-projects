using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;

namespace Dfe.ManageFreeSchoolProjects.TagHelpers;

 [HtmlTargetElement("govuk-year-input", TagStructure = TagStructure.WithoutEndTag)]
    
 public class YearInputTagHelper : InputTagHelperBase
    {
        public bool HeadingLabel { get; set; }

        private readonly ErrorService _errorService;

        public YearInputTagHelper(IHtmlHelper htmlHelper, ErrorService errorService) : base(htmlHelper)
        {
            _errorService = errorService;
        }

        protected override async Task<IHtmlContent> RenderContentAsync()
        {
            YearInputViewModel model = ValidateRequest();

            return await _htmlHelper.PartialAsync("_YearInput", model);
        }

        private YearInputViewModel ValidateRequest()
        {
            if (For.ModelExplorer.ModelType != typeof(string))
            {
                throw new ArgumentException("ModelType is not a string");
            }

            var year = For.Model as string;
            var model = new YearInputViewModel
            {
                Id = Id,
                Name = Name,
                Label = Label,
                HeadingLabel = HeadingLabel,
                Hint = Hint,
                Year = year
            };

           
            var error = _errorService.GetError(Name);
            if (error != null)
            {
                model.ErrorMessage = error.Message;
                model.YearInvalid = error.InvalidInputs.Contains($"{Name}-year");
                if (ViewContext.HttpContext.Request.Form.TryGetValue($"{Name}-year", out var dayValue))
                {
                    model.Year = dayValue;
                }
                
            }
			else
			{
                if (!string.IsNullOrEmpty(year) && Regex.Match(year, "20\\d\\d/\\d\\d", RegexOptions.None, TimeSpan.FromSeconds(5)).Success)
                {
                    model.Year = "20" + year.Substring(2, 2);
                }
			}

			return model;
        }
    }