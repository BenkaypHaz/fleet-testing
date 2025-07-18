﻿
using Library.Infraestructure.Persistence.DTOs.Setting.FreightPricing.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Update;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Update;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Shipment
{
    public class SettingProfiles : AutoMapper.Profile
    {
        public SettingProfiles()
        {
            #region Shipment Project Contract
            CreateMap<SettingFreightPricingPerCustomer, FreightPricingSuggestedPriceReadDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerWarehouse.CustomerProfileId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerWarehouse.CustomerProfile.Name))
                .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.CustomerWarehouseId))
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.CustomerWarehouse.Name))
                .ForMember(dest => dest.DispatchBranchId, opt => opt.MapFrom(src => src.SettingDispatchBranchId))
                .ForMember(dest => dest.DispatchBranchName, opt => opt.MapFrom(src => src.SettingDispatchBranch.Name))
                .ForMember(dest => dest.SuggestedPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
            #endregion

            #region SettingDispatchBranch

            CreateMap<SettingDispatchBranch, SettingDispatchBranchReadDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.GeneralCity != null ? src.GeneralCity.Name : null));

            CreateMap<SettingDispatchBranch, SettingDispatchBranchReadFirstDto>()
                .ForMember(dest => dest.GeneralCity, opt => opt.MapFrom(src => src.GeneralCity))
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            CreateMap<SettingDispatchBranchCreateDto, SettingDispatchBranch>();

            CreateMap<SettingDispatchBranchUpdateDto, SettingDispatchBranch>();

            #endregion

            #region SettingFuelOrderIssuer

            CreateMap<SettingFuelOrderIssuer, SettingFuelOrderIssuerReadDto>();

            CreateMap<SettingFuelOrderIssuer, SettingFuelOrderIssuerReadFirstDto>()
               .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
               .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            CreateMap<SettingFuelOrderIssuerCreateDto, SettingFuelOrderIssuer>();

            CreateMap<SettingFuelOrderIssuerUpdateDto, SettingFuelOrderIssuer>();

            #endregion
        }
    }
}
