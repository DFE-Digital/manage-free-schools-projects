using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dfe.ManageFreeSchoolProjects.Models;

public class YearInputModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        Type modelType = ValidateBindingContext(bindingContext);

        var YearModelName = $"{bindingContext.ModelName}-year";
        
        var ValueProviderResult = bindingContext.ValueProvider.GetValue(YearModelName);
        
        if (IsOptionalOrFieldTypeMismatch(bindingContext, ValueProviderResult))
        {
            if (modelType == typeof(string))
            {
                if (IsEmpty(ValueProviderResult))
                {
                    bindingContext.Result = ModelBindingResult.Success("");
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                }
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
        
        var displayName = bindingContext.ModelMetadata.DisplayName;

        IDateValidationMessageProvider page =
            (bindingContext.ActionContext as Microsoft.AspNetCore.Mvc.RazorPages.PageContext)?.ViewData.Model as IDateValidationMessageProvider;

        string startYear = ValueProviderResult.FirstValue;
        
        if (!ValidateYearFormat(startYear))
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Date should be in the format: 20XX");
            bindingContext.ModelState.SetModelValue(YearModelName, ValueProviderResult);
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
        
        int start = int.Parse(startYear);
        
        bindingContext.Result = ModelBindingResult.Success($"20{startYear.Substring(2, 2)}");

        return Task.CompletedTask;
    }
    
    private static bool ValidateYearFormat(string year)
    {
        return year.Length == 4 && Regex.Match(year, "20\\d\\d", RegexOptions.None, TimeSpan.FromSeconds(5)).Success;
    }
  
    private static Type ValidateBindingContext(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelType = bindingContext.ModelType;
        if (modelType != typeof(string))
        {
            throw new InvalidOperationException($"Cannot bind {modelType.Name}.");
        }

        return modelType;
    }
    
    private static bool IsEmpty(ValueProviderResult startValueProviderResult)
    {
        return startValueProviderResult.FirstValue == string.Empty;

    }
    
    private static bool IsOptionalOrFieldTypeMismatch(ModelBindingContext bindingContext, ValueProviderResult ValueProviderResult)
    {
        return string.IsNullOrWhiteSpace(ValueProviderResult.FirstValue)
               && !bindingContext.ModelMetadata.IsRequired
               || ValueProviderResult == ValueProviderResult.None;
    }
}