using OpenAI;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TranslatorGame
{
    public partial class GameWindow : UserControl
    {
        public string Category { get; set; }
        private OpenAiClient _client;
        private string? _key = Environment.GetEnvironmentVariable("openai_api_key");
        public GameWindow()
        {
            InitializeComponent();
        }
        public GameWindow(string category)
        {      
            InitializeComponent();
            if (category is not null)
            {
                Category = category;
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new ChoiceGameWindow();
        }

        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (_key is not null)
            //{
            //    _client = new OpenAiClient(_key);
            //}
            //else
            //{
            //    throw new InvalidOperationException("Переменная окружения openai_api_key не задана!");
            //}

            //var image = new BitmapImage();
            //int options = 10;//количество слов, которое будет выводиться на экране
            //то есть вывели 1 картинку, нажали вариант ответа, затем 2 и так далее до 10
            //for (int i = 0; i < options; i++)
            //{
            //    //var img = await _client.GenerateImageBytes("женщина");
            //    //image = BitmapToImageSource(img);
            //}
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
    }
}
