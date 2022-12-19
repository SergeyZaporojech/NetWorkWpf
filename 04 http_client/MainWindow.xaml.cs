using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace _04_http_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path;
        //private CancellationTokenSource ctSourse;
        //private CancellationToken token;
        public MainWindow()
        {
            InitializeComponent();
            CbCaregoryImages();
        }
        private void CbCaregoryImages()
        {
            cbCategory.Items.Add("Animals");
            cbCategory.Items.Add("Travel");
            cbCategory.Items.Add("Three");
            cbCategory.Items.Add("Mountain");
            cbCategory.Items.Add("Experimental");
            cbCategory.Items.Add("Nature");      
            cbCategory.Items.Add("Street Photography");        
        }

        private void AddHistoryItem(string fileName)
        {
            historyList.Items.Add($"{DateTime.Now.ToShortTimeString()}: {fileName}");
        }
        private async void DownloadBtnClick(object sender, RoutedEventArgs e)
        {
            string url = $"{urlTxtBox.Text}/{sizeTxtBox.Text}x{sizeTxtBox2.Text}/?{cbCategory.Text}&1";

            // TODO: add URL validation
            //  * check if URL is not empty
            //  * check if URL is valid

            // generate new file name
            string name = $"{Guid.NewGuid().ToString()}.jpg";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = name;
            
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            #region   HttpClient
            // get source file extension
            //string extension = Path.GetExtension(url);
            // create destination file path
            //string dest = Path.Combine(folderName, $"{name}.jpg");

            // create directory if it does not exist
            //if (!Directory.Exists(folderName))
            //    Directory.CreateDirectory(folderName);

            // 1 - download resources using HttpClient
            //HttpClient client = new HttpClient();
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync(url);
            //    this.Title = "Status: " + response.StatusCode;
            //
            //    using (var stream = File.Create(dest))
            //    {
            //        // write file content
            //        await response.Content.CopyToAsync(stream);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion

            // 2 - download files using WebClient            
            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            webClient.DownloadFileAsync(new Uri(url), path);

            #region Cansel download
            //ctSourse = new CancellationTokenSource();
            //token = ctSourse.Token;
            //Task thread = new Task((()=> webClient.DownloadFileAsync(new Uri(url), path)), token);
            //thread.Start();

            //if (token.IsCancellationRequested)
            //{
            //    Dispatcher.Invoke(() =>
            //    {                    
            //        this.Title += $" Add cansel";
            //    });
            //    return;
            //}
            #endregion

            // add to history 
            AddHistoryItem(url);
        }

        private void WebClient_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.Title = $"Download Completed";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Title = $"Progress: {e.ProgressPercentage}%";
            progressBar.Value = e.ProgressPercentage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (path != null)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                };
                Process.Start(psi);                
            }
            else MessageBox.Show("The image is not loaded");
        }

        //private void CanselBtnClick(object sender, RoutedEventArgs e)
        //{
        //    ctSourse.Cancel();
        //}
    }
}
