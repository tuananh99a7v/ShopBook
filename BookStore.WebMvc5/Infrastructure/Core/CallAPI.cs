using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStore.Utilities.ViewModel;
using UniLibrary.Helper;

namespace BookStore.Website.Infrastructure.Core
{
    public static class CookieApi
    {
        public static HttpResponseMessage SetCookieLogin(this HttpResponseMessage response, string cookieName, UserViewModel userViewModel, DateTimeOffset expires)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            if (userViewModel != null)
            {
                nameValueCollection["cookie"] = JsonConvert.SerializeObject(new UserCookie()
                {
                    UserId = userViewModel.UserId,
                    UserName = userViewModel.UserName,
                    FullName = userViewModel.FullName,
                    Avatar = userViewModel.Avatar
                }).EncryptString();
                CookieHeaderValue cookie = new CookieHeaderValue(cookieName, nameValueCollection)
                {
                    Expires = expires,
                    Domain = null,
                    Path = "/"
                };
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            else
            {
                nameValueCollection["cookie"] = "";
                CookieHeaderValue cookie = new CookieHeaderValue(cookieName, nameValueCollection)
                {
                    Expires = DateTimeOffset.Now.AddDays(-1),
                    Domain = null,
                    Path = "/"
                };
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            return response;
        }
    }
    public class CallApiResult<T> where T : class
    {
        public string Message { set; get; }
        public string code { set; get; }
        public T data { set; get; }
    }

    public static class CallApi
    {
        public static HttpResponseMessage OkResult<T>(this HttpRequestMessage request, T result) where T : class
        {
            return request.CreateResponse(HttpStatusCode.OK, new { Message = "", code = HttpStatusCode.OK, data = result });
        }

        public static HttpResponseMessage BadResult(this HttpRequestMessage request, string message)
        {
            return request.CreateResponse(HttpStatusCode.BadRequest, new { Message = message, code = HttpStatusCode.BadRequest, data = "" });
        }

        /// <summary>
        /// API Controller
        /// </summary>
        public static async Task<HttpResponseMessage> GetApiAsync(HttpRequestMessage request, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return await client.GetAsync(pathApi);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage GetApi(HttpRequestMessage request, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return client.GetAsync(pathApi).Result;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> PutApiAsync<T>(HttpRequestMessage request, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return await client.PutAsJsonAsync(pathApi, obj);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage PutApi<T>(HttpRequestMessage request, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return client.PutAsJsonAsync(pathApi, obj).Result;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> PostApiAsync<T>(HttpRequestMessage request, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return await client.PostAsJsonAsync(pathApi, obj);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage PostApi<T>(HttpRequestMessage request, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return client.PostAsJsonAsync(pathApi, obj).Result;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> DeleteApiAsync(HttpRequestMessage request, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return await client.DeleteAsync(pathApi);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage DeleteApi(HttpRequestMessage request, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = request.Headers.Authorization;
                    return client.DeleteAsync(pathApi).Result;
                }
            }
            catch
            {
                throw;
            }
        }

        // Controller
        public static async Task<HttpResponseMessage> GetApiAsync(string token, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return await client.GetAsync(pathApi);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage GetApi(string token, string sUriHost, string pathApi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return client.GetAsync(pathApi).Result;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> PostApiAsync<T>(string token, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return await client.PostAsJsonAsync(pathApi, obj);
                }
            }
            catch
            {
                throw;
            }
        }

        public static HttpResponseMessage PostApi<T>(string token, string sUriHost, string pathApi, T obj) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sUriHost);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    return client.PostAsJsonAsync(pathApi, obj).Result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}