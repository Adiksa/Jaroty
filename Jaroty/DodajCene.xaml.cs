using MySql.Data.MySqlClient;
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
    /// Logika interakcji dla klasy DodajCene.xaml
    /// </summary>
    public partial class DodajCene : Window
    {
        public DodajCene()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Medium.Text.Length>0 && (Double.TryParse(Naleznosc.Text, out double result)))
            {
                try
                {
                    var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                    using (var conn = new MySqlConnection(connstr))
                    {
                        conn.Open();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `CennikOplat`(`Medium`, `CenaMedium`) VALUES (@med, @nal)", conn);
                        command.Parameters.Add("@med", MySqlDbType.VarChar).Value = Medium.Text;
                        command.Parameters.Add("@nal", MySqlDbType.Decimal,10).Value = Naleznosc.Text;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Dodano cenę.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("ERROR");
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola poprawnie.");
            }
        }
    }
}
