using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniLibrary.Helper;

namespace BookStore.Website.Infrastructure.Core
{
	public class MvcControllerBase : Controller
    {
      

        public string GetTokenIdentity()
        {
            try
            {
                if (Request.Cookies["access_token"] == null) return "";
                return Request.Cookies["access_token"].Value;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        protected ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        protected ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        protected ActionResult ReturnResult(Func<ActionResult> function, string parammeter = "")
        {
            ActionResult result = null;
            try
            {
                result = function.Invoke();
            }
            catch (MessageDuplicateException mdEx)
            {
                result = RedirectToAction("ErrorMessage", "Home", new { url = Request.Url?.PathAndQuery, content = mdEx.Message.EncodeBase64() });
            }
            return result;
        }

        protected async Task<ActionResult> ReturnResultAsync(Func<Task<ActionResult>> function, string parammeter = "")
        {
            ActionResult result = null;
            try
            {
                result = await function.Invoke();
            }
            catch (MessageDuplicateException mdEx)
            {
                result = RedirectToAction("ErrorMessage", "Home", new { url = Request.Url?.PathAndQuery, content = mdEx.Message.EncodeBase64() });
            }
            
            return result;
        }
    }
}