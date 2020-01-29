using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logika interakcji dla klasy EdytujCene.xaml
    /// </summary>
    public partial class EdytujCene : Window
    {
        DataRowView wybrane = null;
        public EdytujCene(DataRowView wybrane2)
        {
            wybrane = wybrane2;
            InitializeComponent();
            Medium.Text = (string)wybrane2.Row.ItemArray[1];
            Naleznosc.Text = ((Decimal)wybrane2.Row.ItemArray[2]).ToString();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Medium.Text.Length > 0 && (Double.TryParse(Naleznosc.Text, out double result)))
            {
                try
                {
                    var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                    using (var conn = new MySqlConnection(connstr))
                    {
                        conn.Open();
                        MySqlCommand command = new MySqlCommand("UPDATE `CennikOplat` SET `Medium`=@med,`CenaMedium`=@nal WHERE `CennikOplatID`=@cid", conn);
                        command.Parameters.Add("@cid", MySqlDbType.Int16).Value = (int)wybrane.Row.ItemArray[0];
                        command.Parameters.Add("@med", MySqlDbType.VarChar).Value = Medium.Text;
                        command.Parameters.Add("@nal", MySqlDbType.Decimal, 10).Value = Naleznosc.Text;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Cena uległa edycji.");
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
