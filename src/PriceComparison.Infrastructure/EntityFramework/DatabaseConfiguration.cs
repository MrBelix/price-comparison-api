using JetBrains.Annotations;

namespace PriceComparison.Infrastructure.EntityFramework;

[UsedImplicitly]
public record DatabaseConfiguration(
    string Host,
    string Port,
    string Name,
    string User,
    string Password)
{
    public string GetConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Name};Username={User};Password={Password}";
    }
}