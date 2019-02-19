﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Our.Umbraco.AuthU.Interfaces
{
    public interface IOAuthUserService
    {
        string UserType { get; }

        bool ValidateUser(string username);

        bool ValidateUser(string username, string password);

        IEnumerable<Claim> GetUserClaims(string username);
    }
}
