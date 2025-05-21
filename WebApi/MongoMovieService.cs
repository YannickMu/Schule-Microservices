using Microsoft.Extensions.Options;
using MongoDB.Driver;
public class MongoMovieService : IMovieService
{

    private string connectionString;
// Constructor.
// Settings werden per dependency injection übergeben.
    public MongoMovieService(IOptions<DatabaseSettings> options)
    {
        connectionString = options.Value.ConnectionString;
    }
    public async Task<string> Check()
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var databases = await mongoClient.ListDatabaseNamesAsync();
            List<string> databaseNames = await databases.ToListAsync();
            string responseString = "MongoDB connection ok: " + string.Join(", ", databaseNames);
            return responseString;
        }
        catch (Exception ex)
        {
            return $"Error fetching databases: {ex.Message}";
        }
    }

    public async void Create(Movie movie)
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mydatabase");
            var movieCollection = database.GetCollection<Movie>("movies");

            await movieCollection.InsertOneAsync(movie);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    public IEnumerable<Movie> Get()
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mydatabase");
            var movieCollection = database.GetCollection<Movie>("movies");

            var filter = Builders<Movie>.Filter.Empty;
            return movieCollection.Find(filter).ToList();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            return [];
        } 
    }
    public Movie Get(string id)
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mydatabase");
            var movieCollection = database.GetCollection<Movie>("movies");

            var filter = Builders<Movie>.Filter.Eq("_id", id);
            var found = movieCollection.Find(filter).FirstOrDefault();
            return found;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            return new Movie();
        } 
    }
    public void Update(string id, Movie movie)
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mydatabase");
            var movieCollection = database.GetCollection<Movie>("movies");

            var filter = Builders<Movie>.Filter.Eq("_id", id);
            var update = Builders<Movie>.Update
                .Set(movie => movie.Title, movie.Title)
                .Set(movie => movie.Year, movie.Year)
                .Set(movie => movie.Summary, movie.Summary)
                .Set(movie => movie.Actors, movie.Actors);
            movieCollection.UpdateOne(filter, update);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        } 
    }
    public void Remove(string id)
    {
        try
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mydatabase");
            var movieCollection = database.GetCollection<Movie>("movies");

            var filter = Builders<Movie>.Filter.Eq("_id", id);
            movieCollection.DeleteOne(filter);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        } 
    }
}