using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public class AppDbContext : DbContext
    {
        //private const string dataBaseName = "LanguageGames.db";

        private const string dataBaseName = @"Data Source = C:\Users\79053\Desktop\Курсовая\TranslatorGame\LanguageGames.db";

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            var appDir = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..");
            //var appDir = "D:\\ITStep\\CSharp\\EFCore\\TranslatorGame";
            //var ConnectionString = $"Data Source = {appDir}\\{dataBaseName}";
            optionsBuilder.UseSqlite(dataBaseName);
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<Word> Words => Set<Word>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Dictionary> Dictionaries => Set<Dictionary>();
    }
}
