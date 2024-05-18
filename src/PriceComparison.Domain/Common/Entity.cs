namespace PriceComparison.Domain.Common;

public class Entity<TId>
{
    public TId Id { get; private set; }

    protected Entity(TId id)
    {
        Id = id;
    }
}