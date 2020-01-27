using System;
using MySql.Data.MySqlClient;
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
    /// Logika interakcji dla klasy Uzytkownik.xaml
    /// </summary>
    public partial class Uzytkownik : Window
    {
        private int IdUser;
        private int IdMieszkania;
        DataTable user = new DataTable();
        public Uzytkownik(int ID, int IDmiesz)
        {
            IdUser = ID;
            IdMieszkania = IDmiesz;
            InitializeComponent();
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM WlascicielMieszkania WHERE IdWlasciciela=@id";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.Int16).Value = IdUser;
                    adapter.SelectCommand = command;
                    adapter.Fill(user);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Welcome.Text = "Zalogowano Użytkownika : " + user.Rows[0].Field<String>(3) + " " + user.Rows[0].Field<String>(4);
        }
            private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            Login logowanie = new Login();
            logowanie.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable oplaty = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select Medium, Zuzycie FROM Oplaty WHERE WlascicielMieszkaniaID=@id AND MieszkanieId=@mid ";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@id", MySqlDbType.Int16).Value = IdUser;
                    command.Parameters.Add("@mid", MySqlDbType.Int16).Value = IdMieszkania;
                    adapter.SelectCommand = command;
                    adapter.Fill(oplaty);
                    Oplaty.ItemsSource = oplaty.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
