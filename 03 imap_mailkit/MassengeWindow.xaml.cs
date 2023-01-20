using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MassengeWindow : Window
    {
        public string myEmail;
        public string myPassword;
        public List<Attachment> attachments = new List<Attachment>();

        public MassengeWindow(string myEmail, string myPassword)
        {
            InitializeComponent();
            this.myEmail = myEmail;
            this.myPassword = myPassword;
        }
        public MassengeWindow()
        {
            InitializeComponent();
        }

        private void AttachFile()
        {
            //var result = MessageBox.Show("Do you want to attach a file?", "Attach File", MessageBoxButton.YesNo);
            //if (result == MessageBoxResult.Yes)
            //{
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                attachments.Add(new Attachment(dialog.FileName));
                txtAddFile.Text += $"{System.IO.Path.GetFileNameWithoutExtension(dialog.FileName)}\n";
            }
            // }
        }

        private void SendBtnClick(object sender, RoutedEventArgs e)
        {
            // --------------- send email using SMTP ---------------

            // create mail message object

            //var resalt = MessageBox.Show("You messeng high priority?", "Attach File", MessageBoxButton.YesNo);
            MailPriority priority = MailPriority.Low;
            if (chbHighPriority.IsChecked == true)
            {
                priority = MailPriority.High;
            }


            MailMessage mail = new MailMessage(myEmail, toTxtBox.Text)
            {
                Subject = subjectTxtBox.Text,
                Body = $"<h1>Hello HTML in mail</h1><p>{bodyTxtBox.Text}</p>",
                IsBodyHtml = true,
                Priority = priority
            };

            // add attachments
            // AttachFile(mail);
            if (attachments != null)
            {
                foreach (Attachment attachment in attachments)
                {
                    mail.Attachments.Add(attachment);
                }
            }

            // send email with SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(myEmail, myPassword)
            };

            client.SendCompleted += Client_SendCompleted;

            try
            {
                client.Send(mail);
                sendBtn.Background = Brushes.LightGreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Mail sent!");
        }

        private void addFileBtnClick(object sender, RoutedEventArgs e)
        {
            AttachFile();
        }
    }
}
