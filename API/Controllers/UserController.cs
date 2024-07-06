using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
     public readonly DataContext _Context;

    public UserController(DataContext context)
    {
        _Context = context;
    }

   [HttpGet]
    public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users= await _Context.Users.ToListAsync();
        return Ok(users);

    }
    [HttpGet("{id:int}")]
       public async Task< ActionResult<AppUser> >GetUser(int id )
    {
        var users=await _Context.Users.FindAsync(id);
       if(users==null) return NotFound();
        return Ok(users);

    }


}
