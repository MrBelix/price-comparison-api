using System.Text.RegularExpressions;

namespace PriceComparison.Domain.Common;


public record Slug
{
    private static readonly Regex ValidationRegex = new("^[a-z0-9]+(?:-[a-z0-9]+)*$");

    public string Value { get; init; }

    public Slug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Slug value cannot be null or empty");
        }

        if (!IsValidSlug(value))
        {
            throw new ArgumentException("Invalid slug format.");
        }

        Value = value;
    }

    private static bool IsValidSlug(string slug)
    {
        return ValidationRegex.IsMatch(slug);
    }

    public static Slug GenerateSlug(string input)
    {
        string slug = input.ToLower(); // Convert to lowercase
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // Remove invalid characters
        slug = Regex.Replace(slug, @"\s+", " ").Trim(); // Replace multiple spaces with single space
        slug = slug.Replace(" ", "-"); // Replace spaces with dashes
        slug = Regex.Replace(slug, @"-+", "-"); // Replace multiple dashes with single dash

        return new Slug(slug);
    }

    public override string ToString() => Value;
}