﻿using System.Security.Claims;

namespace Bad.Strings;

public static class ClaimsAnalyser
{
    public static bool HasNightPrivileges(ClaimsPrincipal user)
    {
        // this code is not correct, but I don't want you to fix it. Write tests that assume that this code is correct
        if (!user.Identities.FirstOrDefault()!.IsAuthenticated)
        {
            return false;
        }

        if (!user.IsInRole("nightrole"))
        {
            return false;
        }

        return user.HasClaim(c => c.Issuer == "computas" && c.Type == "adgroup" && c.Value == "night-adgroup");
    }
}