﻿using System;
using System.Collections.Generic;
using System.Linq;
using Database.DatabaseModels;
using Database.Repositories.Declarations;
using Models.Movies;

namespace Database.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviepickerContext _context;

        public MovieRepository(MoviepickerContext context)
        {
            _context = context;
        }

        public void LikeMovie(int userId, int movieId)
        {
            // retrieve movie from database
            // increment genres, actors, language and year
            // write to database
            // indicate movie has been rated by this user
            AdjustRating(userId, movieId, +1);
        }

        public void DislikeMovie(int userId, int movieId)
        {
            // retrieve movie from database
            // decrement genres, actors, language and year
            // write to database
            // indicate movie has been rated by this user
            AdjustRating(userId, movieId, -1);
        }

        public IEnumerable<Movie> GetMovies(int userId)
        {
            // get all preferences from user
            // order preferences by rating
            // search for movie constrained by the first preference
            // search for movie constrained by the first and second preferences
            // search for movie constrained by (year - 5), (year + 5) and first genre
            // For each movie that isn't inside the database: add it

            throw new NotImplementedException();
        }

        public void InsertOrUpdate(Genre genre)
        {
            var localGenre = _context.Genres.SingleOrDefault(x => x.TmdbId == genre.TmdbId);
            if (localGenre == null)
            {
                _context.Genres.Add(genre);
            }
            else
            {
                _context.Entry(localGenre).CurrentValues.SetValues(genre);
            }

            Console.WriteLine("{0} genre \"{1}\" with TMDb ID {2}", localGenre == null ? "Inserting" : "Updating", genre.Name, genre.TmdbId);
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Movie movie)
        {
            movie.Genres = new List<Genre>(InsertOrUpdate(movie.Genres, x => x.TmdbId));
            movie.Keywords = new List<Keyword>(InsertOrUpdate(movie.Keywords, x => x.Id));
            movie.Languages = new List<Language>(InsertOrUpdate(movie.Languages, x => x.Iso));
            movie.Backdrops = new List<BackdropImageInfo>(InsertOrUpdate(movie.Backdrops, x => x.Id));
            movie.Posters = new List<PosterImageInfo>(InsertOrUpdate(movie.Posters, x => x.Id));

            var localMovie = _context.Movies.SingleOrDefault(x => x.TmdbId == movie.TmdbId);
            if (localMovie == null)
            {
                movie.AddedOn = DateTime.UtcNow;
                _context.Movies.Add(movie);
            }
            else
            {
                _context.Entry(localMovie).CurrentValues.SetValues(movie);
                _context.Entry(localMovie).Property(x => x.AddedOn).IsModified = false;
            }

            movie.LastUpdatedOn = DateTime.UtcNow;
            Console.WriteLine("{0} movie \"{1}\" with TMDb ID {2}", localMovie == null ? "Inserting" : "Updating", movie.Title, movie.TmdbId);
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Show show)
        {
            show.Genres = new List<Genre>(InsertOrUpdate(show.Genres, x => x.TmdbId));
            show.Languages = new List<Language>(InsertOrUpdate(show.Languages, x => x.Iso));
            show.Backdrops = new List<BackdropImageInfo>(InsertOrUpdate(show.Backdrops, x => x.Id));
            show.Posters = new List<PosterImageInfo>(InsertOrUpdate(show.Posters, x => x.Id));

            var localShow = _context.Shows.SingleOrDefault(x => x.TmdbId == show.TmdbId);
            if (localShow == null)
            {
                show.AddedOn = DateTime.UtcNow;
                _context.Shows.Add(show);
            }
            else
            {
                _context.Entry(localShow).CurrentValues.SetValues(show);
                _context.Entry(localShow).Property(x => x.AddedOn).IsModified = false;
            }

            show.LastUpdatedOn = DateTime.UtcNow;
            Console.WriteLine("{0} show \"{1}\" with TMDb ID {2}", localShow == null ? "Inserting" : "Updating", show.Name, show.TmdbId);
            _context.SaveChanges();
        }

        private IEnumerable<T> InsertOrUpdate<T, TKey>(IEnumerable<T> entities, Func<T, TKey> idExpression) where T : class
        {
            foreach (var entity in entities)
            {
                var existingEntity = _context.Set<T>().Find(idExpression(entity));
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                    yield return existingEntity;
                }
                else
                {
                    _context.Set<T>().Add(entity);
                    yield return entity;
                }
            }
            _context.SaveChanges();
        }

        private void AdjustRating(int userId, int movieId, int change)
        {
            throw new NotImplementedException();
        }
    }
}