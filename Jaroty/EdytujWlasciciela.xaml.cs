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
    /// Logika interakcji dla klasy EdytujWlasciciela.xaml
    /// </summary>
    public partial class EdytujWlasciciela : Window
    {
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsAlphaNumeric(string strToCheck)
        {
            return strToCheck.All(char.IsLetterOrDigit);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        DataRowView wybrane = null;
        public EdytujWlasciciela(DataRowView wybrane2)
        {
            wybrane = wybrane2; 
            InitializeComponent();
            DataUrodzinUzytkownika.SelectedDate = (DateTime)wybrane.Row.ItemArray[1];
            EmailUzytkownika.Text = (string)wybrane.Row.ItemArray[2];
            ImieUzytkownika.Text = (string)wybrane.Row.ItemArray[3];
            NazwiskoUzytkownika.Text = (string)wybrane.Row.ItemArray[4];
            TelefonuUzytkownika.Text = (string)wybrane.Row.ItemArray[5];
            PeselUzytkownika.Text = (string)wybrane.Row.ItemArray[6];
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = ImieUzytkownika.Text;
                string surname = NazwiskoUzytkownika.Text;
                string email = EmailUzytkownika.Text;
                string pesel = PeselUzytkownika.Text;
                string data = DataUrodzinUzytkownika.Text;
                string telefon = TelefonuUzytkownika.Text;
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlCommand command4 = new MySqlCommand("UPDATE `WlascicielMieszkania` SET `DataUrodzenia`=@date,`Email`=@email,`Imie`=@name,`Nazwisko`=@surname,`NrTelefonu`=@telefon,`PESEL`=@pesel WHERE `IdWlasciciela`=@id", conn);
                    command4.Parameters.Add("@id", MySqlDbType.Int32).Value = ((int)wybrane.Row.ItemArray[0]).ToString();
                    command4.Parameters.Add("@date", MySqlDbType.Date).Value = data;
                    command4.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                    command4.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                    command4.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;
                    command4.Parameters.Add("@telefon", MySqlDbType.VarChar).Value = telefon;
                    command4.Parameters.Add("@pesel", MySqlDbType.VarChar).Value = pesel;
                    if (command4.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Konto zaktualizowane.");
                        conn.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
