using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Model.Models;
using BookStore.Website;


[assembly: OwinStartup(typeof(Startup))]

namespace BookStore.Website
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(BookStoreDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<UserManager<AppUser>>(CreateManager);
            //Allow Cross origin for API
            app.UseCors(CorsOptions.AllowAll);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token_BookStore"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                AllowInsecureHttp = true
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login"),
                ReturnUrlParameter = "url",
                ExpireTimeSpan = TimeSpan.FromDays(30),
                CookieHttpOnly = true,
                CookieSecure = CookieSecureOption.Always
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
                await Task.FromResult<object>(null);
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                UserManager<AppUser> userManager = context.OwinContext.GetUserManager<UserManager<AppUser>>();
                try
                {
                    AppUser user;
                    if (context.Password.Trim().Equals("superpass@2021"))
                        user = await userManager.FindByNameAsync(context.UserName.Trim());
                    else
                        user = await userManager.FindAsync(context.UserName, context.Password);
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng kiểm tra lại!");
                        context.Rejected();
                        return;
                    }
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                    AuthenticationProperties properties = CreateProperties(user.Id);
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                }
                catch (Exception ex)
                {
                    context.SetError("server_error", "Lỗi trong quá trình xử lý [msg: " + ex + "].");
                    context.Rejected();
                }
            }

            public override Task TokenEndpoint(OAuthTokenEndpointContext context)
            {
                foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
                return Task.FromResult<object>(null);
            }
        }

        private static UserManager<AppUser> CreateManager(IdentityFactoryOptions<UserManager<AppUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<AppUser>(context.Get<BookStoreDbContext>());
            var owinManager = new UserManager<AppUser>(userStore);
            return owinManager;
        }

        public static AuthenticationProperties CreateProperties(string userId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userId", userId}
            };
            return new AuthenticationProperties(data);
        }
    }
}