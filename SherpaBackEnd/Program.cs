using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Serializers;
using SherpaBackEnd.Shared.Services;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Application;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); })
    .AddNewtonsoftJson();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ITeamRepository, MongoTeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();

builder.Services.AddSingleton<ITemplateRepository, MongoTemplateRepository>();
builder.Services.AddScoped<ITemplateService, TemplateService>();

builder.Services.AddSingleton<ISurveyRepository, MongoSurveyRepository>();
builder.Services.AddScoped<ISurveyService, SurveyService>();

builder.Services.AddScoped<IEmailService, SesEmailService>(provider =>
{
    RegionEndpoint clientConfig = builder.Environment.IsDevelopment()? RegionEndpoint.EUCentral1 : RegionEndpoint.EUWest1;
    string defaultEmail = builder.Environment.IsDevelopment()? "paula.masutier@codurance.com" : "sherpa@codurance.com";
    
    var accessKey = Environment.GetEnvironmentVariable("AWS_SES_ACCESS_KEY");
    var secretKey = Environment.GetEnvironmentVariable("AWS_SES_SECRET_KEY");
    var basicAwsCredentials = new BasicAWSCredentials(accessKey, secretKey);
    var amazonEmailServiceClient = new AmazonSimpleEmailServiceClient(basicAwsCredentials, clientConfig);
    return new SesEmailService(amazonEmailServiceClient, defaultEmail);
});

builder.Services.AddSingleton<ISurveyNotificationsRepository, MongoSurveyNotificationRepository>();
builder.Services.AddScoped<IEmailTemplateFactory, EmailTemplateFactory>(provider => new EmailTemplateFactory(provider.GetService<IHttpContextAccessor>()!));
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<ISurveyNotificationService, SurveyNotificationService>();

builder.Services.AddScoped<ISurveyResponsesFileService, SurveyResponsesCsvFileService>();

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