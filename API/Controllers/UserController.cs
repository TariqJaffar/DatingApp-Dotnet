using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UserController:BaseApiController
{
     public readonly DataContext _Context;

    public UserController(DataContext context)
    {
        _Context = context;
    }
 [AllowAnonymous]
   [HttpGet]
    public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users= await _Context.Users.ToListAsync();
        return Ok(users);

    }
   [Authorize]
    [HttpGet("{id:int}")]
       public async Task< ActionResult<AppUser> >GetUser(int id )
    {
        var users=await _Context.Users.FindAsync(id);
       if(users==null) return NotFound();
        return Ok(users);

    }


}
