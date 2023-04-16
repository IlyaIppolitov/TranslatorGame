using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TranslatorGame
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUserControl.xaml
    /// </summary>
    public partial class RegistrationUserControl : UserControl
    {
        private DbLanguageGamesAPI _dbAPI = new DbLanguageGamesAPI();
        public RegistrationUserControl()
        {
            InitializeComponent();
        }

        private async void Create_Account_Button_Click(object sender, RoutedEventArgs e)
        {
            string newLogin = newLoginTextBox.Text;
            string newPassword = newPasswordBox.Password.ToString();
            string newPasswordCheck = newPasswordBoxCheck.Password.ToString();

            if (string.IsNullOrWhiteSpace(newLogin)
                || string.IsNullOrWhiteSpace(newPassword)
                || string.IsNullOrWhiteSpace(newPasswordCheck))
            {
                outputPasswordTextBlock.Text = "Заполните все данные";
                return; 
            }

            if (_dbAPI.CheckPlayerExists(newLogin))
            {
                outputLoginTextBlock.Text = "Пользователь с таким логином уже существует";
            }
            else if (newPassword!= newPasswordCheck)
            {
                outputPasswordTextBlock.Text = "Пароли не совпадают";
            }
            else
            {
                await _dbAPI.AddNewPlayer(newLogin, newPassword);
                outputPasswordTextBlock.Text = "Пользователь добавлен";
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new AutorizationUserControl(); 
        }
    }
}
