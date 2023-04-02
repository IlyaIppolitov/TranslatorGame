using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TranslatorGame.Entities;

namespace TranslatorGame
{
    public partial class ChoiceGameWindow : UserControl
    {
        //в это части кода очень много повторов, нужно будет смодернизировать
        private AppDbContext _db = new AppDbContext();
        List<Category> categories = new List<Category>();
        public string category;
        public ChoiceGameWindow()
        {
            InitializeComponent();           
        }
        private async void ChoiceGameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //подзгружаем все категории с бд
            categories = _db.Categories.ToList();
                                                 
            //по умолчанию устанавливаем русский язык 
            languages.SelectedIndex = 0;
        }

        private void Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            category = "Медицина";
            this.Content = new GameWindow(category);
        }
        private void Animals_Button_Click(object sender, RoutedEventArgs e)
        {         
            category = "Животные";
            this.Content = new GameWindow(category);
        }

        private void IT_Button_Click(object sender, RoutedEventArgs e)
        {          
            category = "IT";
            this.Content = new GameWindow(category);
        }
        private void languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var categories = _db.Categories.ToList();
            var animalsCategory = categories[0];
            var medicineCategory = categories[1];         
            var ITCategory = categories[2];

        }

       
    }
}
