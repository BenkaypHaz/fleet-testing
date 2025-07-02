

using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Shipment
{
    public class ShipmentProfile : AutoMapper.Profile
    {
        public ShipmentProfile()
        {
            #region Shipment Project Contract
            CreateMap<ShipmentProjectContract, ShipmentProjectContractReadDto>();
            #endregion
        }
    }
}
