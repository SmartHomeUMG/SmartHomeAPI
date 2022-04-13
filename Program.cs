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
builder.Services.AddCors( options => {
   options.AddPolicy("CorsPolicy",builder => builder
   .WithOrigins("http://localhost:28096")
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

app.MapControllers();

app.MapGet("/", () => "Hello World!");
//localhost:5108/temperatures/period/2022-04-01,2022-04-06
app.MapGet("/temperatures/period/{start:datetime},{stop:datetime}", async(SmartBuildingDb db, DateTime start, DateTime stop) => await db.Temperatures.Where(t => t.MeasureDate < stop && t.MeasureDate > start).ToListAsync());

app.MapPost("/homeholders/person/add", async (SmartBuildingDb db, Householders hs) =>{
   await db.Homeholders.AddAsync(hs);
   await db.SaveChangesAsync();
   return Results.Accepted();
});

app.MapPost("/homeholders/group/add", async (SmartBuildingDb db, IEnumerable<Householders> hsList) => {
   await db.Homeholders.AddRangeAsync(hsList);
   await db.SaveChangesAsync();
   return Results.Accepted();
});

app.MapPost("/householders/identify", async (SmartBuildingDb db, string code) => 
 await db.Homeholders.FirstAsync(hs => hs.IdentifyCode == code) != null);

app.MapHub<HomeConditionHub>("/HomeConditionHub");

app.Run();
