using System.Diagnostics;
using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extension;
using API.Interface;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class UserController(IUserRepository userRepository,IMapper mapper,
IPhotoService photoService):BaseApiController
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

var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
if(user==null) return BadRequest("could not found");
mapper.Map(memberUpdateDto,user);

if(await userRepository.SaveAllAsync()) return NoContent();
return BadRequest("Failed to update the user");
}

[HttpPost("add-photo")]
public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
{
var user=await userRepository.GetUserByUsernameAsync(User.GetUsername());
if(user==null) return BadRequest("Cannot Update the user");
var result=await photoService.AddPhotoAsync(file);
if(result.Error!=null)return BadRequest(result.Error.Message);
var photo = new Photo
{
URL=result.SecureUrl.AbsoluteUri,
PublicID=result.PublicId

};
if(user.photos.Count==0)photo.IsMain=true;
user.photos.Add(photo);

if(await userRepository.SaveAllAsync())
{
     return CreatedAtAction(nameof(GetUser),
     new {UserName= user.UserName},mapper.Map<PhotoDto>(photo));
    
    }
return BadRequest("Problem adding photo");

}
[HttpPut("set-main-photo/{photoId:int}")]
public async Task<ActionResult> SetMainPhoto(int photoId){
    var user =await userRepository.GetUserByUsernameAsync(User.GetUsername());
    if(user==null) return BadRequest("Could Not find the user");
    var photo=user.photos.FirstOrDefault(x=>x.Id==photoId);
    if(photo==null || photo.IsMain) return BadRequest("CannotUse this as main photo");
    var currentMain=user.photos.FirstOrDefault(x=>x.IsMain) ;
    if(currentMain!=null) currentMain.IsMain=false;
    photo.IsMain=true;
    if(await userRepository.SaveAllAsync() )return NoContent();
    return BadRequest("Problem setting main photo");
}

[HttpDelete("delete-photo/{photoId:int}")]
public async Task<ActionResult> DeletePhotos(int photoId)
{
    // Fetch the user with their photos
    var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

    if (user == null) return BadRequest("Could Not find the user");

    // Find the photo by photoId
    var photo = user.photos.FirstOrDefault(x => x.Id == photoId);

    // Check if the photo exists or is the main photo
    if (photo == null || photo.IsMain) return BadRequest($"Cannot use this as main photo: {photoId}");

    // If the photo has a PublicID, delete it from the cloud service
    if (photo.PublicID != null)
    {
        var result = await photoService.DeletePhotoAsync(photo.PublicID);
        if (result.Error != null) return BadRequest(result.Error.Message);
    }

    // Remove the photo from the user's photo collection
    user.photos.Remove(photo);

    // Save changes to the database
    if (await userRepository.SaveAllAsync()) return Ok();

    return BadRequest("Problem Deleting Photo");
}










}
