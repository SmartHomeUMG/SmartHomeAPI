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

app.MapGet("/temperatures/alarm", (int temperature) => temperature >= 40);

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

<<<<<<< HEAD
app.MapPost("/householders/identify", async (SmartBuildingDb db, string code) => 
 await db.Homeholders.FirstAsync(hs => hs.IdentifyCode == code) != null);

app.MapHub<HomeConditionHub>("/HomeConditionHub");

=======
app.MapPost("/householders/identify/ishomeholder", (SmartBuildingDb db, string code) => smartBuilding.Helpers.HomeholderHelper.IsHomeHolder(db,code));
>>>>>>> f10374290de46c5a0e12f0f4eb90e7c502293350
app.Run();
