using ApplicationCore.Contracts.Repositories;
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
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();
            var movieCards = new List<MovieCard>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCard { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            };
            return movieCards;

        }
    }
}
