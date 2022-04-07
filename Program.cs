using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SmartBuilding") ?? "Data Source=smartbuilding.db";
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<SmartBuildingDb>(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo {
      Title = "Smarthome API",
      Description = "Smarthome project - home remote control ",
      Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Home API V1");
});

 app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.MapGet("/temperatures", async (SmartBuildingDb db) => await db.Temperatures.ToListAsync());
app.MapGet("/temperatures/recents", async (SmartBuildingDb db) => await db.Temperatures.Where(t => t.MeasureDate <= DateTime.Now).ToListAsync());
//localhost:5108/temperatures/period/2022-04-01,2022-04-06
app.MapGet("/temperatures/period/{start:datetime},{stop:datetime}", async(SmartBuildingDb db, DateTime start, DateTime stop) => await db.Temperatures.Where(t => t.MeasureDate < stop && t.MeasureDate > start).ToListAsync());

HomeTemperature recent = null;

app.MapPost("/temperatures", async (SmartBuildingDb db, HomeTemperature temperature) => {
   temperature.MeasureDate = DateTime.Now;
   await db.Temperatures.AddAsync(temperature);
   await db.SaveChangesAsync();
   recent = temperature;
   return Results.Created($"",temperature);
});

app.MapGet("/temperatures/recent", () => recent);

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

app.Run();
