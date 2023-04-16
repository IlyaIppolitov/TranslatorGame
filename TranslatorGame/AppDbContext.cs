using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public class AppDbContext : DbContext
    {
        private const string dataBaseName = "LanguageGames.db";     
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            var appDir = "C:\\Users\\79053\\Desktop\\Курсач\\TranslatorGame";
            //var appDir = "D:\\ITStep\\CSharp\\EFCore\\TranslatorGame\\TranslatorGame";
            var ConnectionString = $"Data Source = {appDir}\\{dataBaseName}";
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<Word> Words => Set<Word>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
