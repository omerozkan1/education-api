using education_application;
using education_domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TrainingDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<ITrainingProgramService, TrainingProgramService>();
builder.Services.AddScoped<ITrainingEfRepository, TrainingEfRepository>();
builder.Services.AddScoped<ITrainingProgramEfRepository, TrainingProgramEfRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>((n) => ConnectionMultiplexer.Connect(builder.Configuration["RedisSettings:Host"]));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = builder.Configuration["id"], Version = builder.Configuration["version"] });
});

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var context = serviceScope.ServiceProvider.GetService<TrainingDbContext>();
context.Database.EnsureCreated();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Configuration["id"]} {builder.Configuration["version"]}");
});
app.Run();
