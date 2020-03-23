using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.AutoMapper
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            //User
            CreateMap<User, UserDto>()
                .ForMember(x => x.Roles, 
                    opt => opt
                        .MapFrom(user => user.UserRoles.Select(x=>x.Role)))
                .ReverseMap();
            
            //Role
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}