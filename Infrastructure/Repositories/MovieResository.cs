using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieResository : Repository<Movie>, IMovieRepository
    {
        public MovieResository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            //select top 30 * from movies order by revenue desc
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            //SELECT TOP 30 * FROM Reviews r INNER JOIN Movies m ON r.MovieId = m.Id ORDER BY AverageRating DESC;
            var movies = await _dbContext.Reviews
                .GroupBy(r => r.MovieId)
                .Select(g => new
                {
                    MovieId = g.Key,
                    AverageRating = g.Average(r => r.Rating)
                })
                .OrderByDescending(g => g.AverageRating)
                .Take(30)
                .Join(
                    _dbContext.Movies,
                    g => g.MovieId,
                    m => m.Id,
                    (g, m) => new Movie
                    {
                        Id = m.Id,
                        Title = m.Title,
                        PosterUrl = m.PosterUrl,
                        Rating = g.AverageRating
                    }
                )
                .ToListAsync();

            return movies;
        }


        //public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        //{
        //    var movies = await _dbContext.Movies
        //        .OrderByDescending(m => m.Rating)
        //        .Take(30)
        //        .ToListAsync();

        //    return movies;
        //}


        public async override Task<Movie> GetById(int id)
        {
            //var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            //var movie = await _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre).Include(m => m.Trailers).Include(m => m.CastsOfMovie).ThenInclude(mc => mc.Cast).Include(m => m.UsersOfReview).FirstOrDefaultAsync(m => m.Id == id);
            //if (movie != null && movie.UsersOfReview != null && movie.UsersOfReview.Any())
            //{
            //    movie.Rating = movie.UsersOfReview.Average(r => r.Rating);
            //}

            var movie = await _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            movie.Rating = await _dbContext.Reviews.Where(m => m.MovieId == id).AverageAsync(m => m.Rating);
           
            return movie;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var totalMoviessCountByGenre = await _dbContext.MovieGenres.
                Where(m => m.GenreId == genreId).
                CountAsync();
            if (totalMoviessCountByGenre == 0)
            {
                throw new Exception("No Movies Found For That Genre");
            }
            var movies = await _dbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .Include(m => m.Movie)
                .OrderBy(m => m.MovieId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(mg => mg.Movie)
                .ToListAsync();
            PagedResultSet<Movie> pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviessCountByGenre);
            return pagedMovies;
        }
    }
}
