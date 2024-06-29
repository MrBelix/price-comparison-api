namespace PriceComparison.Application.Authentication;

public record AccessToken
{
    public string Access { get; init; }

    public string Refresh { get; init; }

    public DateTimeOffset Expires { get; init; }

    public AccessToken(string access, string refresh, DateTimeOffset expires)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(access, nameof(access));
        ArgumentException.ThrowIfNullOrWhiteSpace(refresh, nameof(refresh));

        Access = access;
        Refresh = refresh;
        Expires = expires;
    }
}