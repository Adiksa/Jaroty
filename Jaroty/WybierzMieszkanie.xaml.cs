using System;
using System.Data;
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
    /// Logika interakcji dla klasy WybierzMieszkanie.xaml
    /// </summary>
    public partial class WybierzMieszkanie : Window
    {
        DataTable table;
        int id;
        public WybierzMieszkanie(DataTable mieszkania, int ID)
        {
            table = mieszkania;
            id = ID;
            InitializeComponent();
            Mieszkania.ItemsSource = table.DefaultView;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Login logowanie = new Login();
            logowanie.Show();
            this.Close();
        }

        private void Wybierz_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = Mieszkania.SelectedItem as DataRowView;
            int userhomeid = (int) wybrane.Row.ItemArray[3];
            Uzytkownik home = new Uzytkownik(id, userhomeid);
            home.Show();
            this.Close();
        }
    }
}
