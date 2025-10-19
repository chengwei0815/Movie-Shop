using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCard>> Get30HighestGrossingMovies();
        Task<MovieDetailModel> GetMovieDetails(int id);
        List<MovieCard> Get30HighestRatedMovies();
    }
}
