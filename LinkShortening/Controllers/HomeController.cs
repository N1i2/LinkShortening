using LinkShortening.Data;
using LinkShortening.Exceptions;
using LinkShortening.Models;
using LinkShortening.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortening.Controllers
{
    public class HomeController(LinkShorteningDbContext context, UrlShorteningService urlShorteningService, ILogger<HomeController> logger) : Controller
    {
        private readonly LinkShorteningDbContext _context = context;
        private readonly UrlShorteningService _urlShorteningService = urlShorteningService;
        private readonly ILogger<HomeController> _logger = logger;

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
                throw new Exception("URL is cannot be empty");
            }

            url.ClickCount++;
            _context.Update(url);
            await _context.SaveChangesAsync();

            return Redirect(url.OriginalUrl);
        }

        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string OriginalUrl)
        {
            if (ModelState.IsValid)
            {
                var shortCode = _urlShorteningService.GenerateUniqueShortUrl(_context);

                var shortUrl = ShortUrl.Create(OriginalUrl, shortCode);

                _context.ShortUrls.Add(shortUrl);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
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
