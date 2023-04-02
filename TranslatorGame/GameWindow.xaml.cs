using OpenAI;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public partial class GameWindow : UserControl
    {
        private string _categoryName;
        LanguageOptions _languageOptions;

        private OpenAiClient _client;
        private string? _key = Environment.GetEnvironmentVariable("openai_api_key");
        //public GameWindow()
        //{
        //    InitializeComponent();
        //}
        public GameWindow(string category, LanguageOptions languageOptions)
        {      
            InitializeComponent();
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
            var words = await DbLanguageGamesAPI.GetWordByCategoryAsync(_categoryName);
            Random rnd = new Random();

            var countOfWords = words.Count;

            var qWord = words.ToList()[rnd.Next(countOfWords)];

            guessWord.Content = words.Where(w => w.Id == qWord.Id).Select(w => w.Russian);

            var rightNumber = rnd.Next(4) + 1;

            PutContentToButton(rightNumber, qWord);

            var options = GetOptionsWords(qWord, words);

            int j = 0;
            for (int i = 1; i <= 4; ++i)
            {
                if (i == rightNumber) {++i; continue;}

                PutContentToButton(i, options[++j]);
            }


        }

        private BitmapImage BitmapToImageSource(byte[] img)//метод, который картинку из байтов
                                                           //конвертирует в BitmapImage
        {
            if (img.Length == 0) return null; //выбросить исключение
            var image = new BitmapImage();
            using (var mem = new MemoryStream(img))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
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




        //private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (_key is not null)
        //    {
        //        _client = new OpenAiClient(_key);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Переменная окружения openai_api_key не задана!");
        //    }

        //    var image = new BitmapImage();
        //    int options = 10;//количество слов, которое будет выводиться на экране
        //    то есть вывели 1 картинку, нажали вариант ответа, затем 2 и так далее до 10
        //    for (int i = 0; i < options; i++)
        //    {
        //        //var img = await _client.GenerateImageBytes("женщина");
        //        //image = BitmapToImageSource(img);
        //    }
        //}


    }
}
