using System;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<ProfileUpdateDto, AppUser>();
        CreateMap<Photo, PhotoDto>();
        CreateMap<AppUser, UserProfileDto>()
            .ForMember(dest => dest.PhotoUrl, opt =>
                opt.MapFrom(src => src.Photo != null ? src.Photo.Url : null));
    }
}
