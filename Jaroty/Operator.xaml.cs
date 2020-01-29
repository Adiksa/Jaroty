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
            Home.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            CennikStackPanel.Visibility = Visibility.Visible;
            BazaMieszkan.Visibility = Visibility.Hidden;
            BazaWlascicieli.Visibility = Visibility.Hidden;
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
            Home.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Hidden;
            CennikStackPanel.Visibility = Visibility.Hidden;
            BazaMieszkan.Visibility = Visibility.Hidden;
            BazaWlascicieli.Visibility = Visibility.Hidden;
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
                var result = MessageBox.Show("Czy na pewno chcesz usunąć tą opłatę?", "Usuń opłatę.", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                var result = MessageBox.Show("Czy na pewno chcesz usunąć tą cenę?", "Usuń opłatę.", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

        private void Mieszkania_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            CennikStackPanel.Visibility = Visibility.Hidden;
            BazaMieszkan.Visibility = Visibility.Visible;
            BazaWlascicieli.Visibility = Visibility.Hidden;
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable user = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM Mieszkanie";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(user);
                    MieszkaniaGr.ItemsSource = user.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Wlasciciele_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            CennikStackPanel.Visibility = Visibility.Hidden;
            BazaMieszkan.Visibility = Visibility.Hidden;
            BazaWlascicieli.Visibility = Visibility.Visible;
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable user = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM WlascicielMieszkania";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(user);
                    WlascicieleGr.ItemsSource = user.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void MieszEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if (MieszkaniaGr.SelectedItem as DataRowView != null)
            {
                EdytujMieszkanie edytuj = new EdytujMieszkanie(MieszkaniaGr.SelectedItem as DataRowView);
                edytuj.ShowDialog();
            }
        }

        private void UsunMiesz_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = MieszkaniaGr.SelectedItem as DataRowView;
            if (wybrane != null)
            {
                int id = (int)wybrane.Row.ItemArray[0];
                var result = MessageBox.Show("Czy na pewno chcesz usunąć te mieszkanie?", "Usuń Mieszkanie.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                        using (var conn = new MySqlConnection(connstr))
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM Mieszkanie WHERE IdMieszkania=@id", conn);
                            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Usunieto Mieszkania.");
                                Mieszkania_Click(Wlasciciele, e);
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

        private void DodajMiesz_Click(object sender, RoutedEventArgs e)
        {
            DodajMieszkanieOp home = new DodajMieszkanieOp();
            home.ShowDialog();
        }

        private void WlascicieleEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if (WlascicieleGr.SelectedItem as DataRowView != null)
            {
                EdytujWlasciciela edytuj = new EdytujWlasciciela(WlascicieleGr.SelectedItem as DataRowView);
                edytuj.ShowDialog();
            }
        }

        private void UsunWlasc_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = WlascicieleGr.SelectedItem as DataRowView;
            if (wybrane != null)
            {
                int id = (int)wybrane.Row.ItemArray[0];
                var result = MessageBox.Show("Czy na pewno chcesz usunąć tego właściciela?", "Usuń właściciela.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                        using (var conn = new MySqlConnection(connstr))
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `WlascicielMieszkania` WHERE `IdWlasciciela`=@id", conn);
                            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Usunieto wlasciciela.");
                                Wlasciciele_Click(Wlasciciele, e);
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

        private void DodajWlasc_Click(object sender, RoutedEventArgs e)
        {
            Creat tworzy = new Creat();
            tworzy.UserGroup.SelectedIndex = 0;
            tworzy.UserGroup.Visibility = Visibility.Hidden;
            tworzy.GU.Visibility = Visibility.Hidden;

            tworzy.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Wlasciciele_Click(Wlasciciele, e);
        }
    }
}
