using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Models
{
    public class YesNoToBoolBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value.FirstValue == null)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value.FirstValue == "Yes");
            return Task.CompletedTask;

        }
    }
}
