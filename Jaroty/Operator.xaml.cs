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
    /// Logika interakcji dla klasy Operator.xaml
    /// </summary>
    public partial class Operator : Window
    {
        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            Login logowanie = new Login();
            logowanie.Show();
            this.Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private int IdUser;

        public object MessageBoxButtons { get; private set; }

        public Operator(int ID)
        {
            IdUser = ID;
            InitializeComponent();
            Welcome.Text = "Zalogowano : operator systemu.";
        }

        private void Cennik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable user = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM Oplaty";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(user);
                    OplatyGr.ItemsSource = user.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable user = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM CennikOplat";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(user);
                    CennikGr.ItemsSource = user.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OplatyEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if(OplatyGr.SelectedItem as DataRowView != null)
            {
                EdytujOplate edytuj = new EdytujOplate(OplatyGr.SelectedItem as DataRowView);
                edytuj.ShowDialog();
            }
        }

        private void UsunOplate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = OplatyGr.SelectedItem as DataRowView;
            if (wybrane != null)
            {
                int id = (int)wybrane.Row.ItemArray[0];
                var result = MessageBox.Show("Czy na pewno chcesz usunąc tą opłatę?", "Usuń opłatę.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                        using (var conn = new MySqlConnection(connstr))
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `Oplaty` WHERE `OplatyID`=@id", conn);
                            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Usunieto oplatę.");
                                Cennik_Click(OplatyRefresh, e);
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

            }
        }

        private void DodajOplate_Click(object sender, RoutedEventArgs e)
        {
            DodajOplate oplata = new DodajOplate();
            oplata.ShowDialog();
        }

        private void UsunCennik_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = CennikGr.SelectedItem as DataRowView;
            if (wybrane != null)
            {
                int id = (int)wybrane.Row.ItemArray[0];
                var result = MessageBox.Show("Czy na pewno chcesz usunąc tą cenę?", "Usuń opłatę.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                        using (var conn = new MySqlConnection(connstr))
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `CennikOplat` WHERE `CennikOplatID`=@id", conn);
                            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Usunieto cenę.");
                                Cennik_Click(OplatyRefresh, e);
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

            }
        }

        private void DodajCennik_Click(object sender, RoutedEventArgs e)
        {
            DodajCene cena = new DodajCene();
            cena.ShowDialog();
        }

        private void CennikEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if (CennikGr.SelectedItem as DataRowView != null)
            {
                EdytujCene edytuj = new EdytujCene(CennikGr.SelectedItem as DataRowView);
                edytuj.ShowDialog();
            }
        }
    }
}
