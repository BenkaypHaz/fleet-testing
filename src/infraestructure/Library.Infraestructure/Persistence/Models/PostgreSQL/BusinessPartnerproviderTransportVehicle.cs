using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class BusinessPartnerproviderTransportVehicle
{
    public long Id { get; set; }

    public long BusinessPartnerProviderProfileId { get; set; }

    public long? BusinessPartnerProviderDriverId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string Color { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public int Axles { get; set; }

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessPartnerProviderDriver? BusinessPartnerProviderDriver { get; set; }

    public virtual BusinessPartnerProviderProfile BusinessPartnerProviderProfile { get; set; } = null!;

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }
}
