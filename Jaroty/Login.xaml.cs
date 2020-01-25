using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreatAcc_Click(object sender, RoutedEventArgs e)
        {
            Creat tworzenie = new Creat();
            tworzenie.ShowDialog();
            
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            string login = UserLogin.Text;
            string password = Haslo.Password;
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM login WHERE Login=@log AND Password=@pas";
                    MySqlCommand command = new MySqlCommand(query,conn);
                    command.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
                    command.Parameters.Add("@pas", MySqlDbType.VarChar).Value = password;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        MessageBox.Show("correct");
                    }
                    else
                        MessageBox.Show("not correct");
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
