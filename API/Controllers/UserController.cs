using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UserController(IUserRepository userRepository,IMapper mapper):BaseApiController
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
[HttpPut]
public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
{
var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
if(username==null)return BadRequest("NO Username Found in Token");
var user = await userRepository.GetUserByUsernameAsync(username);
if(user==null) return BadRequest("could not found");
mapper.Map(memberUpdateDto,user);

if(await userRepository.SaveAllAsync()) return NoContent();
return BadRequest("Failed to update the user");
}

}
