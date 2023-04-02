using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public class DbLanguageGamesAPI
    {
        public static AppDbContext _db = new AppDbContext();
        public static async Task<List<Category>> GetCategoriesAsync()
        {
            var listCat = await _db.Categories.ToListAsync();

            if (listCat is null)
                throw new ArgumentNullException("Нет подключения к базе данных!");

            if (listCat.Count < 3)
                throw new ArgumentOutOfRangeException("Not enough categories for this UI!");

            return listCat;
        }

        public static async Task<List<Word>> GetWordByCategoryAsync(string _categoryName)
        {            
            return await _db.Words.Where(w => w.Category.Name == _categoryName).ToListAsync();
        }
    }
}
