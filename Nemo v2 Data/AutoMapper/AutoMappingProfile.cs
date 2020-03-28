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
                        .MapFrom(user => user.UserRoles.Select(x => x.Role)))
                .ReverseMap();
            //Role
            CreateMap<Role, RoleDto>().ReverseMap();
            
            //Restaurant
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            
            //Warehouse
            CreateMap<Warehouse, WarehouseDto>()
                .ForMember(x => x.Restaurants,
                    opt => opt
                        .MapFrom(warehouse => warehouse.RestWareRels.Select(x => x.Restaurant)))
                .ReverseMap();
        }
    }
}