using AccessTokenMvcWebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TheIdentityHub;

namespace AccessTokenMvcWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult CallAccessTokenMvcApiService()
        {
            try
            {
                var access_token_cookie = this.Request.Cookies["access_token_cookie"];

                if (access_token_cookie == null)
                {
                    return View("CallServices", new CallServiceViewModel { Result = "Cookie was null." });
                }

                var accessToken = access_token_cookie.Value;

                if (string.IsNullOrEmpty(accessToken))
                {
                    // Need to get accesstoken first.
                    return CallAccessTokenMvcApiServicePost();
                }

                using (var httpClient = CreateHttpClient(accessToken))
                {
                    var result = httpClient.GetStringAsync(new Uri("https://localhost:44335/api/values")).Result;
                    return View("CallServices", new CallServiceViewModel { Result = result });
                }
            }
            catch (Exception ex)
            {
                return View("CallServices", new CallServiceViewModel { Result = ex.Message + ex.InnerException?.Message });
            }
        }

        [HttpGet]
        public ActionResult CallAccessTokenMvcApiServiceParseToken()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("CallAccessTokenMvcApiService")]
        public ActionResult CallAccessTokenMvcApiServicePost()
        {
            var redirectUrl = CreateAccessTokenImplicitGrantUrl(this.Request);

            return new RedirectResult(redirectUrl);
        }

        [HttpPost]
        public async Task<ActionResult> CallAppTokenMvcApiService()
        {
            try
            {
                using (var httpClient = await CreateHttpClientForApp("ReadProfile gr").ConfigureAwait(continueOnCapturedContext: false))
                {
                    var result = httpClient.GetStringAsync(new Uri("https://localhost:44329/api/values")).Result;
                    return View("CallServices", new CallServiceViewModel { Result = result });
                }
            }
            catch (Exception ex)
            {
                return View("CallServices", new CallServiceViewModel { Result = ex.Message + ex.InnerException?.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CallAppTokenWcfService()
        {
            try
            {
                var appToken = await GetAppToken(string.Empty).ConfigureAwait(continueOnCapturedContext: false);

                using (var service = new AppTokenWcfServiceServiceServiceReference.AppTokenWcfServiceServiceClient())
                {
                    using (new OperationContextScope(service.InnerChannel))
                    {
                        var requestMessage = new HttpRequestMessageProperty();
                        requestMessage.Headers["Authorization"] = "Bearer " + appToken.AccessToken;
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                        var data = service.GetDataAsync(1).Result;

                        return View("CallServices", new CallServiceViewModel { Result = data });
                    }
                }
            }
            catch (Exception ex)
            {
                return View("CallServices", new CallServiceViewModel { Result = ex.Message + ex.InnerException?.Message });
            }
        }

        [HttpPost]
        public ActionResult CallBasicAuthMvcWebApiService(string username, string password)
        {
            using (var httpClient = CreateHttpClient(username, password))
            {
                try
                {
                    var result = httpClient.GetStringAsync(new Uri("https://localhost:44325/api/values")).Result;
                    return View("CallServices", new CallServiceViewModel { Result = result });
                }
                catch (Exception ex)
                {
                    return View("CallServices", new CallServiceViewModel { Result = ex.Message + ex.InnerException?.Message });
                }
            }
        }

        [HttpPost]
        public ActionResult CallBasicAuthWcfService(string username, string password)
        {
            try
            {
                using (var service = new BasicAuthWcfServiceServiceServiceReference.BasicAuthWcfServiceServiceClient())
                {
                    service.ClientCredentials.UserName.UserName = username;
                    service.ClientCredentials.UserName.Password = password;
                    var data = service.GetDataAsync(1).Result;

                    return View("CallServices", new CallServiceViewModel { Result = data });
                }
            }
            catch (Exception ex)
            {
                return View("CallServices", new CallServiceViewModel { Result = ex.Message + ex.InnerException?.Message });
            }
        }

        [HttpGet]
        public ActionResult CallServices()
        {
            return View();
        }

        [Authorize]
        public ActionResult IndexAuthentication()
        {
            ViewBag.Title = "Home Page Authentification";

            if (this.User.IsInRole("Domain Users"))
            {
                // Do some stuff.
            }

            return View();
        }

        public ActionResult IndexIdentification()
        {
            ViewBag.Title = "Home Page Identification";
            return View();
        }

        public ActionResult SignIn()
        {
            HubAuthenticationModule.SignIn();
            return new EmptyResult();
        }

        public ActionResult SignOut()
        {
            HubAuthenticationModule.SignOut();

            return new RedirectResult("IndexIdentification");
        }

        /// <summary>
        /// Gets the configured value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The configured value.
        /// </returns>
        internal static string GetConfiguredValue(string name, string defaultValue = null)
        {
            var value = WebConfigurationManager.AppSettings[name];

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        private static string CreateAccessTokenImplicitGrantUrl(HttpRequestBase request)
        {
            var returnUrl = new Uri(GetConfiguredValue("redirectUri"));
            returnUrl = new Uri(returnUrl, "Home/CallAccessTokenMvcApiServiceParseToken");
            var clientIdAndRedirectUri = "&client_id=" + GetConfiguredValue("accessTokenMvcApiServiceClientId") + "&redirect_uri=" + Uri.EscapeDataString(returnUrl.ToString()) + "&state=" + Uri.EscapeDataString(request.Url.AbsoluteUri);

            const string scopes = "";

            if (!string.IsNullOrEmpty(scopes))
            {
                clientIdAndRedirectUri += "&scope=" + Uri.EscapeDataString(scopes);
            }

            var requestTokenUrl = new Uri(new Uri(GetConfiguredValue("baseUrl")), "oauth2/v1/auth?response_type=token" + clientIdAndRedirectUri);

            return requestTokenUrl.AbsoluteUri;
        }

        private static HttpClient CreateHttpClient(string username, string password)
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return client;
        }

        private static HttpClient CreateHttpClient(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }

        private static async Task<HttpClient> CreateHttpClientForApp(string scopes)
        {
            var appToken = await GetAppToken(scopes).ConfigureAwait(continueOnCapturedContext: false);

            return CreateHttpClient(appToken.AccessToken);
        }

        private static async Task<TokenResponse> GetAppToken(string scopes)
        {
            var baseUri = new Uri(GetConfiguredValueAsUrl("baseUrl"));

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = new KeyValuePair<string, string>[]
                {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", scopes),
                new KeyValuePair<string, string>("client_id", GetConfiguredValue("clientId")),
                new KeyValuePair<string, string>("client_secret", GetConfiguredValue("clientSecret"))
                };

                var response = await client.PostAsync(new Uri(baseUri, "oauth2/v1/token"), new FormUrlEncodedContent(parameters)).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    var tokenString = await response.Content.ReadAsStringAsync();

                    return new TokenResponse(tokenString);
                }
                else
                {
                    return null;
                }
            }
        }

        private static string GetConfiguredValueAsUrl(string name)
        {
            var url = GetConfiguredValue(name);
            if (url != null && !url.EndsWith("/"))
            {
                url += "/";
            }
            return url;
        }
    }

    [DataContract]
    internal sealed class Token
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }

    internal sealed class TokenResponse
    {
        private readonly string accessToken;

        public TokenResponse(string response)
        {
            var responseStream = new MemoryStream(Encoding.UTF8.GetBytes(response));
            var ser = new DataContractJsonSerializer(typeof(Token));

            responseStream.Position = 0;
            var token = (Token)ser.ReadObject(responseStream);

            this.accessToken = token.AccessToken;
        }

        public string AccessToken
        {
            get { return this.accessToken; }
        }
    }
}