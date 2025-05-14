using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var movieDatabaseConfigSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);
var app = builder.Build();

app.MapGet("/", () => "Hello World!\r\n");


app.MapGet("/check", async (Microsoft.Extensions.Options.IOptions<DatabaseSettings> options) =>
{
    try
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var databases = await mongoClient.ListDatabaseNamesAsync();
        var databaseNames = await databases.ToListAsync();
        string responseString = "MongoDB connection ok: ";
        foreach (var db in databaseNames)
        {
            responseString += db + ", ";
        }
        return responseString.Remove(responseString.Length - 2);
    }
    catch (Exception ex)
    {
        return $"Error fetching databases: {ex.Message}";
    }
});

app.Run();
