using System.ComponentModel.DataAnnotations;
using Marten;

namespace HtmxCollections.Models;

public class Shelf
{
    public int Id { get; set; }
    public List<Movie> Movies { get; set; }
        = new List<Movie>();
}

public class Movie
{
    [Required] public string Title { get; set; }
    [Required] public int Year { get; set; }
    [Required] public string Genre { get; set; }
    [Required] public string ImageUrl { get; set; }
}

public static class DatabaseHelper
{
    private static readonly Shelf Initial = new()
    {
        Movies =
        {
            new() {Genre = "Drama,Romance", Title = "Forest Gump", Year = 1994, ImageUrl = "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_UY209_CR2,0,140,209_AL_.jpg"},
            new() {Genre = "Action", Title = "The Dark Knight", Year = 2008, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_UY209_CR0,0,140,209_AL_.jpg"},
            new() {Genre = "Horror", Title = "The Shining", Year = 1980, ImageUrl = "https://m.media-amazon.com/images/M/MV5BZWFlYmY2MGEtZjVkYS00YzU4LTg0YjQtYzY1ZGE3NTA5NGQxXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_UX140_CR0,0,140,209_AL_.jpg" },
            new() {Genre = "Crime", Title = "Se7en", Year = 1995, ImageUrl = "https://m.media-amazon.com/images/M/MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_UX140_CR0,0,140,209_AL_.jpg"},
            new() {Genre = "Drama", Title = "Fight Club", Year = 1999, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_UX140_CR0,0,140,209_AL_.jpg" }
        }
    };
    
    public static async Task InitializeAsync(IDocumentStore store)
    {
        await using var session = store.OpenSession();
        var anyRecords = await session.Query<Shelf>().AnyAsync();
        if (!anyRecords) {
            session.Store(Initial);
            await session.SaveChangesAsync();
        }
    }
}