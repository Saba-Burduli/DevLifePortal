using System.Net.Http.Headers;
using Api.EndpointDefinitions.BugChaseEndpointDefinitions;
using Api.EndpointDefinitions.DevDatingEndpointDefinition;
using Api.EndpointDefinitions.EscapeMeetingEndPointDefinitions;
using Api.EndpointDefinitions.GitHubPersonalityEndpointDefinition;
using Api.EndpointDefinitions.ShowCodeEndpointDefinitions;
using Api.Extensions;
using Api.Hubs.BugChaseHub;
using Api.Middleware;
using Serilog;
using Application;
using Application.BugChase.BugChaseCommands;
using Application.CodeRoast.CodeRoastHandler;
using Application.CodeRoast.RoastCodeCommand;
using Application.DevDating.CreateProfileCommands;
using Application.EscapeMeeting.GenerateExcuseQueries;
using Application.GitHubPersonality.AnalyzeGitHubRepoCommands;
using Application.ShowCode.AnalyzeRepoQueries;
using Domain.Repository.BugChaseRepositories;
using Domain.Repository.CodeRoastRepositories;
using Domain.Repository.DevDatingRepositories;
using Domain.Repository.EscapeMeetingRepositories;
using Domain.Repository.GitHubPersonalityRepositories;
using Domain.Repository.ShowCodeRepositories;
using Infrastructure;
using Infrastructure.BugChaseDbContext;
using Infrastructure.DevDatingDbContext;
using Infrastructure.DevDatingRepository;
using Infrastructure.GitHubPersonalityRepository;
using Infrastructure.Persistence.Configuration.CodeRoastConfiguration;
using Infrastructure.Persistence.Repositories.BugChaseRepositories;
using Infrastructure.Persistence.Repositories.CodeRoastRepositories;
using Infrastructure.Persistence.Repositories.EscapeMeetingRepositories;
using Infrastructure.Persistence.Repositories.ShowCodeRepositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

RateLimiterServiceCollectionExtensions.AddRateLimiter(builder.Services);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Code Roast Project : 
 builder.Services.Configure<OpenAiSettings>(builder.Configuration.GetSection("OpenAi"));
 builder.Services.AddScoped<ICodeRoastRepository, CodeRoastRepository>();
 builder.Services.AddHttpClient();
 builder.Services.AddSignalR();
 builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
 builder.Services.AddMediatR(typeof(RoastCodeHandler));

// Bug Chase Game Project :
 builder.Services.AddDbContext<BugChaseDbContext>(opt =>
 opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
 builder.Services.AddScoped<IBugChaseRepository, BugChaseRepository >();
 builder.Services.AddSignalR();
 builder.Services.AddMediatR(typeof(UpdateScoreHandler));

 app.MapHub<BugChaseHub>("/bugchase-hub");
 app.MapBugChaseEndpoints();

//Show code Project :
 builder.Services.AddHttpClient<IShowCodeRepository, ShowCodeRepository>(client => {
     client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DevLife", "1.0"));
     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 });
 builder.Services.AddScoped<IShowCodeRepository, ShowCodeRepository>();
 builder.Services.AddMediatR(typeof(AnalyzeRepoHandler));
 app.MapShowCodeEndpoints();

//Dev Dating Project :
 builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
 builder.Services.AddScoped<IDevDatingRepository, DevDatingRepository>();
 builder.Services.AddHttpClient();
 builder.Services.AddMediatR(typeof(CreateProfileHandler));
 app.MapDevDatingEndpoints();

//Escape Meeting Project :
 builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
 builder.Services.AddScoped<IEscapeMeetingRepository, EscapeMeetingRepository>();
 builder.Services.AddMediatR(typeof(GenerateExcuseHandler));
 app.MapEscapeMeetingEndpoints();



app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseRateLimiter();

WebApiExtensions.RegisterEndpointDefinitions(app);

app.Run();