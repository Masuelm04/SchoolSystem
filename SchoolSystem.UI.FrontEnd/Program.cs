using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SchoolSystem.UI.FrontEnd;
using SchoolSystem.UI.FrontEnd.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7041/") });

builder.Services.AddScoped<AsistenciaService>();
builder.Services.AddScoped<CalificacionService>();
builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<EstadoAsistenciaService>();
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<MateriaService>();

builder.Services.AddBlazorStrap();

await builder.Build().RunAsync();
