using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class BuggyController(DataContext context):BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string>GetAuth(){
        return "AutherText";
    }

 
     [HttpGet("not-found")]
    public ActionResult<AppUser>GetAuthNotFound(){
        
        var thing =context.Users.Find(-1);
        if(thing==null)return NotFound();
        return thing;
    }
     [HttpGet("server-error")]
    public ActionResult<AppUser>GetServerError(){
      var thing=context.Users.Find(-1)?? throw new Exception("Server Not  REspondin Tar");
return thing;
    }
     [HttpGet("bad-Request")]
    public ActionResult<string>GetBadRequest(){
        return BadRequest("this is not a good request");
    }



}
