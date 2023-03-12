using AutoMapper;
using PlatformService.Data.Dtos;
using PlatformService.Models;

namespace PlatformService.Data.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}