﻿using Microsoft.EntityFrameworkCore;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public class AppDbContext : DbContext
    {
        private const string directory = "D:\\ITStep\\CSharp\\EFCore\\TranslatorGame\\LanguageGames.db";
        private const string ConnectionString = $"Data Source = {directory}";

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<Word> Words => Set<Word>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Dictionary> Dictionaries => Set<Dictionary>();
    }
}
