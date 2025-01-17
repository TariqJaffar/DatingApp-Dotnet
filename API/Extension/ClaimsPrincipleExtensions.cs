using System;
using System.Resources;
using System.Security.Claims;

namespace API.Extension;

public static class ClaimsPrincipleExtensions
{
public static string GetUsername(this ClaimsPrincipal user){
    var username=user.FindFirstValue(ClaimTypes.NameIdentifier);
    if(username==null) throw new Exception("Cannot get the usernameToken");
    return username;
}
}
