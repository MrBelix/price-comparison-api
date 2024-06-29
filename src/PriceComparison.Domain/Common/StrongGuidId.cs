namespace PriceComparison.Domain.Common;

public abstract record StrongGuidId<T>(Guid Value)
where T : StrongGuidId<T>
{
    public static T New => (T)Activator.CreateInstance(typeof(T), Guid.NewGuid())!;

    public static T Empty => (T)Activator.CreateInstance(typeof(T), Guid.Empty)!;
}