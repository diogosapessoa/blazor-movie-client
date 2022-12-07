using Blazored.LocalStorage;

using BlazorMovieClient;
using BlazorMovieClient.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Omdb", client => client.BaseAddress = new Uri("https://www.omdbapi.com"));

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IMovieFinder, OmdbFinder>();

await builder.Build().RunAsync();
