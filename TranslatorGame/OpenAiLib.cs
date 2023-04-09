using OpenAI;
using OpenAI.Models.Images;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public class OpenAiLib
    {
        public OpenAiClient _client;

        private string? _key = Environment.GetEnvironmentVariable("openai_api_key");
        public OpenAiLib()
        {
            if (_key is not null)
            {
                _client = new OpenAiClient(_key);
            }
            else
            {
                throw new InvalidOperationException("Переменная окружения openai_api_key не задана!");
            }
        }

        public void StartOpenAiClient()
        {

        }
        public async Task<BitmapImage> GetPictureAsync(string descriprtion) 
        {
            var imgBytes = await _client.GenerateImageBytes(descriprtion, "guessWord", OpenAiImageSize._256);
            Bitmap bmp;
            BitmapImage btmImage;
            using (var ms = new MemoryStream(imgBytes))
            {
                bmp = new Bitmap(ms);
                btmImage = BitmapToImageSource(bmp);
            }
            return btmImage;
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
    }
}
