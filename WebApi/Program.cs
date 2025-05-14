using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!\r\n");


var mongoClient = new MongoClient("mongodb://gbs:geheim@mongodb:27017");
app.MapGet("/check", async () =>
{
    try
    {
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
