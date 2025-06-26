using Api.Extensions;
using Api.Middleware;
using Serilog;
using Application;
using Application.CodeRoast.CodeRoastHandler;
using Domain.Repository.CodeRoastRepositories;
using Infrastructure;
using Infrastructure.Persistence.Configuration.CodeRoastConfiguration;
using Infrastructure.Persistence.Repositories.CodeRoastRepositories;

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

 builder.Services.Configure<OpenAiSettings>(builder.Configuration.GetSection("OpenAi"));
 builder.Services.AddScoped<ICodeRoastRepository, CodeRoastRepository>();
 builder.Services.AddHttpClient();
 builder.Services.AddMediatR(typeof(CodeRoastHandler));


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseRateLimiter();

WebApiExtensions.RegisterEndpointDefinitions(app);

app.Run();