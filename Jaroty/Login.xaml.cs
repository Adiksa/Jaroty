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
                        DataTable table3 = new DataTable();
                        MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                        String query3 = "Select * FROM login WHERE Login=@log";
                        MySqlCommand command3 = new MySqlCommand(query3, conn);
                        command3.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
                        adapter3.SelectCommand = command;
                        adapter3.Fill(table3);
                        int ID = table3.Rows[0].Field<int>(0);
                        int usertype = table3.Rows[0].Field<int>(3);
                        if(usertype == 0)
                        {
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            String query2 = "Select Ulica,NrBloku,NrMieszkania,IdMieszkania FROM Mieszkanie WHERE IdWlasciciela=@id";
                            MySqlCommand command2 = new MySqlCommand(query2, conn);
                            command2.Parameters.Add("@id", MySqlDbType.Int16).Value = ID;
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            if (table2.Rows.Count == 0)
                            {
                                DodajMieszkanie dodaj = new DodajMieszkanie(ID);
                                this.Close();
                                dodaj.Show();
                            }
                            if (table2.Rows.Count > 1)
                            {
                                WybierzMieszkanie wybierz = new WybierzMieszkanie(table2, ID);
                                this.Close();
                                wybierz.Show();
                            }
                        }
                    }
                    else
                        MessageBox.Show("Błędna nazwa użytkownika lub hasło.");
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
