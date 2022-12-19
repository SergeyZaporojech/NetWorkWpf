using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _03_imap_mailkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MailboxWindows : Window
    {

        //const string username = "zaporojechs@gmail.com"; 
        //const string password = "rvckocqidlykxlga"; 
        public string username ; 
        public string password ; 
        public MailboxWindows()
        {
            InitializeComponent();
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_Loaded_1(object sender, RoutedEventArgs e)
        {
   
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var client = new ImapClient())
            {

                
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(username, password);

                foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
                {
                    txtFolders.Text += ($"{item.Name}\n");
                }
                var folder = client.GetFolder(SpecialFolder.Sent);
                folder.Open(FolderAccess.ReadWrite);

                IList<UniqueId> uids = folder.Search(SearchQuery.All);

                for (int i = 0; i < 10; i++)
                {
                    MimeMessage message = folder.GetMessage(i);
                    txtMessage.Text += ($"{message.Subject} - {new string(message.TextBody.Take(10).ToArray())}...\n");
                }               

                client.Disconnect(true);
            }
        }
    }
}
