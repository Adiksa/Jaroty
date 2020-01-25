using System;
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

namespace Jaroty
{
    /// <summary>
    /// Logika interakcji dla klasy Creat.xaml
    /// </summary>
    public partial class Creat : Window
    {
        public static bool IsAlphaNumeric(string strToCheck)
        {
            return strToCheck.All(char.IsLetterOrDigit);
        }
        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
            if(usertype==1)
            {
                string name = ImieUzytkownika.Text;
                string surname = NazwaUzytkownika.Text;
                string email = EmailUzytkownika.Text;
                string pesel = PeselUzytkownika.Text;
                if (!IsEmailValid(email))
                {
                    MessageBox.Show("Nie poprawny adres email.");
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
