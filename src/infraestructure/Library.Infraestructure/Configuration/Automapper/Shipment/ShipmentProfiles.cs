

using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ShipmentFreight.Create;
using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentFreightType.Read;
using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Shipment
{
    public class ShipmentProfiles : AutoMapper.Profile
    {
        public ShipmentProfiles()
        {
            #region Shipment Freight Type

            CreateMap<ShipmentFreightType, ShipmentFreightTypeReadDto>();

            #endregion

            #region Shipment Project Contract

            CreateMap<ShipmentProjectContract, ShipmentProjectContractReadDto>();

            #endregion

            #region Shipment Freight

            CreateMap<ShipmentFreightCreateDto, ShipmentFreight>()
                   .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                   .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            #endregion
        }
    }
}
