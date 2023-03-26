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
    }
}
