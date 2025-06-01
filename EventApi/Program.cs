using EventApi.Data.Contexts;
using EventApi.Handlers;
using EventApi.Repositories;
using EventApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(x =>
//{
//    x.EnableAnnotations();
//    x.ExampleFilters();
//});
//builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();
builder.Services.AddMemoryCache();

builder.Services. AddScoped(typeof(ICacheHandler<>), typeof(CacheHandler<>));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>(); 

//builder.Services.AddScoped<IEventService, EventService>();
//builder.Services.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

//app.MapOpenApi();
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.UseAuthentication();
//app.UseCors(x => x
//    .AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader());

//app.UseSwagger();
//app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API"));

//app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));


app.MapGrpcService<EventService>();
app.MapGrpcService<CategoryService>();
app.MapGrpcService<StatusService>();
app.MapGrpcService<LocationService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
