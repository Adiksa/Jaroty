using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jaroty
{
    /// <summary>
    /// Logika interakcji dla klasy Creat.xaml
    /// </summary>
    public partial class Creat : Window
    {
        public Creat()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User.Visibility = Visibility.Hidden;
            if (UserGroup.SelectedIndex == 0)
                User.Visibility = Visibility.Visible;
        }
    }
}
