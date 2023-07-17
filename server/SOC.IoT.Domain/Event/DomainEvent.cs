using SOC.IoT.Domain.Enum;

namespace SOC.IoT.Domain.Event;

public record DomainEvent<T>(DomainAction Action, T Item);

public record InitialDomainEvent<T> : DomainEvent<T>
{
    public InitialDomainEvent(T Item)
        : base(DomainAction.Initial, Item) { }
}

public record CreatedDomainEvent<T> : DomainEvent<T>
{
    public CreatedDomainEvent(T Item)
        : base(DomainAction.Created, Item) { }
}

public record UpdatedDomainEvent<T> : DomainEvent<T>
{
    public UpdatedDomainEvent(T Item)
        : base(DomainAction.Updated, Item) { }
}

public record DeletedDomainEvent<T> : DomainEvent<T>
{
    public DeletedDomainEvent(T Item)
        : base(DomainAction.Deleted, Item) { }
}
