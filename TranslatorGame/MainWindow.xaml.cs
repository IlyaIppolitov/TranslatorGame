using System.Windows;


namespace TranslatorGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Content = new ChooiceGameWindow(); 
        }
    }
}
