using EventApi.Data.Contexts;
using EventApi.Repositories;
using EventApi.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
    x.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API"));

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.MapControllers();

app.Run();
