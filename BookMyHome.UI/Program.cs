using BookMyHome.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    // Match the API HTTPS URL in BookMyHome.Api\Properties\launchSettings.json
    BaseAddress = new Uri("https://localhost:7269")
});
    
await builder.Build().RunAsync();
