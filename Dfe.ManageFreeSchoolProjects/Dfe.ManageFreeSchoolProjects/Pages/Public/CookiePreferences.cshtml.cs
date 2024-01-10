using Dfe.ManageFreeSchoolProjects.Configuration;
using Dfe.ManageFreeSchoolProjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Dfe.ManageFreeSchoolProjects.Pages.Public
{
	public class CookiePreferences : PageModel
	{
		private readonly string cookieDomain;
		
		private const string ConsentCookieName = ".ManageFreeSchoolProjects.Consent";
		public bool? Consent { get; set; }
		public bool PreferencesSet { get; set; } = false;
		public string returnPath { get; set; }
		
		private readonly ILogger<CookiePreferences> _logger;
		private readonly IOptions<ServiceLinkOptions> _options;

		public CookiePreferences(ILogger<CookiePreferences> logger, IOptions<ServiceLinkOptions> options)
		{
			_logger = logger;
			_options = options;
		}

		public string TransfersCookiesUrl { get; set; }

		public ActionResult OnGet(bool? consent, string returnUrl)
		{
			returnPath = returnUrl;
			TransfersCookiesUrl = $"{_options.Value.TransfersUrl}/cookie-preferences?returnUrl=%2Fhome";

			if (Request.Cookies.ContainsKey(ConsentCookieName))
			{
				Consent = bool.Parse(Request.Cookies[ConsentCookieName]);
			}

			if (consent.HasValue)
			{
				PreferencesSet = true;

				ApplyCookieConsent(consent);

				if (!string.IsNullOrEmpty(returnUrl))
				{
					return Redirect(returnUrl);
				}

				return RedirectToPage(Links.Public.CookiePreferences);
			}

			return Page();
		}

		public IActionResult OnPost(bool? consent, string returnUrl)
		{
			returnPath = returnUrl;

			if (Request.Cookies.ContainsKey(ConsentCookieName))
			{
				Consent = bool.Parse(Request.Cookies[ConsentCookieName]);
			}

			if (consent.HasValue)
			{
				Consent = consent;
				PreferencesSet = true;

				var cookieOptions = new CookieOptions { Expires = DateTime.Today.AddMonths(6), Secure = true, HttpOnly = true };
				Response.Cookies.Append(ConsentCookieName, consent.Value.ToString(), cookieOptions);

				if (!consent.Value)
				{
					ApplyCookieConsent(consent);
					
				}
				return Page();
			}

			return Page();
		}

		private void ApplyCookieConsent(bool? consent)
		{
			if (consent.HasValue)
			{
				var cookieOptions = new CookieOptions { Expires = DateTime.Today.AddMonths(6), Secure = true, HttpOnly = true };
				Response.Cookies.Append(ConsentCookieName, consent.Value.ToString(), cookieOptions);
			}

			if (!consent.Value)
			{
				foreach (var cookie in Request.Cookies.Keys)
				{
					if (cookie.StartsWith("_ga") || cookie.Equals("_ga_"))
					{
						_logger.LogInformation("Expiring Google analytics cookie: {cookie}", cookie);
						Response.Cookies.Delete(cookie, new CookieOptions { Domain = "", Path = "/" });

						var gaCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key.StartsWith("_ga"));
						if (gaCookie.Key != null)
							Response.Cookies.Delete(gaCookie.Key,
								new CookieOptions
								{
									Expires = DateTime.Now.AddHours(-120),
									Path = "/",
									HttpOnly = true,
									Secure = this.Request.IsHttps,
									IsEssential = true
								});
					}
				}
			}
			TempData["cookiePreferenceChanged"] = true;
		}
	}
}