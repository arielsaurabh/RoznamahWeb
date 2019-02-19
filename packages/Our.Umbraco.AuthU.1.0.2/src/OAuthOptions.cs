﻿using Our.Umbraco.AuthU.Interfaces;
using Our.Umbraco.AuthU.Services;

namespace Our.Umbraco.AuthU
{
    public class OAuthOptions : IOAuthOptions
    {
        public OAuthOptions()
        {
            UserService = new UmbracoMembersOAuthUserService();
            AllowedOrigin = "*";
            AccessTokenLifeTime = 20; // 20 minutes
            RefreshTokenLifeTime = 1440; // 1 day
        }

        public IOAuthUserService UserService { get; set; }

        public string SymmetricKey { get; set; }

        public string AllowedOrigin { get; set; }

        public int AccessTokenLifeTime { get; set; }

        public IOAuthClientStore ClientStore { get; set; }

        public IOAuthRefreshTokenStore RefreshTokenStore { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        public bool AllowInsecureHttp { get; set; }
    }
}
