using HtmxCollections.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxCollections.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDocumentStore db;

    public IndexModel(ILogger<IndexModel> logger, IDocumentStore db)
    {
        _logger = logger;
        this.db = db;
    }

    public async Task OnGet()
    {
        await using var session = db.LightweightSession();
        // there should be a record in the database, I seeded it
        Shelf = await session.Query<Shelf>().FirstAsync();
    }

    public async Task<IActionResult> OnPost([FromForm] Shelf shelf)
    {
        await using var session = db.OpenSession();
        // overwrite existing shelf
        session.Store(shelf);
        await session.SaveChangesAsync();

        Thread.Sleep(TimeSpan.FromSeconds(1));

        return Partial("Shared/_Movies", shelf);
    }

    public Shelf Shelf { get; set; } = new();
}