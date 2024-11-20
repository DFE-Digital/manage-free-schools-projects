using Dfe.ManageFreeSchoolProjects.API.Contracts.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DfE.CoreLibs.Security.Interfaces;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public abstract class ApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ApiClient> _logger;
        private string _httpClientName;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICorrelationContext _correlationContext;
        private readonly IClientUserInfoService _userInfoService;
        private readonly IUserTokenService _apiTokenService;


        protected ApiClient(
            IHttpClientFactory clientFactory,
            ILogger<ApiClient> logger,
            IHttpContextAccessor httpContextAccessor,
            string httpClientName,
            ICorrelationContext correlationContext,
            IClientUserInfoService clientUserInfoService,
            IUserTokenService apiTokenService
            )
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClientName = httpClientName;
            _correlationContext = correlationContext;
            _userInfoService = clientUserInfoService;
            _apiTokenService = apiTokenService;
        }

        public async Task<T> Get<T>(string endpoint) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                var client = CreateHttpClient();

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Stream> GetStream(string endpoint)
        {
            try 
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                var client = CreateHttpClient();

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var fileStream = await response.Content.ReadAsStreamAsync();

                return fileStream;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<TResult> Post<T, TResult>(string endpoint, T dto)
        {
            try
            {
                var request = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                var client = CreateHttpClient();

                var response = await client.PostAsync(endpoint, request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<TResult> Patch<T, TResult>(string endpoint, T dto)
        {
            try
            {
                var request = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                var client = CreateHttpClient();

                var response = await client.PatchAsync(endpoint, request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<TResult> Put<T, TResult>(string endpoint, T dto)
        {
            try
            {
                var request = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                var client = CreateHttpClient();

                var response = await client.PutAsync(endpoint, request);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<TResult> Delete<T, TResult>(string endpoint, T dto)
        {
            try
            {
                var request = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                var client = CreateHttpClient();

                var response = await client.DeleteAsync($"{endpoint}/{dto}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = _clientFactory.CreateClient(_httpClientName);

            var apiToken = _apiTokenService.GetUserTokenAsync(_httpContextAccessor.HttpContext.User).Result;

            //client.DefaultRequestHeaders.Add(HttpHeaderConstants.UserContextName, _httpContextAccessor.HttpContext.User?.Identity?.Name);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            AddDefaultRequestHeaders(client, _correlationContext, _userInfoService, _logger);
            return client;
        }

        public static void AddDefaultRequestHeaders(HttpClient httpClient, ICorrelationContext correlationContext, IClientUserInfoService userInfoService, ILogger<ApiClient> logger)
        {

            var headerAdded = httpClient.DefaultRequestHeaders.TryAddWithoutValidation(correlationContext.HeaderKey, correlationContext.CorrelationId);
            if (!headerAdded)
            {
                logger?.LogWarning("Warning. Unable to add correlationId to request headers");
            }

            var userInfoHeadersAdded = userInfoService.AddUserInfoRequestHeaders(httpClient);

            if (!userInfoHeadersAdded)
            {
                logger?.LogWarning("Warning. Attempt to call api without user info headers");
            }

        }
    }
}
