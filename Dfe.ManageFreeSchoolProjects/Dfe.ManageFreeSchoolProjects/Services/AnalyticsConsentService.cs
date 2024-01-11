using Microsoft.AspNetCore.Http;
using System;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public interface IAnalyticsConsentService
    {
        void AllowConsent();
        bool? ConsentValue();
        void DenyConsent();
        bool HasConsent();
    }

    public class AnalyticsConsentService : IAnalyticsConsentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ConsentCookieName = ".ManageFreeSchoolProjects.Consent";
        private bool? Consent { get; set; }
        public AnalyticsConsentService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool? ConsentValue()
        {
            if (Consent.HasValue)
            {
                return Consent;
            }

            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(ConsentCookieName))
            {
                return bool.Parse(_httpContextAccessor.HttpContext.Request.Cookies[ConsentCookieName]);
            }

            return false;
        }

        public bool HasConsent()
        {
            return ConsentValue() ?? false;
        }

        public void AllowConsent()
        {
            SetConsent(true);
        }

        public void DenyConsent()
        {
            SetConsent(false);
        }

        private void SetConsent(bool consent)
        {
            Consent = consent;
            var cookieOptions = new CookieOptions { Expires = DateTime.Today.AddMonths(6), Secure = true, HttpOnly = true };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(ConsentCookieName, consent.ToString(), cookieOptions);

            if(!consent)
            {
                foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
                {
                    if (cookie.StartsWith("_ga") || cookie.Equals("_gid"))
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
                    }
                }

            }
        }
    }

}