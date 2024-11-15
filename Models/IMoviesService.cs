namespace MoviesAPI.Models
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetMovies(int page, int perPage, string title = null);
        Task<Movie> GetMovieById(string id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> UpdateMovieById(Movie movie, string id);
        Task DeleteMovieById(string id);
    }
}
