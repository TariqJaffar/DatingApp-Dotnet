using System;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMappersProfiles:Profile
{
public AutoMappersProfiles()
{
    CreateMap<AppUser,MembersDto>()
    .ForMember(d=>d.Age ,o=>o.MapFrom(s=> s.DateOfBirth.CalculateAge()))
    .ForMember(d=>d.photoUrl,
    o=>o.MapFrom(
        s=>s.photos.FirstOrDefault(x=>x.IsMain)!.URL)
        );
    CreateMap<Photo,PhotoDto>();
}
}
