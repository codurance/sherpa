using System.Text.Json;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos;
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
builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddScoped<ITeamDataService, TeamServiceHttpClient>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IToastNotificationService, BlazoredToastService>();
builder.Services.AddScoped<ICookiesService, CookiesService>();
builder.Services.AddScoped<ICachedResponseService, LocalStorageCachedResponseService>();
builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

// POC
var client = new HttpClient(){BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)};
var httpRequestMessage =
    new HttpRequestMessage(HttpMethod.Get, $"/survey/44177fd8-685f-4a13-9c88-e2a75f84eaec");

var httpResponseMessage = await client.SendAsync(httpRequestMessage);
var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

var result = JsonSerializer.Deserialize<SurveyWithoutQuestions>(responseString,
    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
Console.WriteLine("ClientID: " + builder.Configuration["Cognito:ClientId"]);
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration["Cognito:Authority"];
    options.ProviderOptions.ClientId = builder.Configuration["Cognito:ClientId"];
    options.ProviderOptions.RedirectUri = builder.HostEnvironment.BaseAddress + builder.Configuration["Cognito:RedirectUri"];
    options.ProviderOptions.PostLogoutRedirectUri = builder.HostEnvironment.BaseAddress + builder.Configuration["Cognito:PostLogoutRedirectUri"];
    options.ProviderOptions.ResponseType = "code";
});
await builder.Build().RunAsync();