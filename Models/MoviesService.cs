using MongoDB.Driver;

namespace MoviesAPI.Models
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesDBContext _dbContext;
        public MoviesService(MoviesDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movie>> GetMovies(int page, int perPage, string title = null)
        {
            var filter = title != null ? Builders<Movie>.Filter.Regex("Title", new MongoDB.Bson.BsonRegularExpression(title, "i")) : Builders<Movie>.Filter.Empty;

            return await _dbContext.Movies.Find(filter).Sort(Builders<Movie>.Sort.Ascending(m => m.Year)).Skip((page - 1) * perPage).Limit(perPage).ToListAsync();
        }

        public async Task<Movie> GetMovieById(string id)
        {
            return await _dbContext.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            await _dbContext.Movies.InsertOneAsync(movie);
            return movie;
        }

        public async Task<Movie> UpdateMovieById(Movie movie, string id)
        {
            await _dbContext.Movies.ReplaceOneAsync(m => m.Id == id, movie);
            return movie;
        }

        public async Task DeleteMovieById(string id)
        {
            await _dbContext.Movies.DeleteOneAsync(m => m.Id == id);
        }
    }
}
