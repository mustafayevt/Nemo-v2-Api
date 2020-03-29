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
                        .MapFrom(user => user.UserRoles.Select(x => x.Role)));
            CreateMap<UserDto, User>()
                .ForMember(x => x.UserRoles, opt => opt
                    .MapFrom(y => y.Roles));
            //Role
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleDto, UserRole>()
                .ForMember(x => x.Role, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //Restaurant
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            
            //Warehouse
            CreateMap<Warehouse, WarehouseDto>()
                .ForMember(x => x.RestWareRels,
                    opt => opt
                        .MapFrom(warehouse => warehouse.RestWareRels.Select(x => x.Restaurant)))
                .ReverseMap();
            
            //WarehouseRestRel
            CreateMap<RestaurantDto, RestWareRel>()
                .ForMember(x => x.Restaurant, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientCategory
            CreateMap<IngredientCategory, IngredientCategoryDto>().ReverseMap();
            CreateMap<IngredientCategoryDto, IngredientCategoryRel>()
                .ForMember(x => x.IngredientCategory, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //Ingredient
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            
            //Table
            CreateMap<Table, TableDto>().ReverseMap();
            
            //Section
            CreateMap<Section, SectionDto>().ReverseMap();
            
            //Supplier
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            
            //FoodGroup
            CreateMap<FoodGroup, FoodGroupDto>().ReverseMap();
            
            //Food
            CreateMap<Food, FoodDto>().ReverseMap();
            
            //FoodGroupRel
            CreateMap<FoodGroupDto, FoodGroupRel>()
                .ForMember(x => x.FoodGroup, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientFoodRel
            CreateMap<IngredientDto, IngredientFoodRel>()
                .ForMember(x => x.Ingredient, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientsInsert
            CreateMap<IngredientsInsert, IngredientsInsertDto>().ReverseMap();
            
            //WarehouseInvoice
            CreateMap<WarehouseInvoice, WarehouseInvoiceDto>().ReverseMap();
        }
    }
}