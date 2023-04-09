using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TranslatorGame.Entities;
using TranslatorGame.ValueObjects;

namespace TranslatorGame
{
    public class DbLanguageGamesAPI
    {
        public AppDbContext _db = new AppDbContext();
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var listCat = await _db.Categories.ToListAsync();

            if (listCat is null)
                throw new ArgumentNullException("Нет подключения к базе данных!");

            if (listCat.Count < 3)
                throw new ArgumentOutOfRangeException("Not enough categories for this UI!");

            return listCat;
        }

        // Получить слова по категории
        public async Task<List<Word>> GetWordByCategoryAsync(string _categoryName)
        {            
            return await _db.Words.Where(w => w.Category.Name == _categoryName).ToListAsync();
        }

        // Добавить нового игрока
        public async Task AddNewPlayer(string login, string password)
        {
            if (CheckPlayerExists(login))
            {
                MessageBox.Show("Пользователь с таким именем уже существует!");
                return;
            }                

            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Login = login,
                Password = new Password(password)
            };

            await _db.Players.AddAsync(player);
            await _db.SaveChangesAsync();
        }

        // Проверить существование игрока
        public bool CheckPlayerExists(string login)
        {
            return _db.Players.Any(p => p.Login == login);
        }

        // Получить плохие слова игрока
        public async Task<List<Word>> GetPlayerWords(string login, string _categoryName)
        {
            var player = await _db.Players.Include(p => p.Words).Where(p => p.Login == login).FirstAsync();
            if (player.Words == null)
                return new List<Word>();
            return player.Words;
        }

        public async Task AddWordToPlayerAsync(string login, Word word)
        {
            var player = await _db.Players.Include(p => p.Words).Where(p => p.Login == login).FirstAsync();
            if (player.Words.Where(w => w.Id == word.Id).Count() == 0)
            {
                player.Words.Add(word);
                await _db.SaveChangesAsync();
            }
        }
    }
}
