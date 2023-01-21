using MailKit.Net.Imap;
using MailKit.Security;
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
        //private string password ;
        //private string login ;
        public int port;
        public string host = ""; 

        public Authorization()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (host == "")
            {
                MessageBox.Show("You not cheked mailbox");
                return;
            }
            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect (host, port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(txtLogin.Text, txtPassword.Text);
                   
                }
                MailboxWindows dilog = new MailboxWindows(txtLogin.Text, txtPassword.Text, host,port);
                dilog.password = txtPassword.Text;
                dilog.username = txtLogin.Text;
                dilog.host = host;
                dilog.port = port;                
                dilog.ShowDialog();
            }
            catch (Exception ex)   
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Enter login or password error.");
            }                      
        }

        private void mMailboxGoogle_Click(object sender, RoutedEventArgs e)
        {
            
            host = "imap.gmail.com";
            port = 993; 
        }

        private void mMailboxUkrNet_Click(object sender, RoutedEventArgs e)
        {
            host = "imap.ukr.net";
            port = 993;
        }
    }
}
