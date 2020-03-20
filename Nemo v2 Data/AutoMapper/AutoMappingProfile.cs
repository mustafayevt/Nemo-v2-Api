using System.Linq;
using AutoMapper;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.AutoMapper
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(x => x.Role)));
            CreateMap<UserDto, User>();
        }
    }
}