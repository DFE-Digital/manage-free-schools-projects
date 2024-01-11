using Dfe.ManageFreeSchoolProjects.Configuration;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Dfe.ManageFreeSchoolProjects.Pages.Public
{
	public class CookiePreferences : PageModel
	{
		private const string ConsentCookieName = ".ManageFreeSchoolProjects.Consent";
		public bool? Consent { get; set; }
		public bool PreferencesSet { get; set; } = false;
		public string returnPath { get; set; }
		private readonly ILogger<CookiePreferences> _logger;
		private readonly IOptions<ServiceLinkOptions> _options;
		private readonly IAnalyticsConsentService _analyticsConsentService;

		public CookiePreferences(ILogger<CookiePreferences> logger, IOptions<ServiceLinkOptions> options, IAnalyticsConsentService analyticsConsentService)
		{
			_logger = logger;
			_options = options;
			_analyticsConsentService = analyticsConsentService;
		}

		public string TransfersCookiesUrl { get; set; }

		public ActionResult OnGet(bool? consent, string returnUrl)
		{
			returnPath = returnUrl;
			TransfersCookiesUrl = $"{_options.Value.TransfersUrl}/cookie-preferences?returnUrl=%2Fhome";

			Consent = _analyticsConsentService.ConsentValue();

            if (consent.HasValue)
			{
				PreferencesSet = true;

				ApplyCookieConsent(consent.Value);

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

            Consent = _analyticsConsentService.ConsentValue();

            if (consent.HasValue)
			{
				Consent = consent;
				PreferencesSet = true;

				ApplyCookieConsent(consent.Value);
				return Page();
			}

			return Page();
		}

		private void ApplyCookieConsent(bool consent)
		{
			if (consent) { 
				_analyticsConsentService.AllowConsent();
			}
			else
			{
				_analyticsConsentService.DenyConsent();
			}
		}
	}
}