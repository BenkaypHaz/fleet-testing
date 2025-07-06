using Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Customer
{
    public class CustomerProfiles : AutoMapper.Profile
    {
        public CustomerProfiles() 
        {
            #region Customer Warehouse
            CreateMap<CustomerWarehouse, CustomerWarehouseReadDto>();
            #endregion
        }
    }
}
