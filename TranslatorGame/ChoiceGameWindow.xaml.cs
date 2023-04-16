using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace TranslatorGame
{
    public partial class ChoiceGameWindow : UserControl
    {
        public DbLanguageGamesAPI dbApi = new DbLanguageGamesAPI();
        private string _login;
        public ChoiceGameWindow(string login)
        {
            _login = login;
            InitializeComponent();           
        }
        private async void ChoiceGameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loginTextBlock.Text = _login;
            //по умолчанию устанавливаем русский язык 
            languages.ItemsSource = Enum.GetValues(typeof(LanguageOptions));

            languages.SelectedIndex = 0;

            FillButtonsContentAsync();
        }

        private void Choose_Button_Click(object sender, RoutedEventArgs e)
        {    
            var category = ((Button)sender).Content as string;
            
            LanguageOptions languageOptions;
            if (Enum.TryParse<LanguageOptions>(languages.SelectedValue.ToString(), out languageOptions))
            {
                this.Content = new GameWindow(category, languageOptions, _login);
            }
            else { throw new ArgumentException("language not selected!"); }
        }

        private async void FillButtonsContentAsync()
        {
            //подзгружаем все категории с бд
            var categories = await dbApi.GetCategoriesAsync();

            chooseButton1.Content = categories.Select(c => c.Name).ToList()[1];
            chooseButton2.Content = categories.Select(c => c.Name).ToList()[0];
            chooseButton3.Content = categories.Select(c => c.Name).ToList()[2];
        }
    }
}
