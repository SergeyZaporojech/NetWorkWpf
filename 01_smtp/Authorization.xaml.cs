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

namespace _01_smtp
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        private string password;
        private string login;
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == password || txtLogin.Text == login)
            {
                MailboxWindows dilog = new MailboxWindows();
                //dilog.myPassword= txtPassword.Text;
                //dilog.myEmail=  txtLogin.Text;
                dilog.Show();
            }
            else
                if(password==null && login==null)
                MessageBox.Show("You have not selected a mailbox.");
            else
                MessageBox.Show("Enter login or password error. ");
            
        }

        private void mMailboxGoogle_Click(object sender, RoutedEventArgs e)
        {
           login= "zaporojechs@gmail.com";
           password = "rvckocqidlykxlga";
        }

        private void mMailboxUkrNet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
