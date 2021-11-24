using BookStore.Model.Models;
using BookStore.Utilities.ViewModel;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Website.Infrastructure.Core;
using Microsoft.AspNet.Identity;

namespace BookStore.WebMvc5.Areas.Admin.Controllers
{
    public class HomeController : MvcControllerBase
    {
		// GET: Admin/Home
		public ActionResult Login()
        {
            return View();
        }
        public PartialViewResult GetMenu()
        {

            UserViewModel userViewModel = new UserViewModel();
            if (string.IsNullOrEmpty(User.Identity.GetUserId()))
			{
				return PartialView(userViewModel);
			}

			AppUser appUser = UserManager.FindById(User.Identity.GetUserId());
            if (appUser == null || appUser.Status == 2)
            {
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                if (Response.Cookies["BookStoreSystem"] != null) Response.Cookies["BookStoreSystem"].Expires = DateTime.Now.AddDays(-1);
                if (Response.Cookies["access_token"] != null) Response.Cookies["access_token"].Expires = DateTime.Now.AddDays(-1);
                ViewBag.Url = "/login";
                return PartialView(userViewModel);
            }

            userViewModel = new UserViewModel
            {
                UserName = appUser.UserName,
                FullName = appUser.FullName,
                Email = appUser.Email,
                Avatar = appUser.Avatar,
            };
            return PartialView(userViewModel);
        }
        public PartialViewResult GetSidebar()
        {
            return PartialView();
        }
    }
}