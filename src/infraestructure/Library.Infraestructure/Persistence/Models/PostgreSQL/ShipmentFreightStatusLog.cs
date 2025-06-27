using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class ShipmentFreightStatusLog
{
    public long Id { get; set; }

    public long ShipmentFreightId { get; set; }

    public long ShipmentFreightStatusId { get; set; }

    public string? Comments { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ShipmentFreight ShipmentFreight { get; set; } = null!;

    public virtual ShipmentFreightStatus ShipmentFreightStatus { get; set; } = null!;
}
