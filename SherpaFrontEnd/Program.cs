using System.Text.Json;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITeamDataService, TeamServiceHttpClient>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ICookiesService, CookiesService>();
builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration["Cognito:Authority"];
    options.ProviderOptions.ClientId = builder.Configuration["Cognito:ClientId"];
    options.ProviderOptions.RedirectUri = builder.HostEnvironment.BaseAddress + builder.Configuration["Cognito:RedirectUri"];
    options.ProviderOptions.PostLogoutRedirectUri = builder.HostEnvironment.BaseAddress + builder.Configuration["Cognito:PostLogoutRedirectUri"];
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();