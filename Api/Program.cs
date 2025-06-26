using Api.Extensions;
using Api.Middleware;
using Serilog;
using Application;
using Application.CodeRoast.CodeRoastHandler;
using Domain.Repository.BugChaseRepositories;
using Domain.Repository.CodeRoastRepositories;
using Infrastructure;
using Infrastructure.BugChaseDbContext;
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

 builder.Services.AddDbContext<BugChaseDbContext>(opt =>
 opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 builder.Services.AddScoped<IBugChaseRepository, BugchaseRe >();
 builder.Services.AddSignalR();
 builder.Services.AddMediatR(typeof(UpdateScoreHandler));

 app.MapHub<BugChaseHub>("/bugchase-hub");
 app.MapBugChaseEndpoints();



app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseRateLimiter();

WebApiExtensions.RegisterEndpointDefinitions(app);

app.Run();