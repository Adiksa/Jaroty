﻿using System;
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
            CreatAcc.Visibility = Visibility.Hidden;
        }
    }
}
