using LinkShortening.Data;
using LinkShortening.Models;
using LinkShortening.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortening.Controllers
{
    public class HomeController(LinkShorteningDbContext context, UrlShorteningService urlShorteningService) : Controller
    {
        private readonly LinkShorteningDbContext _context = context;
        private readonly UrlShorteningService _urlShorteningService = urlShorteningService;

        public async Task<IActionResult> Index()
        {
            var urls = await _context.ShortUrls.ToListAsync();
            return View(urls);
        }

        [HttpGet("/{shortUrl}")]
        public async Task<IActionResult> RedirectShortUrl(string shortUrl)
        {
            var url = await _context.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == shortUrl);

            if (url == null)
            {
                return NotFound();
            }

            url.ClickCount++;
            _context.Update(url);
            await _context.SaveChangesAsync();

            return Redirect(url.OriginalUrl);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OriginalUrl")] ShortUrl shortUrl)
        {
            if (ModelState.IsValid)
            {
                if (!Uri.TryCreate(shortUrl.OriginalUrl, UriKind.Absolute, out _))
                {
                    ModelState.AddModelError("OriginalUrl", "URL is invalid");
                    return View(shortUrl);
                }

                shortUrl.ShortCode = _urlShorteningService.GenerateUniqueShortUrl(_context);
                _context.Add(shortUrl);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(shortUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var shortUrl = await _context.ShortUrls.FindAsync(id);
            if (shortUrl == null)
            {
                return NotFound();
            }

            _context.ShortUrls.Remove(shortUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
