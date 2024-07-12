using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Errors
{
	public class IndexModel : PageModel
	{
		private const string InternalErrorMessage = "Sorry, there is a problem with the system";
		private const string PageNotFoundMessage = "Page not found";
		private const string PageNotFoundPartial = "_PageNotFound";
		private const string InternalErrorPartial = "_ProblemWithTheSystem";

		public string ErrorMessage { get; private set; } = InternalErrorMessage;

		public string PartialViewName { get; private set; } = InternalErrorPartial;

		public void OnGet(int? statusCode = null)
		{
			ManageErrors(statusCode);
		}

		public void OnPost(int? statusCode = null)
		{
			ManageErrors(statusCode);
		}

		private void ManageErrors(int? statusCode)
		{
			if (!statusCode.HasValue)
			{
				ManageUnhandledErrors();
				return;
			}

			ErrorMessage = statusCode.Value switch
			{
				404 => PageNotFoundMessage,
				500 => InternalErrorMessage,
				_ => InternalErrorMessage
			};

			PartialViewName = statusCode.Value switch
			{
				404 => PageNotFoundPartial,
				500 => InternalErrorPartial,
				_ => InternalErrorPartial
			};
		}

		private void ManageUnhandledErrors()
		{
			var unhandledError = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

			// Thrown by RedirectToPage when the name of the page is incorrect.
			if (unhandledError is InvalidOperationException && unhandledError.Message.ToLower().Contains("no page named"))
			{
				ErrorMessage = PageNotFoundMessage;
				PartialViewName = PageNotFoundPartial;
				HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
			}
			else
			{
				ErrorMessage = InternalErrorMessage;
				PartialViewName = InternalErrorPartial;
				HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			}
		}
	}
}