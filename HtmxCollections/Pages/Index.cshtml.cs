using Htmx;
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

    public async Task<IActionResult> OnPost()
    {
        await using var session = db.OpenSession();
        // overwrite existing shelf
        session.Store(Shelf);
        await session.SaveChangesAsync();
        return Partial("Shared/_Movies", this);
    }

    public async Task<IActionResult> OnPostAddMovie()
    {
        if (ModelState.IsValid)
        {
            await using var session = db.OpenSession();
            // there should be a record in the database, I seeded it
            var shelf = await session.LoadAsync<Shelf>(Shelf.Id);

            if (shelf == null) {
                return NotFound();
            }
            
            Shelf = shelf;
            Shelf.Movies.Insert(0, Movie);
            session.Store(Shelf);
            AddedMovieSuccessfully = true;
            
            await session.SaveChangesAsync();
            
            Response.Htmx(htmx => {
                htmx.Retarget("#movies");
                htmx.Trigger("movie:success");
            });
            
            ModelState.Clear();
            Movie = new Movie();
            return Partial("Shared/_Success", this);
        }
        
        Response.Htmx(htmx => {
            htmx.Retarget("#addMovie");
        });
        return Partial("Shared/_Movie", this);
    }
    
    [BindProperty]
    public Shelf Shelf { get; set; } = new();

    [BindProperty] 
    public Movie Movie { get; set; } = new();

    public bool AddedMovieSuccessfully { get; set; }
}