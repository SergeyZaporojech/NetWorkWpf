using MailKit.Net.Imap;
using System;
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

namespace _03_imap_mailkit
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private string password = "";
        private string login = "";
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    client.Authenticate(txtLogin.Text, txtPassword.Text);
                }
                MailboxWindows dilog = new MailboxWindows();
                dilog.password = txtPassword.Text;
                dilog.username = txtLogin.Text;
            }
            catch (Exception)   
            {
                MessageBox.Show("Enter login or password error.");
            }
            if (password == null && login == null)
                MessageBox.Show("You have not selected a mailbox.");
            
        }

        private void mMailboxGoogle_Click(object sender, RoutedEventArgs e)
        {
            login = "zaporojechs@gmail.com";
            password = "rvckocqidlykxlga";
        }

        private void mMailboxUkrNet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
