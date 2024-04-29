using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.EfcQueries;

public partial class Event
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Capacity { get; set; }

    public string? Start { get; set; }

    public string? End { get; set; }

    public string Status { get; set; } = null!;

    public string Visibility { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
}
