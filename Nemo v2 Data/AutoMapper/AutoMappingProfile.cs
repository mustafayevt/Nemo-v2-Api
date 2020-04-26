﻿using System;
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
            
            //RestSupplierRel
            CreateMap<RestaurantDto, RestSupplierRel>()
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
            CreateMap<KeyValuePair<SectionDto, PrinterDto>, FoodPrinterAndSectionRel>()
                .ForMember(x => x.SectionId, opt => opt.MapFrom(src => src.Key.Id))
                .ForMember(x => x.PrinterId, opt => opt.MapFrom(src => src.Value.Id));
            
                //reverse
                CreateMap<FoodPrinterAndSectionRel, KeyValuePair<SectionDto, PrinterDto>>()
                    .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Section))
                    .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Printer));


                CreateMap<KeyValuePair<long, decimal>, IngredientFoodRel>()
                    .ForMember(x => x.IngredientId, opt => opt.MapFrom(src => src.Key))
                    .ForMember(x => x.Quantity, opt => opt.MapFrom(src => src.Value));
                
                //reverse
                CreateMap<IngredientFoodRel,KeyValuePair<long, decimal>>()
                    .ForMember(x => x.Key, opt => opt.MapFrom(src => src.IngredientId))
                    .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Quantity));


            CreateMap<Food, FoodDto>()
                .ForMember(x=>x.SectionToPrinter,opt=>opt.MapFrom(y=>y.FoodPrinterAndSectionRels)).ReverseMap();
            
            //FoodGroupRel
            CreateMap<FoodGroupDto, FoodGroupRel>()
                .ForMember(x => x.FoodGroup, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientFoodRel
            CreateMap<IngredientDto, IngredientFoodRel>()
                .ForMember(x => x.Ingredient, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientWarehouseRel
            CreateMap<WarehouseDto, IngredientWarehouseRel>()
                .ForMember(x => x.Warehouse, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientsInsert
            CreateMap<IngredientsInsert, IngredientsInsertDto>().ReverseMap();
            
            //WarehouseInvoice
            CreateMap<WarehouseInvoice, WarehouseInvoiceDto>().ReverseMap();
            
            //Invoice
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            
            //Printer
            CreateMap<Printer, PrinterDto>().ReverseMap();
            
            //FoodInvoiceRel
            CreateMap<FoodDto,FoodInvoiceRel>()
                .ForMember(x => x.Food, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //IngredientsExport
            CreateMap<IngredientsExport, IngredientsExportDto>().ReverseMap();
            
            //Buyer
            CreateMap<Buyer, BuyerDto>().ReverseMap();
            
            //RestBuyerRel
            CreateMap<RestaurantDto, RestBuyerRel>()
                .ForMember(x => x.Restaurant, opt => opt
                    .MapFrom(y => y)).ReverseMap();
            
            //WarehouseExportInvoice
            CreateMap<WarehouseExportInvoice, WarehouseExportInvoiceDto>().ReverseMap();
            
            
            //Profit
            CreateMap<Profit, ProfitDto>().ReverseMap();
            
        }
    }
}