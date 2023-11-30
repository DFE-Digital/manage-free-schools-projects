using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Models
{
    public class NumberRangeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Type modelType = ValidateBindingContext(bindingContext);

            var fromModelName = $"{bindingContext.ModelName}-from";
            var toModelName = $"{bindingContext.ModelName}-to";

            var fromProviderResult = bindingContext.ValueProvider.GetValue(fromModelName);
            var toProviderResult = bindingContext.ValueProvider.GetValue(toModelName);

            if (IsOptionalOrFieldTypeMismatch(bindingContext, fromProviderResult, toProviderResult))
            {
                if (modelType == typeof(string))
                {
                    if (IsEmpty(fromProviderResult, toProviderResult))
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

            if(bindingContext.ModelMetadata.IsRequired 
                && IsEmpty(fromProviderResult, toProviderResult))
            {
					bindingContext.Result = ModelBindingResult.Success("");
					return Task.CompletedTask;
			}

            if (FieldsAreValid(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult))
            {
                string fromResult = fromProviderResult.FirstValue;
                string toResult = toProviderResult.FirstValue;
                bindingContext.Result = ModelBindingResult.Success($"{fromResult}-{toResult}");
            }

            return Task.CompletedTask;
        }

        private static bool FieldsAreValid(ModelBindingContext bindingContext, string fromModelName, string toModelName, ValueProviderResult fromProviderResult, ValueProviderResult toProviderResult)
        {
            var displayName = bindingContext.ModelMetadata.DisplayName;

            string fromResult = fromProviderResult.FirstValue;
            string toResult = toProviderResult.FirstValue;

            if (fromResult.Length > 2)
            {
                string ErrorMessage = $"'{displayName} from' must be 2 characters or less.";
                AddError(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult, ErrorMessage);
                return false;
            }

            int from;

            if (!int.TryParse(fromResult, out from) || from < 0)
            {
                string ErrorMessage = $"Please enter a valid number";
                AddError(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult, ErrorMessage);
                return false;
            }

            if (toResult.Length > 2)
            {
                string ErrorMessage = $"'{displayName} to' must be 2 characters or less.";
                AddError(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult, ErrorMessage);
                return false;
            }

            int to;

            if (!int.TryParse(toResult, out to) || to < 0)
            {
                string ErrorMessage = $"Please enter a valid number";
                AddError(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult, ErrorMessage);
                return false;
            }

            if (from >= to)
            {
                var ErrorMessage = $"'{displayName} from' must be less than '{displayName} to'";
                AddError(bindingContext, fromModelName, toModelName, fromProviderResult, toProviderResult, ErrorMessage);
                return false;
            }

            return true;
        }

        private static void AddError(ModelBindingContext bindingContext, string fromModelName, string toModelName, ValueProviderResult fromProviderResult, ValueProviderResult toProviderResult, string ErrorMessage)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ErrorMessage);
            bindingContext.ModelState.SetModelValue(fromModelName, fromProviderResult);
            bindingContext.ModelState.SetModelValue(toModelName, toProviderResult);
            bindingContext.Result = ModelBindingResult.Failed();
        }

        private static Type ValidateBindingContext(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext, nameof(bindingContext));
            
            var modelType = bindingContext.ModelType;
            if (modelType != typeof(string))
            {
                throw new InvalidOperationException($"Cannot bind {modelType.Name}.");
            }

            return modelType;
        }

        private static bool IsEmpty(ValueProviderResult startValueProviderResult, ValueProviderResult endValueProviderResult)
        {
            return startValueProviderResult.FirstValue == string.Empty
                   && endValueProviderResult.FirstValue == string.Empty;
        }

        private static bool IsOptionalOrFieldTypeMismatch(ModelBindingContext bindingContext, ValueProviderResult startValueProviderResult, ValueProviderResult endValueProviderResult)
        {
            return string.IsNullOrWhiteSpace(startValueProviderResult.FirstValue)
                   && string.IsNullOrWhiteSpace(endValueProviderResult.FirstValue)
                   && !bindingContext.ModelMetadata.IsRequired
                   || startValueProviderResult == ValueProviderResult.None
                   && endValueProviderResult == ValueProviderResult.None;
        }
    }
}
