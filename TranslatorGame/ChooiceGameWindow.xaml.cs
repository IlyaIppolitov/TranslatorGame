using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TranslatorGame
{
    public partial class ChooiceGameWindow : UserControl
    {
        public ChooiceGameWindow()
        {
            InitializeComponent();
        }

        private void Choice_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new GameWindow(); 
        }
    }
}
