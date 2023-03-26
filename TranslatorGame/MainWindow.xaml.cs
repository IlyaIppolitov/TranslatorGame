using System.Windows;


namespace TranslatorGame
{
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();
        public MainWindow()
        {
            var players = _db.Players;
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Content = new ChooiceGameWindow();
        }
        // Финализатор - вызывается Garbage collector
        ~MainWindow()
        {
            _db.Dispose();
        }
    }
}
