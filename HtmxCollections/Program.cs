using HtmxCollections.Models;
using Marten;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("marten");
builder.Services.AddMarten(o =>
{
    o.Connection(connectionString);
    o.AutoCreateSchemaObjects = AutoCreate.All;
});

var app = builder.Build();
// initialize database
var documentStore = app.Services.GetRequiredService<IDocumentStore>();
await DatabaseHelper.InitializeAsync(documentStore);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();