using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;
using SherpaBackEnd.Serializers;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); })
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ITeamRepository, InMemoryTeamRepository>(
    _ => new InMemoryTeamRepository(new List<Team>()));
builder.Services.AddSingleton<ITeamService, TeamService>();

builder.Services.AddSingleton<ITemplateRepository, InMemoryFilesTemplateRepository>(_ =>
    new InMemoryFilesTemplateRepository("Templates"));
builder.Services.AddSingleton<ITemplateService, TemplateService>();

builder.Services.AddSingleton<IAssessmentRepository, InMemoryAssessmentRepository>();
builder.Services.AddSingleton<IAssessmentService, AssessmentService>();

builder.Services.AddSingleton<ISurveyRepository, InMemorySurveyRepository>();
builder.Services.AddSingleton<ISurveyService, SurveyService>();

builder.Services.AddSingleton<IEmailService, SesEmailService>(provider =>
{
    if (!builder.Environment.IsDevelopment())
    {
        return new SesEmailService(provider.GetService<IHttpContextAccessor>()!);
    }

    var accessKey = Environment.GetEnvironmentVariable("AWS_SES_ACCESS_KEY");
    var secretKey = Environment.GetEnvironmentVariable("AWS_SES_SECRET_KEY");
    return new SesEmailService(provider.GetService<IHttpContextAccessor>()!, accessKey!, secretKey!);
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();