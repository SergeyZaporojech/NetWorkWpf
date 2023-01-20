using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
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
        public string username ; 
        public string password ;
        public string host;
        public int port;
        int start = 0;
        int page = 10;
        int end = 0;
        public MailboxWindows()
        {

        }
        public MailboxWindows(string username,  string password, string host, int port)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.host = host;
            this.port = port;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            using (var client = new ImapClient())
            {
                client.Connect(host, port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(username, password);

                txtFolders.Items.Clear();

                foreach (var item in client.GetFolders((client.PersonalNamespaces[0])))
                {
                    txtFolders.Items.Add($"{item.Name}");
                    cbFolders.Items.Add(item.Name);
                }
                 
                
                //var folder = client.GetFolder(SpecialFolder.Sent);
                //folder.Open(FolderAccess.ReadWrite);

                //IList<UniqueId> uids = folder.Search(SearchQuery.All);

                //for (int i = 0; i < 20; i++)
                //{
                //    MimeMessage message = folder.GetMessage(i);

                //    //txtMessage.Text += ($"{message.Subject} - {new string(message.TextBody.Take(10).ToArray())}...\n");
                //    txtMessage.Text += ($"{folder.Name} : {message.Subject} \n");
                //} 

                client.Inbox.Open(FolderAccess.ReadOnly);
                var uids = client.Inbox.Search(SearchQuery.All);

                LoadtxtMasseges( client, uids);

                #region LoadMessages
                //for (int i = start; i < end; i++)
                //{
                //    var message = client.Inbox.GetMessage(uids[i]);
                //    if (message.TextBody != null)
                //        txtMessage.Text += ($"{message.Subject} : {new string(message.TextBody.Take(10).ToArray())} .... \n");
                //    else
                //        txtMessage.Text += ($"{message.Subject} : .... \n");
                //}
                //foreach (var uid in uids)
                //{
                //    var message = client.Inbox.GetMessage(uid);
                //    txtMessage.Text += ($"{message.Subject} : {new string (message.TextBody.Take(10).ToArray())} .... \n");
                //}
                #endregion
               
                client.Disconnect(true);
            }
        }


        private void LoadtxtMasseges(ImapClient client, IList<UniqueId> uids)
        {
            txtMessage.Items.Clear();
            int countMesssage = uids.Count();
            end = start + page;
            for (int i = start; i < end; i++)
            {
                var message = client.Inbox.GetMessage(uids[i]);
                if (message.TextBody != null)
                    txtMessage.Items.Add($"{message.Subject} : {new string(message.TextBody.Take(10).ToArray())} .... ");
                
                else
                    txtMessage.Items.Add($"{message.Subject} : .... \n");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start += 10;
            Load();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (start != 0) start -= 10;
            Load();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MassengeWindow dilog = new MassengeWindow(username, password);
            dilog.Show();
        }
    }
}
