﻿using Microsoft.AspNetCore.Identity;

namespace WebApi2.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
