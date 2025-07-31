using LinkShortening.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortening.Data;

public class LinkShorteningDbContext(DbContextOptions<LinkShorteningDbContext> options) : DbContext(options)
{
    public DbSet<ShortUrl> ShortUrls { get; set; }
}
