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
using System;
using TranslatorGame.Entities;
using System.Linq;
using System.Text.RegularExpressions;
using TranslatorGame.ValueObjects;

namespace TranslatorGame
{
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();

        public MainWindow()
        {
            InitializeComponent();
            SpeechRecognizer speechRecognizer = new SpeechRecognizer();

            var dict = _db.Dictionaries.ToList();
            dic.ItemsSource = dict; 
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Content = new ChoiceGameWindow();

            var cat = _db.Categories.ToList();
            dic.ItemsSource = cat; 

        }
        // Финализатор - вызывается Garbage collector
        ~MainWindow()
        {
            _db.Dispose();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            //Word w = new Word()
            //{
            //    RussianText = rus.Text,
            //    EnglishText = eng.Text,
            //    GermanText = ger.Text,
            //    Dictionary = (Dictionary)dic.SelectedItem
            //};

            //_db.Dictionaries.Remove((Dictionary)dic.SelectedItem);
            //_db.SaveChanges();
            //rus.Clear();
            //eng.Clear();
            //ger.Clear(); 

            //var _cat = (Category)dic.SelectedItem;
            //_cat.RussianName = rus.Text;
            //_cat.EnglishName = eng.Text;
            //_cat.GermanName = ger.Text; 

            //await _db.SaveChangesAsync();

        }

        private void dic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rus.Text = dic.SelectedItem.ToString();
            
        }
    }
}
