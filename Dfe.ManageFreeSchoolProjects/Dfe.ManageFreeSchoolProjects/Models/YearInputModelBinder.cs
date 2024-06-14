using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Models
{
	public class YearInputModelBinder : IModelBinder
	{
		private readonly ILoggerFactory _loggerFactory;

		public YearInputModelBinder(ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory;
		}

		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var modelName = bindingContext.ModelName;

			var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
			
				bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

				var value = valueProviderResult.FirstValue;

				bool isNumber = int.TryParse(value, out int intYear);
				
				
				if (!isNumber)
				{
					bindingContext.ModelState.AddModelError(
						modelName, "Enter a year in the correct format");
					return Task.CompletedTask;
				}
				
				if (intYear is < 2000 or > 2050)
				{
					bindingContext.ModelState.AddModelError(
						modelName, "Enter a year between 2000 and 2050");
					return Task.CompletedTask;
				}

				bindingContext.Result = ModelBindingResult.Success(value);
			return Task.CompletedTask;
		}
	}
}
