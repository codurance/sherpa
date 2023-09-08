using System.Text.Json;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SherpaFrontEnd;
using SherpaFrontEnd.Serializers;
using SherpaFrontEnd.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SherpaBackEnd", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddScoped<ITeamMemberService, InMemoryTeamMemberService>();
builder.Services.AddScoped<ITeamDataService, TeamServiceHttpClient>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddBlazoredModal();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Cognito", options.ProviderOptions);
});

await builder.Build().RunAsync();