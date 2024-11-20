using MongoDB.Driver;

namespace MoviesAPI.Models
{
    public class MoviesDBContext
    {
        private readonly IMongoDatabase _database;
        public MoviesDBContext(IConfiguration config)
        {
            DotNetEnv.Env.Load();
            var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            //var client = new MongoClient(config.GetConnectionString("MONGO_CONNECTION_STRING"));
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("sample_mflix");
        }
        public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("movies");
    }
}