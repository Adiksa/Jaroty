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
    /// Logika interakcji dla klasy Wspolnota.xaml
    /// </summary>
    public partial class Wspolnota : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private int IdUser;
        DataTable user = new DataTable();
        public Wspolnota(int ID)
        {
            IdUser = ID;
            InitializeComponent();
            Welcome.Text = "Zalogowano : pracownik wspólnoty.";
        }
        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            Login logowanie = new Login();
            logowanie.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Visibility.Hidden;
            Zuzycie.Visibility = Visibility.Visible;
            StatusOplat.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            DoOplat.Visibility = Visibility.Hidden;
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable oplaty = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "";
                    if(ZuzId.Text.Length>0&&ZuzMId.Text.Length>0)
                    {
                        query = "Select Medium, Zuzycie FROM Oplaty WHERE WlascicielMieszkaniaID=@id AND MieszkanieId=@mid";   
                    }
                    if (ZuzId.Text.Length > 0 && ZuzMId.Text.Length == 0)
                    {
                        query = "Select MieszkanieId, Medium, Zuzycie FROM Oplaty WHERE WlascicielMieszkaniaID=@id";
                    }
                    if (ZuzId.Text.Length == 0 && ZuzMId.Text.Length > 0)
                    {
                        query = "Select WlascicielMieszkaniaID, Medium, Zuzycie FROM Oplaty WHERE MieszkanieId=@mid";
                    }
                    if(ZuzId.Text.Length == 0 && ZuzMId.Text.Length == 0)
                    {
                        query = "Select WlascicielMieszkaniaID, MieszkanieId, Medium, Zuzycie FROM Oplaty";
                    }
                    MySqlCommand command = new MySqlCommand(query, conn);
                    if (ZuzId.Text.Length > 0 && ZuzMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = ZuzId.Text;
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = ZuzMId.Text;
                    }
                    if (ZuzId.Text.Length > 0 && ZuzMId.Text.Length == 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = ZuzId.Text;
                    }
                    if (ZuzId.Text.Length == 0 && ZuzMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = ZuzMId.Text;
                    }
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

        private void StatusO_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Visibility.Hidden;
            Zuzycie.Visibility = Visibility.Hidden;
            StatusOplat.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Visible;
            DoOplat.Visibility = Visibility.Hidden;
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable oplaty = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "";
                    if (SOId.Text.Length > 0 && SOMId.Text.Length > 0)
                    {
                        query = "Select CzyOplacone, OplatyID, Medium, Naleznosc  FROM Oplaty WHERE WlascicielMieszkaniaID=@id AND MieszkanieId=@mid ";
                    }
                    if (SOId.Text.Length > 0 && SOMId.Text.Length == 0)
                    {
                        query = "Select CzyOplacone, OplatyID, MieszkanieId, Medium, Naleznosc FROM Oplaty WHERE WlascicielMieszkaniaID=@id";
                    }
                    if (SOId.Text.Length == 0 && SOMId.Text.Length > 0)
                    {
                        query = "Select CzyOplacone, OplatyID, WlascicielMieszkaniaID, Medium, Naleznosc FROM Oplaty WHERE MieszkanieId=@mid ";
                    }
                    if (SOId.Text.Length == 0 && SOMId.Text.Length == 0)
                    {
                        query = "Select CzyOplacone, OplatyID, WlascicielMieszkaniaID, MieszkanieId, Medium, Naleznosc FROM Oplaty";
                    }
                    MySqlCommand command = new MySqlCommand(query, conn);
                    if (SOId.Text.Length > 0 && SOMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = SOId.Text;
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = SOMId.Text;
                    }
                    if (SOId.Text.Length > 0 && SOMId.Text.Length == 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = SOId.Text;
                    }
                    if (SOId.Text.Length == 0 && SOMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = SOMId.Text;
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(oplaty);
                    SOplaty.ItemsSource = oplaty.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DoPlatyButton_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Visibility.Hidden;
            Zuzycie.Visibility = Visibility.Hidden;
            StatusOplat.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Visible;
            DoOplat.Visibility = Visibility.Visible;

            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    DataTable oplaty = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "";
                    if (SKId.Text.Length > 0 && SKMId.Text.Length > 0)
                    {
                        query = "Select OplatyID, Medium, Naleznosc FROM Oplaty WHERE WlascicielMieszkaniaID=@id AND MieszkanieId=@mid AND CzyOplacone=false ";
                    }
                    if (SKId.Text.Length > 0 && SKMId.Text.Length == 0)
                    {
                        query = "Select OplatyID, MieszkanieId, Medium, Naleznosc FROM Oplaty WHERE WlascicielMieszkaniaID=@id AND CzyOplacone=false";
                    }
                    if (SKId.Text.Length == 0 && SKMId.Text.Length > 0)
                    {
                        query = "Select OplatyID, WlascicielMieszkaniaID, Medium, Naleznosc FROM Oplaty WHERE MieszkanieId=@mid AND CzyOplacone=false";
                    }
                    if (SKId.Text.Length == 0 && SKMId.Text.Length == 0)
                    {
                        query = query = "Select OplatyID, WlascicielMieszkaniaID, MieszkanieId, Medium, Naleznosc FROM Oplaty WHERE CzyOplacone=false";
                    }
                    MySqlCommand command = new MySqlCommand(query, conn);
                    if (SKId.Text.Length > 0 && SKMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = SKId.Text;
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = SKMId.Text;
                    }
                    if (SKId.Text.Length > 0 && SKMId.Text.Length == 0)
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int16).Value = SKId.Text;
                    }
                    if (SKId.Text.Length == 0 && SKMId.Text.Length > 0)
                    {
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = SKMId.Text;
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(oplaty);
                    DOplaty.ItemsSource = oplaty.DefaultView;
                    double total = 0.0;
                    if ((SKId.Text.Length > 0 || SKMId.Text.Length > 0) && oplaty.Rows.Count > 0)
                    {
                        total += Convert.ToDouble(oplaty.Compute("SUM(Naleznosc)", string.Empty));
                        DOplatyLacznie.Text = "Łączna kwota do opłaty : " + total.ToString() + " zł.";
                    }
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
            Zuzycie.Visibility = Visibility.Hidden;
            StatusOplat.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Hidden;
            DoOplat.Visibility = Visibility.Hidden;
        }

        private void Autoryzuj_Click(object sender, RoutedEventArgs e)
        {
            DataRowView wybrane = SOplaty.SelectedItem as DataRowView;
            if (wybrane != null)
            {
                int idoplaty = (int)wybrane.Row.ItemArray[1];
                bool oplacone = (bool)wybrane.Row.ItemArray[0];
                if(oplacone)
                {
                    MessageBox.Show("Faktura jest oplacona");
                }
                else
                {
                    try
                    {
                        var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                        using (var conn = new MySqlConnection(connstr))
                        {
                            conn.Open();
                            DataTable table = new DataTable();
                            MySqlDataAdapter adapter = new MySqlDataAdapter();
                            MySqlCommand command = new MySqlCommand("UPDATE Oplaty SET CzyOplacone=true WHERE OplatyID=@id", conn);
                            command.Parameters.Add("@id", MySqlDbType.Int16).Value = idoplaty;
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Potwierdzono autorzyacje.");
                                conn.Close();
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
    }
}
