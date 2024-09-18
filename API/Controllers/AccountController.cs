using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService
,IMapper mapper) : BaseApiController
{

    [HttpPost("Register")]//accountRegister
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (registerDto.username == null)
        {
            return BadRequest("User NAme VAlue NUll");
        }
        if (await UserExist(registerDto.username)) return BadRequest("User Already Exist");


        using var hmac = new HMACSHA512();
        var user=mapper.Map<AppUser>(registerDto);
        user.UserName=registerDto.username.ToLower();
        user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password));
        user.PasswordSalt=hmac.Key;
      

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
            UserName = user.UserName,
            Token = tokenService.CreateToken(user),
            KnownAs=user.KnownAs
         };
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto _logindto)
    {
        var user = await context.Users
        .Include(p=>p.photos)
        .FirstOrDefaultAsync(
        x => x.UserName.ToLower() == _logindto.username.ToLower());
        if (user == null) return Unauthorized("Invalid UserName");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_logindto.password));

        for (int i = 0; i < ComputeHash.Length; i++)
        {
            if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid PassWord");

        }
        return new UserDto
        {
            UserName = user.UserName,
            KnownAs=user.KnownAs,
            Token = tokenService.CreateToken(user),
            photoUrl =user.photos.FirstOrDefault(x=>x.IsMain)?.URL
        };
    }





    private async Task<bool> UserExist(string Username)
    {
        if (string.IsNullOrEmpty(Username))
        {
            throw new ArgumentNullException(nameof(Username));
        }
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == Username.ToLower());
    }

}