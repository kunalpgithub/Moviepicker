﻿using System;
using System.Configuration;
using System.Threading.Tasks;
using Database.DatabaseModels;
using Database.Repositories;
using Nito.AsyncEx;

namespace DataService
{
    public class DataService
    {
        public DataService(MovieRepository movieRepository)
        {
            MovieRepository = movieRepository;
        }

        private MovieRepository MovieRepository { get; set; }

        public static void Main(string[] args)
        {
            // Step 1: movie genres
            // Step 2: show genres
            // Step 3: get a movie
            // Step 3.1: get the movie's keywords
            // Step 3.2: get the movie's images
            // Step 4: get a show
            // Step 4.1: get the show's keywords
            // Step 4.2: get the show's images

            //try
            //{
            AsyncContext.Run(() => MainAsync(args));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            Console.Read();
        }

        private static async Task MainAsync(string[] args)
        {
            var dataService = new DataService(new MovieRepository(new MoviepickerContext()));
            await dataService.StartScraper().ConfigureAwait(false);
            Console.WriteLine("Data inserted");
        }

        public async Task StartScraper()
        {
            var scraper = new DataScraper(ConfigurationManager.AppSettings["apikey"], MovieRepository);
            await scraper.UpdateGenresAsync();
            await scraper.UpdateMoviesAsync();
        }
    }
}