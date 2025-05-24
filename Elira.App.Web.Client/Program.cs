using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Elira.App.Shared.Services;
using Elira.App.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Elira.App.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
