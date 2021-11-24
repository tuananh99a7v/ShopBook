using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UniLibrary.Helper
{
	public class ResponseApi<T> where T : class
	{
		public List<T> data { set; get; }
		public int status { set; get; }
		public string date { set; get; }
	}

	public static class ApiHelper
	{
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
		public static HttpResponseMessage Post(string sUriHost, string pathApi, string jSonObject)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.BaseAddress = new Uri(sUriHost);
					client.DefaultRequestHeaders.Accept.Clear();
					var requestContent = new StringContent(jSonObject, Encoding.UTF8, "application/json");
					var a = client.PostAsync(pathApi, requestContent).Result;
					return client.PostAsync(pathApi, requestContent).Result;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public static async Task<HttpResponseMessage> PostAsync(string sUriHost, string pathApi, string jSonObject)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.BaseAddress = new Uri(sUriHost);
					client.DefaultRequestHeaders.Accept.Clear();
					var requestContent = new StringContent(jSonObject, Encoding.UTF8, "application/json");
					return await client.PostAsync(pathApi, requestContent);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public static HttpResponseMessage PostApi(HttpRequestMessage request, string sUriHost, string pathApi)
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
	}
}