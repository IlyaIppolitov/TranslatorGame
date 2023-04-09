using OpenAI;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TranslatorGame.Entities;
using OpenAI.Models.Images;
using System.Speech.Synthesis;
using Microsoft.EntityFrameworkCore;

namespace TranslatorGame
{
    public partial class GameWindow : UserControl
    {
        private AppDbContext _db;
        private string _categoryName;
        LanguageOptions _languageOptions;
        public Word QWord { get; set; }

        private OpenAiClient _client;
        private string? _key = Environment.GetEnvironmentVariable("openai_api_key");       

        public GameWindow(string category, LanguageOptions languageOptions)
        {
            InitializeComponent();
            _db = new AppDbContext();
            if (category is null)
                throw new ArgumentNullException("category");

            _categoryName = category;
            _languageOptions = languageOptions;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new ChoiceGameWindow();
        }

        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_key is not null)
            {
                _client = new OpenAiClient(_key);
            }
            else
            {
                throw new InvalidOperationException("Переменная окружения openai_api_key не задана!");
            }

            var words = await DbLanguageGamesAPI.GetWordByCategoryAsync(_categoryName);
            Random rnd = new Random();
            var countOfWords = words.Count;
            QWord = words.ToList()[rnd.Next(countOfWords)];       
            string guessWord = words.Where(w => w.Id == QWord.Id).Select(w => w.Russian).First();
            guessWorButton.Content = guessWord;


            //var imgBytes = await _client.GenerateImageBytes(guessWord, "guessWord", OpenAiImageSize._256);
            //Bitmap bmp;
            //BitmapImage btmImage;
            //using (var ms = new MemoryStream(imgBytes))
            //{
            //    bmp = new Bitmap(ms);
            //    btmImage = BitmapToImageSource(bmp);
            //    outputImage.Source = btmImage;
            //}

            var rightNumber = rnd.Next(4) + 1;            
            PutContentToButton(rightNumber, QWord);            
            var options = GetOptionsWords(QWord, words);

            int j = 0;
            for (int i = 1; i <= 4; ++i)
            {
                if (i == rightNumber) 
                {
                    continue; 
                }
                PutContentToButton(i, options[j]);
                j++;
            }
        }
        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        void PutContentToButton(int button, Word content)
        {
            string word;
            switch (_languageOptions)
            {
                case LanguageOptions.English:
                    word = content.English;
                    break;

                case LanguageOptions.German:
                    word = content.German;
                    break;

                default:
                    throw new Exception("Что-то пошло не так в части выбора языка!");
            }
            switch (button)
            {
                case 1:
                    answer1.Content = word;
                    break;
                case 2:
                    answer2.Content = word;
                    break;
                case 3:
                    answer3.Content = word;
                    break;
                case 4:
                    answer4.Content = word;
                    break;
            }
        }

        List<Word> GetOptionsWords(Word qWord, List<Word> words)
        {
            List<Word> options = new List<Word>();
            var countOfWords = words.Count;
            Random rnd = new Random();

            while (options.Count != 3)
            {
                var word = words[rnd.Next(countOfWords)];
                if (word.Id == qWord.Id) continue;
                var flag = false;
                foreach (var w in options)
                {
                    if (w.Id == word.Id) flag = true;
                }
                if (flag) continue;

                options.Add(word);
            }
            return options;
        }

        private async void Check_Answer_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            switch (_languageOptions)
            {
                case LanguageOptions.English:
                    if (button.Content == QWord.English)
                    {
                        MessageBox.Show("Верно угадали слово!");
                        Content = new GameWindow(_categoryName, _languageOptions);                       
                    }
                    else
                    {
                        MessageBox.Show("Вы неверно угадали слово!");
                        Content = new GameWindow(_categoryName, _languageOptions);
                    }
                    break;
                case LanguageOptions.German:
                    if (button!.Content == QWord.German) MessageBox.Show("Верно угадали слово!");
                    else
                    {
                        MessageBox.Show("Вы неверно угадали слово!");
                       
                        await _db.Words.AddAsync(QWord);
                        await _db.SaveChangesAsync();

                        Content = new GameWindow(_categoryName, _languageOptions);
                    }
                    break;

                default:
                    throw new Exception("Что-то пошло не так в части выбора языка!");
            }            
        }
    }
}
