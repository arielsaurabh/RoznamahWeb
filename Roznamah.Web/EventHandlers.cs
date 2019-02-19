using Our.Umbraco.AuthU;
using Our.Umbraco.AuthU.Data;
using Our.Umbraco.AuthU.Services;
using System;
using System.Web;
using Umbraco.Core;

namespace Roznamah.Web
{
    public class EventHandlers : ApplicationEventHandler
    {
        /// <summary>
        /// Override application start for implementing the oAuth
        /// set symmetric key for authentication token should be > 32 chars
        /// </summary>
        /// <param name="app"></param>
        /// <param name="ctx"></param>
        protected override void ApplicationStarted(UmbracoApplicationBase app, ApplicationContext ctx)
        {
            //OAuth implementation for get the authentication token

            OAuth.ConfigureEndpoint("/oauth/token", new OAuthOptions
            {
                UserService = new UmbracoUsersOAuthUserService(),
                SymmetricKey = "856FECBA3B06519C8DDDBC80BB080553",
                AccessTokenLifeTime = 20, // Minutes
                AllowInsecureHttp = true // During development only
            });

            //OAuth.ConfigureEndpoint("realm", "/oauth/token", new OAuthOptions
            //{
            //    UserService = new UmbracoMembersOAuthUserService(),
            //    SymmetricKey = "856FECBA3B06519C8DDDBC80BB080553",
            //    AccessTokenLifeTime = 20, // Minutes
            //    ClientStore = new UmbracoDbOAuthClientStore(),
            //    RefreshTokenStore = new UmbracoDbOAuthRefreshTokenStore(),
            //    RefreshTokenLifeTime = 1440, // Minutes (1 day)
            //    AllowedOrigin = "*",
            //    AllowInsecureHttp = true // During development only
            //});
        }
    }
}