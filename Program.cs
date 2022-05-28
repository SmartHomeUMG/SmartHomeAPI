using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;
using smartBuilding.Hubs;
using smartBuilding.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SmartBuilding") ?? "Data Source=smartbuilding.db";

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<SmartBuildingDb>(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddCors( options => {
   options.AddPolicy("CorsPolicy",builder => builder
   .WithOrigins("192.168.100.132:28096")
   .AllowAnyHeader()
   .AllowAnyMethod()
   .AllowCredentials()
   );
});
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo {
      Title = "Smarthome API",
      Description = "Smarthome project - home remote control ",
      Version = "v1" });
});

//register own services
builder.Services.AddScoped<IHomeConditionRepository, HomeConditionRepository>();

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Home API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

var webSocketOptions = new WebSocketOptions
{
   KeepAliveInterval = TimeSpan.FromMinutes(2) // send ping every 2min to have connection assurance 
};

app.UseWebSockets(webSocketOptions);

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/temperatures/alarm", (int temperature) => temperature >= 40);

app.MapHub<HomeConditionHub>("/HomeConditionHub");

app.Run();
