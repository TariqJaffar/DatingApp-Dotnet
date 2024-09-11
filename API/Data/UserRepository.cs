using System;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context,IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<MembersDto>> GetMemberAsync()
    {
    return await context.Users
    .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
    .ToListAsync();
      }

    public async Task<MembersDto?> GetMemberAsync(string username)
    {
        return await context.Users
        .Where(x=>x.UserName== username)
      .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
   
    }

    public async Task<IEnumerable<AppUser>> GetUserAsync()
    {
        return await context.Users
        .Include(x=>x.photos)
        .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
    return await context.Users
        .Include(u => u.photos) 
        
        .SingleOrDefaultAsync(x => x.UserName == username);
}

    public async Task<bool> SaveAllAsync()
    {
      return await context.SaveChangesAsync()>0;
    }

    public void Update(AppUser user)
    {
      context.Entry(user).State=EntityState.Modified;
    }
}
