using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UserController(IUserRepository userRepository):BaseApiController
{

   [HttpGet]
    public async Task< ActionResult<IEnumerable<MembersDto>>> GetUsers()
    {
        var users= await userRepository.GetMemberAsync();
        return Ok(users);

    }

    [HttpGet("{username}")]
       public async Task< ActionResult<MembersDto> >GetUser(string username )
    {
        var users=await userRepository.GetMemberAsync(username);
       if(users==null) return NotFound();
        return users;

    }


}
