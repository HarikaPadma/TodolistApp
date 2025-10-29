using ToDoApp.Services;
using Microsoft.Azure.Cosmos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddSingleton<ToDoService>();
builder.Services.AddSingleton<CosmosDbService>(ServiceProvider => 
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    var cosmosDbConfig = configuration.GetSection("CosmosDb");
    var account = cosmosDbConfig["Account"];
    var key = cosmosDbConfig["Key"];
    var databaseName = cosmosDbConfig["DatabaseName"];
    var containerName = cosmosDbConfig["ContainerName"];

    var cosmosClient = new CosmosClient(account, key);
    
    // Here you would typically initialize and return your CosmosDbService
    return new CosmosDbService(cosmosClient, databaseName, containerName);
});

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();



app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();

app.Run();
