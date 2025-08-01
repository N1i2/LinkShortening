using LinkShortening.Exceptions;
using LinkShortening.Exceptions.Factory;
using LinkShortening.ValidationRules;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LinkShortening.Models;

public class ShortUrl : BaseModel
{
    public string OriginalUrl { get; set; }
    public string ShortCode { get; set; }
    public DateTime CreateDate { get; set; }
    public uint ClickCount { get; set; }

    private const uint StartClickCount = 0;

    private ShortUrl() { }

    public static ShortUrl Create(string originalUrl, string shortCode)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
        {
            throw ExceptionFactory.CreateEmptyUrlException<ModelException>();
        }
        if(!Uri.IsWellFormedUriString(originalUrl, UriKind.Absolute))
        {
            throw ExceptionFactory.CreateInvalidUrlFormatException<ModelException>(originalUrl);
        }

        if (string.IsNullOrWhiteSpace(shortCode))
        {
            throw new EmptyShortCodeException();
        }
        if (!Regex.IsMatch(shortCode, RegularExpressionsForValidation.ShortCodePattern))
        {
            throw new InvalidShortCodeFormatException(shortCode);
        }

        return new ShortUrl
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreateDate = DateTime.Now,
            ClickCount = StartClickCount
        };
    }
}
