﻿using System;
using System.Linq;
using System.Security.Claims;

namespace Our.Umbraco.AuthU.Web
{
    public class BaseOAuthAttribute : Attribute
    {
        public string Realm { get; set; }

        protected OAuthContext Context { get; }

        public BaseOAuthAttribute()
            : this(OAuth.DefaultRealm)
        { }

        public BaseOAuthAttribute(string realm)
        {
            Realm = realm;
            Context = OAuth.GetContext(Realm);
        }

        protected bool ValidatePrincipal(ClaimsPrincipal principal)
        {
            // Make sure principal isn't null
            if (principal == null)
                return false;

            // Make sure identity is authenticated
            if (!principal.Identity.IsAuthenticated)
                return false;

            // Make sure principal belongs to realm
            var realm = principal.Claims.FirstOrDefault(x => x.Type == OAuth.ClaimTypes.Realm)?.Value;
            if (realm == null || realm != Realm)
                return false;

            // Make sure username is present
            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // Make sure username is valid
            if (!Context.Services.UserService.ValidateUser(username))
                return false;

            return true;
        }
    }
}
