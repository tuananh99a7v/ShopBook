using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using BookStore.Model.Models;
using BookStore.Service;
using BookStore.Utilities.ViewModel;
using UniLibrary.Helper;

namespace BookStore.Website.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private readonly ILogHistoryService _logHistoryService;

        public ApiControllerBase(ILogHistoryService logHistoryService)
        {
            _logHistoryService = logHistoryService;
        }

        public string GetTokenIdentity()
        {
            try
            {
                if (Request.Headers.GetCookies("access_token") == null) return "";
                return Request.Headers.GetCookies("access_token").FirstOrDefault()?["access_token"].Value;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public UserCookie GetCookie(string cookieName)
        {
            UserCookie userCookie = new UserCookie();
            CookieHeaderValue cookie = Request.Headers.GetCookies("BookStoreSystem").FirstOrDefault();
            if (cookie != null)
            {
                string value = cookie["BookStoreSystem"]["cookie"];
                if (string.IsNullOrEmpty(value)) return userCookie;
                userCookie = JsonConvert.DeserializeObject<UserCookie>(value.DecryptString());
            }
            return userCookie;
        }

        public int AddLogHistory(LogHistory logHistory)
        {
            return _logHistoryService.Add(logHistory);
        }

        public LogHistory AddCommitLogHistory(LogHistory logHistory)
        {
            _logHistoryService.Add(logHistory);
            _logHistoryService.Save();
            return logHistory;
        }

        public async Task AddLogHistoryAsync(LogHistory logHistory)
        {
            _logHistoryService.Add(logHistory);
            await _logHistoryService.SaveAsync();
        }

        protected ApplicationRoleManager RoleManager => Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        public ApplicationSignInManager SignInManager => Request.GetOwinContext().Get<ApplicationSignInManager>();

        protected ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        protected IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function, string parammeter = "")
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (MessageDuplicateException mdEx)
            {
                response = requestMessage.BadResult(mdEx.Message);
            }
            catch (Exception ex)
            {
                response = requestMessage.BadResult(ex.Message);
            }
            //Đang code dở đến đây
            return response;
        }

        protected async Task<HttpResponseMessage> CreateHttpResponseAsync(HttpRequestMessage requestMessage, Func<Task<HttpResponseMessage>> function, string parammeter = "")
        {
            HttpResponseMessage response = null;
            try
            {
                response = await function.Invoke();
            }
            catch (MessageDuplicateException mdEx)
            {
                response = requestMessage.BadResult(mdEx.Message);
            }
            return response;
        }
    }
}