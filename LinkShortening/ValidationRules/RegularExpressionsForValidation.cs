namespace LinkShortening.ValidationRules;

public record RegularExpressionsForValidation
{
    public const string ShortCodePattern = @"^[a-zA-Z0-9_-]+$";
}
