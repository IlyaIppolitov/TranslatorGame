using System.Speech.Recognition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Recognition;

namespace TranslatorGame
{
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();

        public MainWindow()
        {
            InitializeComponent();           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Content = new AutorizationUserControl();
        }
        // Финализатор - вызывается Garbage collector
        ~MainWindow()
        {
            _db.Dispose();
        }
    }
}
