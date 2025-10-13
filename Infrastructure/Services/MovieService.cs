using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = new List<MovieCard>
            {
                new MovieCard { Title = "Inception", Id = 1, PosterUrl = "https://image.tmdb.org/t/p/w342/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg" },
                new MovieCard { Title = "Interstellar", Id = 2, PosterUrl = "https://image.tmdb.org/t/p/w342/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg" },
                new MovieCard { Title = "The Dark Knight", Id = 3, PosterUrl = "https://image.tmdb.org/t/p/w342/qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
                new MovieCard { Title = "Deadpool", Id = 4, PosterUrl = "https://image.tmdb.org/t/p/w342/yGSxMiF0cYuAiyuve5DA6bnWnBw.jpg" },
                new MovieCard { Title = "The Avengers", Id = 5, PosterUrl = "https://image.tmdb.org/t/p/w342/RYMX2wcKCBAr24UyPD7xwmjaTn.jpg" }
            };
            return movies;
        }
    }
}
