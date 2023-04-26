using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SherpaFrontEnd;
using SherpaFrontEnd.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SherpaBackEnd", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IGroupMemberService, InMemoryGroupMemberService>();
builder.Services.AddScoped<IGroupDataService, GroupServiceHttpClient>();
builder.Services.AddScoped<IAssessmentsDataService, AssessmentsServiceHttpClient>();

builder.Services.AddBlazoredModal();

await builder.Build().RunAsync();