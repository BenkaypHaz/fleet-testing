using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerProviderDriver
{
    public long Id { get; set; }

    public long? BusinessPartnerProviderProfileId { get; set; }

    public string? NationalId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string Nationality { get; set; } = null!;

    public DateOnly? HireDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessPartnerProviderProfile? BusinessPartnerProviderProfile { get; set; }

    public virtual ICollection<BusinessPartnerproviderTransportVehicle> BusinessPartnerproviderTransportVehicles { get; set; } = new List<BusinessPartnerproviderTransportVehicle>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
