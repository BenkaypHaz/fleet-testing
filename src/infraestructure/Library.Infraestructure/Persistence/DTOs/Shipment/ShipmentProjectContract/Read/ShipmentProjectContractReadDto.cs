using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read
{
    public class ShipmentProjectContractReadDto
    {
        public long Id { get; set; }

        public long CustomerProfileId { get; set; }

        public long BusinessPartnerFuelOrderIssuerId { get; set; }

        public long SettingDispatchBranchId { get; set; }

        public long ContractNumber { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int ExpectedFreightQuantity { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
