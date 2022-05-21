using AdvertisementApp.Dto;
using AdvertisementApp.Web.Models;
using AutoMapper;

namespace AdvertisementApp.Web.Mappings.AutoMapper
{
    public class UserCreateModelProfile : Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel, AppUserCreateDto>();
        }
    }
}
