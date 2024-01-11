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
	public class Cookies(ILogger<Cookies> logger, IOptions<ServiceLinkOptions> options,
			IConfiguration configuration)
		: PageModel
	{
		private readonly string cookieDomain = configuration["Google:CookieDomain"];
		
		private const string ConsentCookieName = ".ManageFreeSchoolProjects.Consent";
		public bool? Consent { get; set; }
		public bool PreferencesSet { get; set; } = false;
		public string returnPath { get; set; }

		public string TransfersCookiesUrl { get; set; }

		public ActionResult OnGet(bool? consent, string returnUrl)
		{
			returnPath = returnUrl;
			TransfersCookiesUrl = $"{options.Value.TransfersUrl}/cookie-preferences?returnUrl=%2Fhome";

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

			if (consent != null && !consent.Value)
			{
				foreach (var cookie in Request.Cookies.Keys)
				{
					if (cookie.StartsWith("_ga") || cookie.StartsWith("_ga_"))
					{
						var cookieOptions =new CookieOptions
						{
							Expires = DateTime.Now.AddDays(-1),
							Domain = cookieDomain,
							Path = "/",
							Secure = true
						};
						logger.LogInformation("Deleting Google analytics cookie: {cookie}", cookie);
						Response.Cookies.Delete(cookie,cookieOptions);
						
						var gaCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key.StartsWith("_ga"));
						if (gaCookie.Key != null)
							Response.Cookies.Delete(gaCookie.Key,cookieOptions);
						
						var gatCookie = Request.Cookies.Keys.FirstOrDefault(key => key.StartsWith("_ga_"));
						if (!string.IsNullOrEmpty(gatCookie))
							Response.Cookies.Delete(gatCookie, cookieOptions);
					}
				}
			}
			TempData["cookiePreferenceChanged"] = true;
		}
	}
}