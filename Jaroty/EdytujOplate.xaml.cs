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
    
    public partial class EdytujOplate : Window
    {
        DataTable cennik = new DataTable();
        DataTable wlasciel = new DataTable();
        DataTable mieszkania = new DataTable();
        DataRowView wybrane2 = null;
        int IDoplata = 0;
        public EdytujOplate(DataRowView wybrane)
        {
            wybrane2 = wybrane;
            IDoplata = (int)wybrane.Row.ItemArray[0];
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "Select * FROM CennikOplat";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(cennik);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                    String query2 = "Select * FROM WlascicielMieszkania";
                    MySqlCommand command2 = new MySqlCommand(query2, conn);
                    adapter2.SelectCommand = command2;
                    adapter2.Fill(wlasciel);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            InitializeComponent();
            foreach (DataRow row in cennik.Rows)
            {
                IdMedium.Items.Add(row.Field<String>(1));
                if (row.Field<int>(0).Equals((int)wybrane.Row.ItemArray[1]))
                    IdMedium.SelectedItem = row.Field<String>(1);
            }
            IdMedium.SelectedIndex = 0;
            foreach (DataRow row in wlasciel.Rows)
            {
                IdUzytkownika.Items.Add(row.Field<int>(0));
                if (row.Field<int>(0).Equals((int)wybrane.Row.ItemArray[2]))
                    IdUzytkownika.SelectedItem = row.Field<int>(0);
            }
            foreach (DataRow row in mieszkania.Rows)
            {
                if (row.Field<int>(0).Equals((int)wybrane2.Row.ItemArray[3]))
                {
                    IdMieszkania.SelectedItem = row.Field<int>(0);
                }
            }
            Zuzcie.Text = ((int)wybrane.Row.ItemArray[6]).ToString();
            if((bool)wybrane.Row.ItemArray[7] == true)
            {
                czy.IsChecked = true;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (IdUzytkownika.SelectedIndex != -1 && IdMieszkania.SelectedIndex != -1 && Zuzcie.Text.Length > 0 && (int.TryParse(Zuzcie.Text, out int result)))
            {
                try
                {
                    var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                    using (var conn = new MySqlConnection(connstr))
                    {
                        conn.Open();
                        MySqlCommand command = new MySqlCommand("UPDATE `Oplaty` SET `CennikOplatID`=@cid,`WlascicielMieszkaniaID`=@wid,`MieszkanieId`=@mid,`Zuzycie`=@z,`CzyOplacone`=@b WHERE `OplatyID`=@oid", conn);
                        command.Parameters.Add("@oid", MySqlDbType.Int16).Value = ((int)wybrane2.Row.ItemArray[0]).ToString();
                        command.Parameters.Add("@cid", MySqlDbType.Int16).Value = cennik.Rows[IdMedium.SelectedIndex].Field<int>(0).ToString();
                        command.Parameters.Add("@wid", MySqlDbType.Int16).Value = wlasciel.Rows[IdUzytkownika.SelectedIndex].Field<int>(0).ToString();
                        command.Parameters.Add("@mid", MySqlDbType.Int16).Value = mieszkania.Rows[IdMieszkania.SelectedIndex].Field<int>(0).ToString();
                        command.Parameters.Add("@z", MySqlDbType.Int16).Value = Zuzcie.Text;
                        if((bool)czy.IsChecked)
                        {
                            command.Parameters.AddWithValue("@b", 1);
                        }
                        else
                            command.Parameters.AddWithValue("@b", 0);
                        MySqlCommand command2 = new MySqlCommand("UPDATE Oplaty SET Medium = (SELECT Medium FROM CennikOplat WHERE Oplaty.CennikOplatID = CennikOplat.CennikOplatID)", conn);
                        MySqlCommand command3 = new MySqlCommand("UPDATE Oplaty SET Naleznosc = (SELECT CenaMedium FROM CennikOplat WHERE Oplaty.CennikOplatID = CennikOplat.CennikOplatID) * Oplaty.Zuzycie", conn);
                        if (command.ExecuteNonQuery() > 0 && command2.ExecuteNonQuery() >= 0 && command3.ExecuteNonQuery() >= 0)
                        {
                            MessageBox.Show("Opłata uległa edycji.");
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
                MessageBox.Show("Wypełnij wszystkie pola.");
            }
        }
        private void IdUzytkownika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    String query = "SELECT `IdMieszkania` FROM `Mieszkanie` WHERE `IdWlasciciela`=@cid";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.Add("@cid", MySqlDbType.Int16).Value = IdUzytkownika.SelectedItem.ToString();
                    adapter.SelectCommand = command;
                    mieszkania.Clear();
                    adapter.Fill(mieszkania);
                    conn.Close();
                    IdMieszkania.Items.Clear();
                    if (mieszkania != null)
                    {
                        foreach (DataRow row in mieszkania.Rows)
                        {
                            IdMieszkania.Items.Add(row.Field<int>(0));
                        }
                        IdMieszkania.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }

        private void Zuzcie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(Zuzcie.Text, out int result) && Zuzcie.Text.Length > 0)
            {
                Zuzcie.Text = "";
                MessageBox.Show("Błędna wartość.");
            }
        }
    }
}
