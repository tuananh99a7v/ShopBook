using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Model.Models;
using BookStore.Website;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Web.Configuration;


[assembly: OwinStartup(typeof(Startup))]
[assembly: OwinStartupAttribute(typeof(Startup))]

namespace BookStore.Website
{
    public partial class Startup
    {
        public void ConfigureApiAuth(IAppBuilder app)
        {
            var issuer = WebConfigurationManager.AppSettings["auth0:Domain"];
            var audience = WebConfigurationManager.AppSettings["auth0:ClientId"];
            var secret = TextEncodings.Base64Url.Decode(WebConfigurationManager.AppSettings["auth0:ClientSecret"]);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[] {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                }
                });
        }

        public void ConfigureWebAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var provider = new Auth0.Owin.Auth0AuthenticationProvider
            {
                OnReturnEndpoint = (context) =>
                {
                    // xsrf validation
                    if (context.Request.Query["state"] != null && context.Request.Query["state"].Contains("xsrf="))
                    {
                        var state = HttpUtility.ParseQueryString(context.Request.Query["state"]);
                        AntiForgery.Validate(context.Request.Cookies["__RequestVerificationToken"], state["xsrf"]);
                    }

                    return System.Threading.Tasks.Task.FromResult(0);
                }
            };

            app.UseAuth0Authentication(
                clientId: System.Configuration.ConfigurationManager.AppSettings["auth0:ClientId"],
                clientSecret: System.Configuration.ConfigurationManager.AppSettings["auth0:ClientSecret"],
                domain: System.Configuration.ConfigurationManager.AppSettings["auth0:Domain"],
                saveIdToken: true,
                saveRefreshToken: true,
                provider: provider);
        }
    } 
}