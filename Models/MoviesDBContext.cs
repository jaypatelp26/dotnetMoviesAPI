using MongoDB.Driver;

namespace MoviesAPI.Models
{
    public class MoviesDBContext
    {
        private readonly IMongoDatabase _database;
        public MoviesDBContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDbConnection"));
            _database = client.GetDatabase("sample_mflix");
        }
        public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("movies");
    }
}