using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class GeneralErrorLog
{
    public long Id { get; set; }

    public string? Project { get; set; }

    public string? Class { get; set; }

    public string? Method { get; set; }

    public int? LineNumber { get; set; }

    public string? ErrorDescription { get; set; }

    public DateTime? CreatedDate { get; set; }
}
