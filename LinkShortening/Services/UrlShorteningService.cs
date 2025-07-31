using LinkShortening.Data;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Text;

namespace LinkShortening.Services;

public class UrlShorteningService
{
    private const int ShortUrlLength = 8;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string GenerateShortUrl()
    {
        var random = new Random();
        var result = new StringBuilder(ShortUrlLength);

        for (int i = 0; i < ShortUrlLength; i++)
        {
            var randomIndex = random.Next(Alphabet.Length);
            result.Append(Alphabet[randomIndex]);
        }

        return result.ToString();
    }

    public string GenerateUniqueShortUrl(LinkShorteningDbContext context)
    {
        string shortCode;

        do
        {
            shortCode = GenerateShortUrl();
        } while (!context.ShortUrls.Any(s => s.ShortCode == shortCode));

        return shortCode;
    }
}