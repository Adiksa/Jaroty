﻿using MySql.Data.MySqlClient;
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
    /// Logika interakcji dla klasy DodajMieszkanieOp.xaml
    /// </summary>
    public partial class DodajMieszkanieOp : Window
    {
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public DodajMieszkanieOp()
        {
            InitializeComponent();
            try
            {
                var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataTable wlasciciele = new DataTable();
                    String query = "Select * FROM WlascicielMieszkania";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    adapter.SelectCommand = command;
                    adapter.Fill(wlasciciele);
                    if (wlasciciele != null)
                    {
                        foreach (DataRow row in wlasciciele.Rows)
                        {
                            IdWlasciciel.Items.Add(row.Field<int>(0));
                        }
                        IdWlasciciel.SelectedIndex = 0;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Ulica.Text.Length > 0 && NrBloku.Text.Length > 0 && NrMieszkania.Text.Length > 0 && Metraz.Text.Length > 0)
            {
                try
                {
                    var connstr = "server=remotemysql.com;uid=9eO1BYpNqH;pwd=Dcs0DDWGze;database=9eO1BYpNqH";
                    using (var conn = new MySqlConnection(connstr))
                    {
                        conn.Open();
                        DataTable table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `Mieszkanie`(`IdWlasciciela`, `Metraz`, `NrBloku`, `NrMieszkania`, `Ulica`) VALUES (@iw, @met, @nrb, @nrm, @uli)", conn);
                        command.Parameters.Add("@iw", MySqlDbType.Int16).Value = IdWlasciciel.SelectedItem.ToString();
                        command.Parameters.Add("@met", MySqlDbType.Int16).Value = Metraz.Text;
                        command.Parameters.Add("@nrb", MySqlDbType.Int16).Value = NrBloku.Text;
                        command.Parameters.Add("@nrm", MySqlDbType.Int16).Value = NrMieszkania.Text;
                        command.Parameters.Add("@uli", MySqlDbType.VarChar).Value = Ulica.Text;
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Dodano mieszkanie.");
                            conn.Close();
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
                MessageBox.Show("Wypełnij formularz poprawnie.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
