using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerProviderProfile
{
    public long Id { get; set; }

    public long BusinessProviderProfileTypeId { get; set; }

    public long BusinessPartnerProviderProfileCategoryId { get; set; }

    public long GeneralCityId { get; set; }

    public string LegalId { get; set; } = null!;

    public string BusinessName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessPartnerProviderDriver> BusinessPartnerProviderDrivers { get; set; } = new List<BusinessPartnerProviderDriver>();

    public virtual BusinessPartnerProviderProfileCategory BusinessPartnerProviderProfileCategory { get; set; } = null!;

    public virtual ICollection<BusinessPartnerProviderProfileContactPerson> BusinessPartnerProviderProfileContactPeople { get; set; } = new List<BusinessPartnerProviderProfileContactPerson>();

    public virtual ICollection<BusinessPartnerproviderTransportVehicle> BusinessPartnerproviderTransportVehicles { get; set; } = new List<BusinessPartnerproviderTransportVehicle>();

    public virtual BusinessPartnerProviderProfileType BusinessProviderProfileType { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual GeneralCity GeneralCity { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
