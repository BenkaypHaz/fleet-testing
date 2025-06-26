using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class CustomerWarehouse
{
    public long Id { get; set; }

    public long CustomerProfileId { get; set; }

    public long GeneralCityId { get; set; }

    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Longitud { get; set; }

    public decimal Latitud { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual CustomerProfile CustomerProfile { get; set; } = null!;

    public virtual GeneralCity GeneralCity { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<SettingFreightPricingPerCustomer> SettingFreightPricingPerCustomers { get; set; } = new List<SettingFreightPricingPerCustomer>();

    public virtual ICollection<ShipmentFreight> ShipmentFreights { get; set; } = new List<ShipmentFreight>();
}
