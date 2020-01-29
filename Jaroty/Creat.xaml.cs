using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using MySql.Data.MySqlClient;

namespace Jaroty
{
    /// <summary>
    /// Logika interakcji dla klasy Creat.xaml
    /// </summary>
    public partial class Creat : Window
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
        public Creat()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User.Visibility = Visibility.Hidden;
            this.Height = 200;
            if (UserGroup.SelectedIndex == 0)
            {
                this.Height = 500;
                User.Visibility = Visibility.Visible;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            int usertype = UserGroup.SelectedIndex;
            string username = NazwaUzytkownika.Text;
            string password = HasloUzytkownika.Password;
            if(usertype==-1)
            {
                MessageBox.Show("Wybierz grupę użytkownika.");
            }
            else
            {
                if (usertype == 0)
                {
                    string name = ImieUzytkownika.Text;
                    string surname = NazwiskoUzytkownika.Text;
                    string email = EmailUzytkownika.Text;
                    string pesel = PeselUzytkownika.Text;
                    string data = DataUrodzinUzytkownika.Text;
                    string telefon = TelefonuUzytkownika.Text;
                    if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Nie poprawny adres email.");
                    }
                    else
                    {
                        if(name.Length>0&&surname.Length>0&&pesel.Length>0&&data.Length>0&&telefon.Length>0&& username.Length > 0 && password.Length > 0)
                        {
                            try
                            {
                                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                                using (var conn = new MySqlConnection(connstr))
                                {
                                    conn.Open();
                                    DataTable table = new DataTable();
                                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                                    String query = "Select * FROM login WHERE Login=@log";
                                    MySqlCommand command = new MySqlCommand(query, conn);
                                    command.Parameters.Add("@log", MySqlDbType.VarChar).Value = username;
                                    adapter.SelectCommand = command;
                                    adapter.Fill(table);
                                    if (table.Rows.Count > 0)
                                    {
                                        MessageBox.Show("Uzytkownik o takim loginie istnieje.");
                                    }
                                    else
                                    {
                                        MySqlCommand command2 = new MySqlCommand("INSERT INTO `login`( `Login`, `Password`, `UserType`) VALUES (@un, @up, @ut)", conn);
                                        command2.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
                                        command2.Parameters.Add("@up", MySqlDbType.VarChar).Value = password;
                                        command2.Parameters.Add("@ut", MySqlDbType.Int16).Value = usertype;
                                        if (command2.ExecuteNonQuery() == 1)
                                        {
                                            DataTable table3 = new DataTable();
                                            MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                                            String query3 = "Select * FROM login WHERE Login=@log";
                                            MySqlCommand command3 = new MySqlCommand(query3, conn);
                                            command3.Parameters.Add("@log", MySqlDbType.VarChar).Value = username;
                                            adapter3.SelectCommand = command;
                                            adapter3.Fill(table3);
                                            int ID = table3.Rows[0].Field<int>(0);
                                            MySqlCommand command4 = new MySqlCommand("INSERT INTO `WlascicielMieszkania`(`IdWlasciciela`, `DataUrodzenia`, `Email`, `Imie`, `Nazwisko`, `NrTelefonu`, `PESEL`) VALUES (@id, @date, @email, @name, @surname, @telefon, @pesel)", conn);
                                            command4.Parameters.Add("@id", MySqlDbType.Int32).Value = ID.ToString();
                                            command4.Parameters.Add("@date", MySqlDbType.Date).Value = data;
                                            command4.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                                            command4.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                                            command4.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;
                                            command4.Parameters.Add("@telefon", MySqlDbType.VarChar).Value = telefon;
                                            command4.Parameters.Add("@pesel", MySqlDbType.VarChar).Value = pesel;
                                            if (command4.ExecuteNonQuery() == 1)
                                            {
                                                MessageBox.Show("Konto założone.");
                                                conn.Close();
                                                this.Close();
                                            }
                                            else
                                            {
                                                MessageBox.Show("ERROR");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("ERROR");
                                        }
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
                            MessageBox.Show("Uzupelnij formularz rejestracyjny.");
                        }
                    }
                }
                else
                {
                    if (username.Length > 0 && password.Length > 0)
                    {
                        try
                        {
                            var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                            using (var conn = new MySqlConnection(connstr))
                            {
                                conn.Open();
                                DataTable table = new DataTable();
                                MySqlDataAdapter adapter = new MySqlDataAdapter();
                                String query = "Select * FROM login WHERE Login=@log";
                                MySqlCommand command = new MySqlCommand(query, conn);
                                command.Parameters.Add("@log", MySqlDbType.VarChar).Value = username;
                                adapter.SelectCommand = command;
                                adapter.Fill(table);
                                if (table.Rows.Count > 0)
                                {
                                    MessageBox.Show("Uzytkownik o takim loginie istnieje.");
                                }
                                else
                                {
                                    MySqlCommand command2 = new MySqlCommand("INSERT INTO `login`( `Login`, `Password`, `UserType`) VALUES (@un, @up, @ut)", conn);
                                    command2.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
                                    command2.Parameters.Add("@up", MySqlDbType.VarChar).Value = password;
                                    command2.Parameters.Add("@ut", MySqlDbType.Int16).Value = usertype;
                                    if (command2.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Konto założone.");
                                        conn.Close();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("ERROR");
                                    }
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
                        MessageBox.Show("Uzupelnij formularz rejestracyjny.");
                    }
                }
            }
        }

        private void NazwaUzytkownika_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsAlphaNumeric(NazwaUzytkownika.Text))
            {
                MessageBox.Show("Błędna nazwa użytkownika.\n" +
                    "Nazwa użytkownika może zawierać tylko male/duze litery i cyfry.\n" +
                    "Nazwa użytkownika musi zawierać conajmniej jeden znak.");
                NazwaUzytkownika.Text = "";
            }
        }
       
        private void HasloUzytkownika_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!IsAlphaNumeric(HasloUzytkownika.Password))
            {
                MessageBox.Show("Błędne hasło.\n" +
                    "Hasło użytkownika może zawierać tylko male/duze litery i cyfry.\n" +
                    "Hasło użytkownika musi zawierać conajmniej jeden znak.");
                HasloUzytkownika.Password = "";
            }
        }
    }
}
