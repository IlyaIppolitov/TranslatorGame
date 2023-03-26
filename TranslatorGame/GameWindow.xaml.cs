using System.Windows;
using System.Windows.Controls;

namespace TranslatorGame
{

    public partial class GameWindow : UserControl
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new ChooiceGameWindow();  
        }
    }
}
