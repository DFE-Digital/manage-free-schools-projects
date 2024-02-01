using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public interface ICookieCacheService<T>
    {
        public T Get();
        public void Delete();
        public void Update(T item);
    }

    public abstract class CookieCacheService<T> : ICookieCacheService<T> where T : new()
    {
        private const string CookieNamePrefix = ".ManageFreeSchoolProjects.PageCache.";
        private readonly string _key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _dataProtector;
        private T _item;

        protected CookieCacheService(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider DataProtectionProvider, string key)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtector = DataProtectionProvider.CreateProtector(nameof(CreateProjectCache));
            _key = CookieNamePrefix + key;
        }

        public T Get()
        {
            if (_item != null)
            {
                return _item;
            }

            string data = "";

            var counter = 0;
            while(counter < 100)
            {
                var chunk = _httpContextAccessor.HttpContext.Request.Cookies[_key + $".{counter}"];
                if(string.IsNullOrEmpty(chunk))
                {
                    break;
                }
                data += chunk;
                counter++;
            }

            if (string.IsNullOrEmpty(data))
            {
                return new T();
            }

            var result = JsonConvert.DeserializeObject<T>(_dataProtector.Unprotect(data.ToString()));

            if (result == null)
            {
                return new T();
            }

            _item = result;

            return result;
        }

        public void Delete()
        {
            foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
            {
                if (cookie.StartsWith(_key))
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
                }
            }
        }

        public void Update(T item)
        {
            _item = item;
            var json = JsonConvert.SerializeObject(item);

            CookieOptions options = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                Secure = true,
            };

            var data = _dataProtector.Protect(json);

            var chunks = StringChunker.Chunk(data, 3000);

            for ( int i = 0; i < chunks.Length; i++ ) { 
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_key + $".{i}", chunks[i], options);
            }
        }
    }
}
