using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Models
{
    public class YesNoToBoolBinder : IModelBinder
    {
        private readonly ILoggerFactory _loggerFactory;

        public YesNoToBoolBinder(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            //var modelType = bindingContext.ModelType;
            //if (modelType != typeof(bool) || modelType != typeof(bool?))
            //{
            //    throw new InvalidOperationException($"Cannot bind {modelType.Name}.");
            //}

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
