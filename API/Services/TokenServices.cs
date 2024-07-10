﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenServices(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
    var tokenKey=config["TokenKey"]??throw new Exception("Cannot access tokenkey from AppSetting");
    
    if( tokenKey.Length<64) throw new Exception("Your token key needs to be longer");
   var tokenHandler=new JwtSecurityTokenHandler();
   var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
    
    var claims =new List<Claim>{
        new Claim(ClaimTypes.NameIdentifier,user.UserName)
        
    };
var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

    var tokenDescriptor = new SecurityTokenDescriptor{
Subject=new ClaimsIdentity(claims),
Expires=DateTime.UtcNow.AddDays(7),
SigningCredentials=creds
    };

    var token=tokenHandler.CreateToken(tokenDescriptor);
return tokenHandler.WriteToken(token);
    }
}