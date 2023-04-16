using System.Windows;
using System.Windows.Controls;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    /// <summary>
    /// Логика взаимодействия для AutorizationUserControl.xaml
    /// </summary>
    public partial class AutorizationUserControl : UserControl
    {
        private DbLanguageGamesAPI _dbAPI = new DbLanguageGamesAPI();
        public AutorizationUserControl()
        {
            InitializeComponent();
        }

        private async void ComeIn_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordBox.Password.ToString();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                outputTextBlock.Text = "Заполните все данные";
                return; 
            }
            if (!_dbAPI.CheckPlayerExists(login))
            {
                outputTextBlock.Text = "Неверный логин и/или пароль";
            }
            else
            {
                Player player = new Player();
                player = await _dbAPI.GetPlayerAsync(login);

                if (player.Password!.ToString() == password)
                {
                    Content = new ChoiceGameWindow(player.Login);
                }
                else
                {
                    outputTextBlock.Text = "Неверный пароль и/или пароль";
                }
            }
        }

        private void Registration_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new RegistrationUserControl();
        }
    }
}
