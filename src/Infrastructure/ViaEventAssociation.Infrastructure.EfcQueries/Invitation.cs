using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.EfcQueries;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public string GuestId { get; set; } = null!;

    public string? EventId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Event? Event { get; set; }

    public virtual Guest Guest { get; set; } = null!;
}
