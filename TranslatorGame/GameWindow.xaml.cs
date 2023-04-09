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
using System.Threading.Tasks;
using System.ComponentModel;

namespace TranslatorGame
{
    public partial class GameWindow : UserControl
    {
        public DbLanguageGamesAPI dbApi = new DbLanguageGamesAPI();
        
        public OpenAiLib AiLib = new OpenAiLib();

        private WordProvider _provider;

        private List<Word> _dictionaryWords;

        private List<Word> _playerWords;

        private string _categoryName;
        public string _playerLogin = "Champion";
        private int _rightNumber;
        LanguageOptions _languageOptions;
        public Word QWord { get; set; }


        public GameWindow(string category, LanguageOptions languageOptions)
        {
            InitializeComponent();

            _categoryName = category;
            _languageOptions = languageOptions;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new ChoiceGameWindow();
        }

        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // определить имеющийся перечень слов из базы данных
            _dictionaryWords = await dbApi.GetWordByCategoryAsync(_categoryName);

            _playerWords = await dbApi.GetPlayerWords(_playerLogin, _categoryName);

            _provider = new WordProvider(new[]
            {
                (_dictionaryWords, 1.0),
                (_playerWords, 0.6)
            });
            //await dbApi.AddNewPlayer(_playerLogin, " ");
            FillAllButtons();
        }

        private void FillAllButtons()
        {
            QWord = _provider.Take(1).First();
            QWord = _provider.Take(1).First();


            guessWorButton.Content = QWord.Russian;

            Random rnd = new Random();

            _rightNumber = rnd.Next(4) + 1;
            PutContentToButton(_rightNumber, QWord);
            var options = GetOptionsWords(QWord, _dictionaryWords);

            int j = 0;
            for (int i = 1; i <= 4; ++i)
            {
                if (i == _rightNumber)
                {
                    continue;
                }
                PutContentToButton(i, options[j]);
                j++;
            }

            //outputImage.Source = await AiLib.GetPictureAsync(QWord.English);
        }

        void PutContentToButton(int buttonNumber, Word word)
        {
            string wordString;
            switch (_languageOptions)
            {
                case LanguageOptions.English:
                    wordString = word.English;
                    break;

                case LanguageOptions.German:
                    wordString = word.German;
                    break;

                default:
                    throw new Exception("Что-то пошло не так в части выбора языка!");
            }
            switch (buttonNumber)
            {
                case 1:
                    answer1.Content = wordString;
                    break;
                case 2:
                    answer2.Content = wordString;
                    break;
                case 3:
                    answer3.Content = wordString;
                    break;
                case 4:
                    answer4.Content = wordString;
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
            if (CheckRightButton((Button)sender))
            {
                MessageBox.Show("Молодец!");
                FillAllButtons();
            }
            else
            {
                MessageBox.Show("Промазал! Мы всё запишем и вернёмся!");
                await dbApi.AddWordToPlayerAsync(_playerLogin, QWord);

                 FillAllButtons();

                //Content = new GameWindow(_categoryName, _languageOptions);
            }        
        }

        private bool CheckRightButton(Button button)
        {
            switch (_rightNumber)
            {
                case 1:
                    return answer1.Name == button.Name;
                case 2:
                    return answer2.Name == button.Name;
                case 3:
                    return answer3.Name == button.Name;
                case 4:
                    return answer4.Name == button.Name;

                default:
                    throw new Exception("Что-то пошло не так в части выбора кнопок!");
            }
        }

    }
}
