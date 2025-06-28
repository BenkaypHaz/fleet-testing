using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL
{
    public partial class BusinessPartnerVehicleBrand
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<BusinessPartnerProviderTransportVehicle> BusinessPartnerProviderTransportVehicles { get; set; } = new List<BusinessPartnerProviderTransportVehicle>();

        public virtual ICollection<BusinessPartnerVehicleModel> BusinessPartnerVehicleModels { get; set; } = new List<BusinessPartnerVehicleModel>();

        public virtual AuthUser CreatedByNavigation { get; set; } = null!;

        public virtual AuthUser? ModifiedByNavigation { get; set; }
    }
}
