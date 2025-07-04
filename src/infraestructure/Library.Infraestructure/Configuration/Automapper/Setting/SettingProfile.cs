
using Library.Infraestructure.Persistence.DTOs.Setting.FreightPricing.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Shipment
{
    public class SettingProfile : AutoMapper.Profile
    {
        public SettingProfile()
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


        }
    }
}
